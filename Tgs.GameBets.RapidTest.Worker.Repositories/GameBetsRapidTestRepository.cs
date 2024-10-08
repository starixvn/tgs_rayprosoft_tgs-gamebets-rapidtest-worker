using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Tgs.GameBets.Client;
using Tgs.GameBets.RapidTest.Worker.Core;
using Tgs.GameBets.RapidTest.Worker.Core.Models;

namespace Tgs.GameBets.RapidTest.Worker.Repositories;

internal class GameBetsRapidTestRepository(
	ILogger<GameBetsRapidTestRepository> logger,
	IOptions<PolicySettingsOptions> policySettingsOptions,
	IGameBetsClient client) : IGameBetsRapidTestRepository
{
	private readonly PolicySettingsOptions _policySettingsOptions = policySettingsOptions.Value;

	public async ValueTask<bool> CommandAsync(GameBetRapidTestCommand command, CancellationToken cancellationToken = default)
	{
		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel
		{
			GameProviderCode = command.GameProviderCode,
			SubGameProviderCode = command.SubGameProviderCode,
			GameId = command.GameId,
			ResellerCode = command.ResellerCode,
			LicenseeCode = command.LicenseeCode,
			BrandCode = command.BrandCode,
			PlayerCode = command.PlayerCode,
			Username = command.Username,
			CurrencyCode = command.CurrencyCode,
			WalletId = command.WalletId,
			ExternalRoundId = command.ExternalRoundId,
			ExternalBetId = command.ExternalBetId,
			ExternalGameId = command.ExternalGameId,
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
			Jackpot = command.Jackpot
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
							source.GameProviderCode,
							source.SubGameProviderCode,
							source.GameId,
							source.ResellerCode,
							source.LicenseeCode,
							source.BrandCode,
							source.PlayerCode,
							source.Username,
							source.CurrencyCode,
							source.WalletId,
							source.ExternalRoundId,
							source.ExternalBetId,
							source.ExternalGameId
						},
						exception.Message
					}));

		return await retryPolicy.ExecuteAsync(() => client.GameBetRapidTestCommandAsync(
			source: source,
			cancellationToken: cancellationToken).AsTask())
			.ConfigureAwait(false);
	}
}
