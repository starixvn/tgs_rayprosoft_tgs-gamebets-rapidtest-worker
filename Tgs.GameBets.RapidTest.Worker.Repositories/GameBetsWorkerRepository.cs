using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Tgs.GameBets.RapidTest.Worker.Core;
using Tgs.GameBets.RapidTest.Worker.Core.Models;
using Tgs.GameBets.Worker.Client;

namespace Tgs.GameBets.RapidTest.Worker.Repositories;

internal class GameBetsWorkerRepository(
	ILogger<GameBetsWorkerRepository> logger,
	IOptions<PolicySettingsOptions> options,
	IGameBetsWorkerClient client) : IGameBetsWorkerRepository
{
	private readonly PolicySettingsOptions _policySettingsOptions = options.Value;

	public async ValueTask CommandAsync(GameBetWorkCommand command, CancellationToken cancellationToken = default)
	{
		var betState = command.BetState switch
		{
			GameBets.Common.Enums.BetState.Running => GameBets.Worker.Common.Protos.BetState.Running,
			GameBets.Common.Enums.BetState.Void => GameBets.Worker.Common.Protos.BetState.Void,
			GameBets.Common.Enums.BetState.Refund => GameBets.Worker.Common.Protos.BetState.Refund,
			GameBets.Common.Enums.BetState.Cancel => GameBets.Worker.Common.Protos.BetState.Cancel,
			GameBets.Common.Enums.BetState.Loss => GameBets.Worker.Common.Protos.BetState.Loss,
			GameBets.Common.Enums.BetState.Win => GameBets.Worker.Common.Protos.BetState.Win,
			GameBets.Common.Enums.BetState.Draw => GameBets.Worker.Common.Protos.BetState.Draw,
			GameBets.Common.Enums.BetState.HalfWin => GameBets.Worker.Common.Protos.BetState.HalfWin,
			GameBets.Common.Enums.BetState.HalfLoss => GameBets.Worker.Common.Protos.BetState.HalfLoss,
			_ => GameBets.Worker.Common.Protos.BetState.Waiting
		};

		var betType = command.BetType switch
		{
			GameBets.Common.Enums.BetType.Normal => GameBets.Worker.Common.Protos.BetType.Normal,
			_ => GameBets.Worker.Common.Protos.BetType.Normal
		};

		var freeGameState = command.FreeGameState switch
		{
			GameBets.Common.Enums.FreeGameState.FreeGame => GameBets.Worker.Common.Protos.FreeGameState.FreeGame,
			_ => GameBets.Worker.Common.Protos.FreeGameState.None
		};

		var gambleState = command.GambleState switch
		{
			GameBets.Common.Enums.GambleState.Gamble => GameBets.Worker.Common.Protos.GambleState.Gamble,
			_ => GameBets.Worker.Common.Protos.GambleState.None
		};

		var cashoutState = command.CashoutState switch
		{
			GameBets.Common.Enums.CashoutState.Cashout => GameBets.Worker.Common.Protos.CashoutState.Cashout,
			_ => GameBets.Worker.Common.Protos.CashoutState.None
		};

		var resettleState = command.ResettleState switch
		{
			GameBets.Common.Enums.ResettleState.Resettle => GameBets.Worker.Common.Protos.ResettleState.Resettle,
			_ => GameBets.Worker.Common.Protos.ResettleState.None
		};

		var jackpotState = command.JackpotState switch
		{
			GameBets.Common.Enums.JackpotState.Jackpot => GameBets.Worker.Common.Protos.JackpotState.Jackpot,
			_ => GameBets.Worker.Common.Protos.JackpotState.None
		};

		var source = new GameBets.Worker.Common.ViewModels.GameBetHandleViewModel
		{
			GameBetId = command.GameBetId,
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
			BetState = betState,
			BetType = betType,
			FreeGameState = freeGameState,
			GambleState = gambleState,
			CashoutState = cashoutState,
			ResettleState = resettleState,
			JackpotState = jackpotState,
			CurrencyRatio = command.CurrencyRatio,
			Bet = command.Bet,
			ValidBet = command.ValidBet,
			Settle = command.Settle,
			WinLoss = command.WinLoss,
			Turnover = command.Turnover,
			Commission = command.Commission,
			Contribute = command.Contribute,
			CreatedOn = command.CreatedOn,
			ClosedOn = command.ClosedOn,
			Jackpot = command.Jackpot,
			AdditionalData = command.AdditionalData,
			ExtendedData = command.ExtendedData
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

		await retryPolicy.ExecuteAsync(() => client.HandleAsync(
			source: source,
			cancellationToken: cancellationToken).AsTask())
		   .ConfigureAwait(false);
	}
}
