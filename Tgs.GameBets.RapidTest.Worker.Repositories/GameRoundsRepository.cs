using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Tgs.GameBets.RapidTest.Worker.Core;
using Tgs.GameBets.RapidTest.Worker.Core.Models;
using Tgs.GameRounds.Client;

namespace Tgs.GameBets.RapidTest.Worker.Repositories;

internal class GameRoundsRepository(
	ILogger<GameRoundsRepository> logger,
	IOptions<PolicySettingsOptions> options,
	IGameRoundsClient client) : IGameRoundsRepository
{
	private readonly PolicySettingsOptions _policySettingsOptions = options.Value;

	public async ValueTask<Guid> CommandAsync(GameRoundCommand command, CancellationToken cancellationToken = default)
	{
		var status = GameRounds.Common.Enums.RoundStatus.Open;
		var clientStatus = GameRounds.Common.Enums.RoundClientStatus.Open;
		var externalCreatedOn = command.ExternalCreatedOn;
		var externalClosedOn = (DateTimeOffset?)null;
		var bet = command.Bet;
		var validBet = command.Bet;
		var settle = 0.0m;
		var commission = 0.0m;
		var turnover = 0.0m;
		var playerWinLoss = 0.0m;

		if (command.BetState is not GameBets.Common.Enums.BetState.Waiting and not GameBets.Common.Enums.BetState.Running)
		{
			status = GameRounds.Common.Enums.RoundStatus.Closed;
			clientStatus = command.BetState switch
			{
				GameBets.Common.Enums.BetState.Loss => GameRounds.Common.Enums.RoundClientStatus.Loss,
				GameBets.Common.Enums.BetState.Win => GameRounds.Common.Enums.RoundClientStatus.Win,
				GameBets.Common.Enums.BetState.Draw => GameRounds.Common.Enums.RoundClientStatus.Draw,
				GameBets.Common.Enums.BetState.HalfWin => GameRounds.Common.Enums.RoundClientStatus.Win,
				GameBets.Common.Enums.BetState.HalfLoss => GameRounds.Common.Enums.RoundClientStatus.Loss,
				_ => GameRounds.Common.Enums.RoundClientStatus.Void,
			};
			externalClosedOn = command.ExternalClosedOn;
			validBet = command.ValidBet;
			settle = command.Settle;
			commission = command.Commission;
			turnover = command.Turnover;
			playerWinLoss = command.WinLoss;
		}

		var source = new GameRounds.Common.ViewModels.RoundCommandViewModel
		{
			Id = command.Id,
			CreatedOn = command.CreatedOn,
			GameProviderId = command.GameProviderId,
			GameProviderConfigurationId = command.GameProviderConfigurationId,
			GameId = command.GameId,
			BrandId = command.BrandId,
			PlayerCode = command.PlayerCode,
			WalletId = command.WalletId,
			CurrencyCode = command.CurrencyCode,
			Status = status,
			ClientStatus = clientStatus,
			ExternalRoundId = command.ExternalBetId,
			ExternalCreatedOn = externalCreatedOn,
			ExternalClosedOn = externalClosedOn,
			BetAmount = bet,
			ValidBetAmount = validBet,
			SettleAmount = settle,
			Commission = commission,
			Turnover = turnover,
			PlayerWinLoss = playerWinLoss,
			AdditionalData = command.AdditionalData
		};

		var retryCount = _policySettingsOptions.RetryCount;
		var retrySeconds = _policySettingsOptions.RetrySeconds;
		var retryPolicy = Policy
			.Handle<HttpRequestException>()
			.WaitAndRetryAsync(
				retryCount: retryCount,
				retryAttempt => TimeSpan.FromSeconds(Math.Pow(retrySeconds, retryAttempt)),
				onRetry: (exception, retryTime, retryCount, context) => logger.LogWarning(exception, "{logWarning}",
					new
					{
						LogOn = DateTime.Now.ToString("u"),
						RetryTime = retryTime,
						RetryCount = retryCount,
						Data = new
						{
							source.GameProviderId,
							source.GameId,
							source.PlayerCode,
							source.WalletId,
							source.ExternalRoundId,
							ExternalCreatedOn = source.ExternalCreatedOn.ToString("u")
						},
						exception.Message
					}));

		return await retryPolicy.ExecuteAsync(() => client.RoundCommandAsync(
			source: source,
			cancellationToken: cancellationToken).AsTask())
			.ConfigureAwait(false);
	}
}
