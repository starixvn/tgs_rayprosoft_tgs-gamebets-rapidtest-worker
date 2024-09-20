using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Tgs.GameBets.RapidTest.Client;
using Tgs.GameBets.RapidTest.Worker.Core;
using Tgs.GameBets.RapidTest.Worker.Core.Models;

namespace Tgs.GameBets.RapidTest.Worker.Repositories;

internal class GameBetsRapidTestRepository(
	ILogger<GameBetsRapidTestRepository> logger,
	IOptions<PolicySettingsOptions> policySettingsOptions,
	IGameBetsRapidTestClient client) : IGameBetsRapidTestRepository
{
	private readonly PolicySettingsOptions _policySettingsOptions = policySettingsOptions.Value;

	public async ValueTask<bool> CommandAsync(GameBetRapidTestCommand command, CancellationToken cancellationToken = default)
	{
		var betState = command.BetState switch
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

		var betType = command.BetType switch
		{
			GameBets.Common.Enums.BetType.Normal => Common.Enums.BetType.Normal,
			_ => Common.Enums.BetType.Normal
		};

		var freeGameState = command.FreeGameState switch
		{
			GameBets.Common.Enums.FreeGameState.FreeGame => Common.Enums.FreeGameState.FreeGame,
			_ => Common.Enums.FreeGameState.None
		};

		var gambleState = command.GambleState switch
		{
			GameBets.Common.Enums.GambleState.Gamble => Common.Enums.GambleState.Gamble,
			_ => Common.Enums.GambleState.None
		};

		var cashoutState = command.CashoutState switch
		{
			GameBets.Common.Enums.CashoutState.Cashout => Common.Enums.CashoutState.Cashout,
			_ => Common.Enums.CashoutState.None
		};

		var resettleState = command.ResettleState switch
		{
			GameBets.Common.Enums.ResettleState.Resettle => Common.Enums.ResettleState.Resettle,
			_ => Common.Enums.ResettleState.None
		};

		var jackpotState = command.JackpotState switch
		{
			GameBets.Common.Enums.JackpotState.Jackpot => Common.Enums.JackpotState.Jackpot,
			_ => Common.Enums.JackpotState.None
		};

		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: command.GameProviderCode,
			SubGameProviderCode: command.SubGameProviderCode,
			GameId: command.GameId,
			ResellerCode: command.ResellerCode,
			LicenseeCode: command.LicenseeCode,
			BrandCode: command.BrandCode,
			PlayerCode: command.PlayerCode,
			Username: command.Username,
			CurrencyCode: command.CurrencyCode,
			WalletId: command.WalletId,
			ExternalRoundId: command.ExternalRoundId,
			ExternalBetId: command.ExternalBetId,
			ExternalGameId: command.ExternalGameId,
			BetState: betState,
			BetType: betType,
			FreeGameState: freeGameState,
			GambleState: gambleState,
			CashoutState: cashoutState,
			ResettleState: resettleState,
			JackpotState: jackpotState,
			CurrencyRatio: command.CurrencyRatio,
			Bet: command.Bet,
			ValidBet: command.ValidBet,
			Settle: command.Settle,
			Commission: command.Commission,
			Turnover: command.Turnover,
			WinLoss: command.WinLoss,
			Jackpot: command.Jackpot,
			AdditionalData: command.AdditionalData.ToJsonString());

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
