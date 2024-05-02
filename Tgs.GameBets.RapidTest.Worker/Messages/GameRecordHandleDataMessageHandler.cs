using System.Text.Json.Nodes;
using Tgs.GameBets.RapidTest.Worker.Core;
using Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

using Valhalla.MessageQueue;

namespace Tgs.GameBets.RapidTest.Worker.Messages;

public class GameRecordHandleDataMessageHandler(
	ILogger<GameRecordHandleDataMessageHandler> logger,
	IRapidTestDispatcher dispatcher) : IMessageHandler<GameRecord.Handle.Common.Protos.GameWagerPublish>
{
	public ValueTask HandleAsync(string subject, GameRecord.Handle.Common.Protos.GameWagerPublish data, IEnumerable<MessageHeaderValue>? headerValues, CancellationToken cancellationToken = default)
	{
		logger.LogInformation("LogOn:{logOn} Subject:{subject} Mesaage:This message has been received.({data})",
			DateTime.Now.ToString("o"),
			subject,
			new
			{
				data.GameProviderCode,
				data.SubGameProviderCode,
				data.GameId,
				data.ResellerCode,
				data.LicenseeCode,
				data.BrandCode,
				data.PlayerCode,
				data.Username,
				WalletId = data.WalletId.ToGuid(),
				data.ExternalRoundId,
				data.ExternalWagerId,
				ExternalCreatedOn = data.ExternalCreatedOn.ToDateTimeOffset()
			});

		var betState = data.WagerState switch
		{
			GameRecord.Handle.Common.Protos.WagerStates.Running => GameBets.Common.Enums.BetState.Running,
			GameRecord.Handle.Common.Protos.WagerStates.Void => GameBets.Common.Enums.BetState.Void,
			GameRecord.Handle.Common.Protos.WagerStates.Refund => GameBets.Common.Enums.BetState.Refund,
			GameRecord.Handle.Common.Protos.WagerStates.Reject => GameBets.Common.Enums.BetState.Cancel,
			GameRecord.Handle.Common.Protos.WagerStates.Lose => GameBets.Common.Enums.BetState.Loss,
			GameRecord.Handle.Common.Protos.WagerStates.Win => GameBets.Common.Enums.BetState.Win,
			GameRecord.Handle.Common.Protos.WagerStates.Draw => GameBets.Common.Enums.BetState.Draw,
			GameRecord.Handle.Common.Protos.WagerStates.HalfWin => GameBets.Common.Enums.BetState.HalfWin,
			GameRecord.Handle.Common.Protos.WagerStates.HalfLose => GameBets.Common.Enums.BetState.HalfLoss,
			_ => GameBets.Common.Enums.BetState.Waiting
		};

		var betType = data.WagerType switch
		{
			GameRecord.Handle.Common.Protos.WagerTypes.Normal => GameBets.Common.Enums.BetType.Normal,
			_ => GameBets.Common.Enums.BetType.Normal
		};

		var freeGameState = data.FreeGameState switch
		{
			GameRecord.Handle.Common.Protos.FreeGameStates.FreeGame => GameBets.Common.Enums.FreeGameState.FreeGame,
			_ => GameBets.Common.Enums.FreeGameState.None
		};

		var gambleState = data.GambleState switch
		{
			GameRecord.Handle.Common.Protos.GambleStates.Gamble => GameBets.Common.Enums.GambleState.Gamble,
			_ => GameBets.Common.Enums.GambleState.None
		};

		var cashoutState = data.CashoutState switch
		{
			GameRecord.Handle.Common.Protos.CashoutStates.Cashout => GameBets.Common.Enums.CashoutState.Cashout,
			_ => GameBets.Common.Enums.CashoutState.None
		};

		var resettleState = data.ResettleState switch
		{
			GameRecord.Handle.Common.Protos.ResettleStates.Resettle => GameBets.Common.Enums.ResettleState.Resettle,
			_ => GameBets.Common.Enums.ResettleState.None
		};

		var jackpotState = GameBets.Common.Enums.JackpotState.None;

		return dispatcher.PushCommandAsync(
			command: new GameBetRapidTestWorkerCommand(
				GameProviderId: data.GameProviderId.ToGuid(),
				GameProviderCode: data.GameProviderCode,
				SubGameProviderCode: data.SubGameProviderCode,
				GameProviderConfigurationId: data.GameProviderConfigurationId.ToGuid(),
				GameId: data.GameId,
				ResellerId: data.ResellerId.ToGuid(),
				ResellerCode: data.ResellerCode,
				LicenseeId: data.LicenseeId.ToGuid(),
				LicenseeCode: data.LicenseeCode,
				BrandId: data.BrandId.ToGuid(),
				BrandCode: data.BrandCode,
				PlayerCode: data.PlayerCode,
				Username: data.Username,
				CurrencyCode: data.CurrencyCode,
				WalletId: data.WalletId.ToGuid(),
				ExternalRoundId: data.ExternalRoundId,
				ExternalBetId: data.ExternalWagerId,
				ExternalGameId: data.ExternalGameId,
				ExternalCreatedOn: data.ExternalCreatedOn.ToDateTimeOffset(),
				ExternalClosedOn: data.ExternalClosedOn.ToDateTimeOffset(),
				ExternalPayoffOn: data.ExternalPayoffOn.ToDateTimeOffset(),
				ExternalResettleOn: data.ExternalResettleOn.ToDateTimeOffset(),
				BetState: betState,
				BetType: betType,
				FreeGameState: freeGameState,
				GambleState: gambleState,
				CashoutState: cashoutState,
				ResettleState: resettleState,
				JackpotState: jackpotState,
				CurrencyRatio: data.CurrencyRatio.ToDecimal().GetValueOrDefault(0.0m),
				Bet: data.Bet.ToDecimal().GetValueOrDefault(0.0m),
				ValidBet: data.ValidBet.ToDecimal().GetValueOrDefault(0.0m),
				Settle: data.Settle.ToDecimal().GetValueOrDefault(0.0m),
				Commission: data.Commission.ToDecimal().GetValueOrDefault(0.0m),
				Turnover: data.Turnover.ToDecimal().GetValueOrDefault(0.0m),
				WinLoss: data.WinLoss.ToDecimal().GetValueOrDefault(0.0m),
				Jackpot: 0.0m,
				AdditionalData: JsonNode.Parse(data.AdditionalData)!),
			cancellationToken: cancellationToken);
	}
}
