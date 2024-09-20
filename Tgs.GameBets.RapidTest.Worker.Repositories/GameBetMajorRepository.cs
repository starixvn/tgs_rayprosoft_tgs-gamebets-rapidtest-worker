using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Tgs.GameBets.Client;
using Tgs.GameBets.Common.ViewModels;
using Tgs.GameBets.RapidTest.Worker.Core;
using Tgs.GameBets.RapidTest.Worker.Core.Models;

namespace Tgs.GameBets.RapidTest.Worker.Repositories;

internal class GameBetMajorRepository(
	ILogger<GameBetMajorRepository> logger,
	IOptions<PolicySettingsOptions> options,
	IGameBetsClient client) : IGameBetMajorRepository
{
	private readonly PolicySettingsOptions _policySettingsOptions = options.Value;

	public async ValueTask<GameBetMajorInfoViewModel> CommandAsync(GameBetMajorCommand command, CancellationToken cancellationToken = default)
	{
		var source = new GameBetMajorCommandViewModel
		{
			GameProviderId = command.GameProviderId,
			GameProviderConfigurationId = command.GameProviderConfigurationId,
			GameId = command.GameId,
			ResellerId = command.ResellerId,
			LicenseeId = command.LicenseeId,
			BrandId = command.BrandId,
			PlayerCode = command.PlayerCode,
			CurrencyCode = command.CurrencyCode,
			WalletId = command.WalletId,
			ExternalRoundId = command.ExternalRoundId,
			ExternalBetId = command.ExternalBetId,
			ExternalCreatedOn = command.ExternalCreatedOn,
			ExternalClosedOn = command.ExternalClosedOn,
			ExternalPayoffOn = command.ExternalPayoffOn,
			ExternalResettleOn = command.ExternalResettleOn,
			BetState = command.BetState,
			BetType = command.BetType,
			FreeGameState = command.FreeGameState,
			GambleState = command.GambleState,
			CashoutState = command.CashoutState,
			ResettleState = command.ResettleState,
			JackpotState = command.JackpotState,
			CurrencyRatio = command.CurrencyRatio,
			Bet = command.Bet,
			ValidBet = command.ValidBet,
			Settle = command.Settle,
			WinLoss = command.WinLoss,
			Turnover = command.Turnover,
			Commission = command.Commission,
			Contribute = command.Contribute,
			Jackpot = command.Jackpot,
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
							source.ExternalBetId,
							ExternalCreatedOn = source.ExternalCreatedOn.ToString("u")
						},
						exception.Message
					}));

		return await retryPolicy.ExecuteAsync(() => client.GameBetMajorCommandAsync(
			source: source,
			cancellationToken: cancellationToken).AsTask())
			.ConfigureAwait(false);
	}
}
