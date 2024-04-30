using Google.Protobuf.WellKnownTypes;
using Tgs.ServiceUtility;
using Valhalla.MessageQueue;
using Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

namespace Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

internal class GameBetCommandService(
	IMessageSender messageSender) : ICommandService<GameBetCommand>
{
	public ValueTask ExecuteAsync(GameBetCommand command, CancellationToken cancellationToken = default)
	{
		if (command.BetState is GameBets.Common.Enums.BetState.Waiting or GameBets.Common.Enums.BetState.Running)
		{
			return ValueTask.CompletedTask;
		}

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

		var data = new GameBets.Worker.Common.Protos.GameBetsWorkerRequest
		{
			GameBetId = command.Id,
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
			ExternalCreatedOn = command.ExternalCreatedOn.ToTimestamp(),
			ExternalClosedOn = command.ExternalClosedOn.ToTimestamp(),
			ExternalPayoffOn = command.ExternalPayoffOn.ToTimestamp(),
			ExternalResettleOn = command.ExternalResettleOn.ToTimestamp(),
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
			Commission = command.Commission,
			Turnover = command.Turnover,
			WinLoss = command.WinLoss,
			Jackpot = command.Jackpot,
			AdditionalData = command.AdditionalData.ToJsonString(),
		};

		return messageSender.PublishAsync(
			subject: GameBets.Worker.Common.GameBetsWorkerNatsConst.GameBetsWorkerRequest,
			data: data,
			cancellationToken: cancellationToken);
	}
}
