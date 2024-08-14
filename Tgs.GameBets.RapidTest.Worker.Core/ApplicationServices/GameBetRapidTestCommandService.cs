using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Tgs.GameBets.RapidTest.Client;
using Tgs.ServiceUtility;

namespace Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

internal class GameBetRapidTestCommandService(
	ILogger<GameBetRapidTestCommandService> logger,
	IOptions<PolicySettingsOptions> policySettingsOptions,
	IGameBetsRapidTestClient client) : IGetService<GameBetRapidTestCommand, bool>
{
	private readonly PolicySettingsOptions _policySettingsOptions = policySettingsOptions.Value;

	public async ValueTask<bool> GetAsync(GameBetRapidTestCommand query, CancellationToken cancellationToken = default)
	{
		var betState = query.BetState switch
		{
			GameBets.Common.Enums.BetState.Running => Common.Enums.BetState.Running,
			GameBets.Common.Enums.BetState.Void => Common.Enums.BetState.Void,
			GameBets.Common.Enums.BetState.Refund => Common.Enums.BetState.Refund,
			GameBets.Common.Enums.BetState.Cancel => Common.Enums.BetState.Cancel,
			GameBets.Common.Enums.BetState.Loss => Common.Enums.BetState.Loss,
			GameBets.Common.Enums.BetState.Win => Common.Enums.BetState.Win,
			GameBets.Common.Enums.BetState.Draw => Common.Enums.BetState.Draw,
			GameBets.Common.Enums.BetState.HalfWin => Common.Enums.BetState.HalfWin,
			GameBets.Common.Enums.BetState.HalfLoss => Common.Enums.BetState.HalfLoss,
			_ => Common.Enums.BetState.Waiting
		};

		var betType = query.BetType switch
		{
			GameBets.Common.Enums.BetType.Normal => Common.Enums.BetType.Normal,
			_ => Common.Enums.BetType.Normal
		};

		var freeGameState = query.FreeGameState switch
		{
			GameBets.Common.Enums.FreeGameState.FreeGame => Common.Enums.FreeGameState.FreeGame,
			_ => Common.Enums.FreeGameState.None
		};

		var gambleState = query.GambleState switch
		{
			GameBets.Common.Enums.GambleState.Gamble => Common.Enums.GambleState.Gamble,
			_ => Common.Enums.GambleState.None
		};

		var cashoutState = query.CashoutState switch
		{
			GameBets.Common.Enums.CashoutState.Cashout => Common.Enums.CashoutState.Cashout,
			_ => Common.Enums.CashoutState.None
		};

		var resettleState = query.ResettleState switch
		{
			GameBets.Common.Enums.ResettleState.Resettle => Common.Enums.ResettleState.Resettle,
			_ => Common.Enums.ResettleState.None
		};

		var jackpotState = query.JackpotState switch
		{
			GameBets.Common.Enums.JackpotState.Jackpot => Common.Enums.JackpotState.Jackpot,
			_ => Common.Enums.JackpotState.None
		};

		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: query.GameProviderCode,
			SubGameProviderCode: query.SubGameProviderCode,
			GameId: query.GameId,
			ResellerCode: query.ResellerCode,
			LicenseeCode: query.LicenseeCode,
			BrandCode: query.BrandCode,
			PlayerCode: query.PlayerCode,
			Username: query.Username,
			CurrencyCode: query.CurrencyCode,
			WalletId: query.WalletId,
			ExternalRoundId: query.ExternalRoundId,
			ExternalBetId: query.ExternalBetId,
			ExternalGameId: query.ExternalGameId,
			BetState: betState,
			BetType: betType,
			FreeGameState: freeGameState,
			GambleState: gambleState,
			CashoutState: cashoutState,
			ResettleState: resettleState,
			JackpotState: jackpotState,
			CurrencyRatio: query.CurrencyRatio,
			Bet: query.Bet,
			ValidBet: query.ValidBet,
			Settle: query.Settle,
			Commission: query.Commission,
			Turnover: query.Turnover,
			WinLoss: query.WinLoss,
			Jackpot: query.Jackpot,
			AdditionalData: query.AdditionalData.ToJsonString());

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
