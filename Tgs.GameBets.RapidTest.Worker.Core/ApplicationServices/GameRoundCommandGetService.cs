using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Tgs.GameBets.Common.Enums;
using Tgs.GameRounds.Client;
using Tgs.ServiceUtility;

namespace Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

internal class GameRoundCommandGetService(
	ILogger<GameRoundCommandGetService> logger,
	IOptions<PolicySettingsOptions> options,
	IGameRoundsClient client) : IGetService<GameRoundCommand, Guid>
{
	private readonly PolicySettingsOptions _policySettingsOptions = options.Value;

	public async ValueTask<Guid> GetAsync(GameRoundCommand query, CancellationToken cancellationToken = default)
	{
		var status = GameRounds.Common.Enums.RoundStatus.Open;
		var clientStatus = GameRounds.Common.Enums.RoundClientStatus.Open;
		var externalCreatedOn = query.ExternalCreatedOn;
		var externalClosedOn = (DateTimeOffset?)null;
		var bet = query.Bet;
		var validBet = query.ValidBet;
		var settle = 0.0m;
		var commission = 0.0m;
		var turnover = 0.0m;
		var playerWinLoss = 0.0m;

		if (query.BetState is not BetState.Waiting and not BetState.Running)
		{
			status = GameRounds.Common.Enums.RoundStatus.Closed;
			clientStatus = query.BetState switch
			{
				BetState.Loss => GameRounds.Common.Enums.RoundClientStatus.Loss,
				BetState.Win => GameRounds.Common.Enums.RoundClientStatus.Win,
				BetState.Draw => GameRounds.Common.Enums.RoundClientStatus.Draw,
				BetState.HalfWin => GameRounds.Common.Enums.RoundClientStatus.Win,
				BetState.HalfLoss => GameRounds.Common.Enums.RoundClientStatus.Loss,
				_ => GameRounds.Common.Enums.RoundClientStatus.Void,
			};
			externalClosedOn = query.ExternalClosedOn;
			settle = query.Settle;
			commission = query.Commission;
			turnover = query.Turnover;
			playerWinLoss = query.PlayerWinLoss;
		}

		var source = new GameRounds.Common.ViewModels.RoundCommandViewModel(
			GameProviderId: query.GameProviderId,
			GameProviderConfigurationId: query.GameProviderConfigurationId,
			GameId: query.GameId,
			BrandId: query.BrandId,
			PlayerCode: query.PlayerCode,
			WalletId: query.WalletId,
			CurrencyCode: query.CurrencyCode,
			Status: status,
			ClientStatus: clientStatus,
			ExternalRoundId: query.ExternalBetId,
			ExternalCreatedOn: externalCreatedOn,
			ExternalClosedOn: externalClosedOn,
			BetAmount: bet,
			ValidBetAmount: validBet,
			SettleAmount: settle,
			Commission: commission,
			Turnover: turnover,
			PlayerWinLoss: playerWinLoss,
			AdditionalData: query.AdditionalData);

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
