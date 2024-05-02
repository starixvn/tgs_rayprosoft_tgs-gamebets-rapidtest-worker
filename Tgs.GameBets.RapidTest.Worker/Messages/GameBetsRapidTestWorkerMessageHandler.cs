using System.Text.Json.Nodes;
using Tgs.GameBets.RapidTest.Worker.Core;
using Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

using Valhalla.MessageQueue;

namespace Tgs.GameBets.RapidTest.Worker.Messages;

public class GameBetsRapidTestWorkerMessageHandler(
	ILogger<GameBetsRapidTestWorkerMessageHandler> logger,
	IRapidTestDispatcher dispatcher) : IMessageHandler<Common.Protos.GameBetsRapidTestWorkerRequest>
{
	public ValueTask HandleAsync(string subject, Common.Protos.GameBetsRapidTestWorkerRequest data, IEnumerable<MessageHeaderValue>? headerValues, CancellationToken cancellationToken = default)
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
				data.ExternalBetId,
				ExternalCreatedOn = data.ExternalCreatedOn.ToDateTimeOffset()
			});

		var betState = data.BetState switch
		{
			Common.Protos.BetState.Running => GameBets.Common.Enums.BetState.Running,
			Common.Protos.BetState.Void => GameBets.Common.Enums.BetState.Void,
			Common.Protos.BetState.Refund => GameBets.Common.Enums.BetState.Refund,
			Common.Protos.BetState.Cancel => GameBets.Common.Enums.BetState.Cancel,
			Common.Protos.BetState.Loss => GameBets.Common.Enums.BetState.Loss,
			Common.Protos.BetState.Win => GameBets.Common.Enums.BetState.Win,
			Common.Protos.BetState.Draw => GameBets.Common.Enums.BetState.Draw,
			Common.Protos.BetState.HalfWin => GameBets.Common.Enums.BetState.HalfWin,
			Common.Protos.BetState.HalfLoss => GameBets.Common.Enums.BetState.HalfLoss,
			_ => GameBets.Common.Enums.BetState.Waiting
		};

		var betType = data.BetType switch
		{
			Common.Protos.BetType.Normal => GameBets.Common.Enums.BetType.Normal,
			_ => GameBets.Common.Enums.BetType.Normal
		};

		var freeGameState = data.FreeGameState switch
		{
			Common.Protos.FreeGameState.FreeGame => GameBets.Common.Enums.FreeGameState.FreeGame,
			_ => GameBets.Common.Enums.FreeGameState.None
		};

		var gambleState = data.GambleState switch
		{
			Common.Protos.GambleState.Gamble => GameBets.Common.Enums.GambleState.Gamble,
			_ => GameBets.Common.Enums.GambleState.None
		};

		var cashoutState = data.CashoutState switch
		{
			Common.Protos.CashoutState.Cashout => GameBets.Common.Enums.CashoutState.Cashout,
			_ => GameBets.Common.Enums.CashoutState.None
		};

		var resettleState = data.ResettleState switch
		{
			Common.Protos.ResettleState.Resettle => GameBets.Common.Enums.ResettleState.Resettle,
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
				ExternalBetId: data.ExternalBetId,
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
				Jackpot: data.Jackpot.ToDecimal().GetValueOrDefault(0.0m),
				AdditionalData: JsonNode.Parse(data.AdditionalData)!),
			cancellationToken: cancellationToken);
	}
}
