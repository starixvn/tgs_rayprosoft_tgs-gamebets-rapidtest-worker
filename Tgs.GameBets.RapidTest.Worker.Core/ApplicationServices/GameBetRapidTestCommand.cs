using System.Text.Json.Nodes;

namespace Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

public record GameBetRapidTestCommand(
	string GameProviderCode,
	string SubGameProviderCode,
	int GameId,
	string ResellerCode,
	string LicenseeCode,
	string BrandCode,
	string PlayerCode,
	string Username,
	string CurrencyCode,
	Guid WalletId,
	string ExternalRoundId,
	string ExternalBetId,
	string ExternalGameId,
	GameBets.Common.Enums.BetState BetState,
	GameBets.Common.Enums.BetType BetType,
	GameBets.Common.Enums.FreeGameState FreeGameState,
	GameBets.Common.Enums.GambleState GambleState,
	GameBets.Common.Enums.CashoutState CashoutState,
	GameBets.Common.Enums.ResettleState ResettleState,
	GameBets.Common.Enums.JackpotState JackpotState,
	decimal CurrencyRatio,
	decimal Bet,
	decimal ValidBet,
	decimal Settle,
	decimal Commission,
	decimal Turnover,
	decimal WinLoss,
	decimal Jackpot,
	JsonNode AdditionalData);
