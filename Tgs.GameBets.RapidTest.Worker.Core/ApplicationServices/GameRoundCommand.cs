using System.Text.Json.Nodes;

namespace Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;
public record GameRoundCommand(
	Guid GameProviderId,
	Guid GameProviderConfigurationId,
	int GameId,
	Guid BrandId,
	string PlayerCode,
	Guid WalletId,
	string CurrencyCode,
	string ExternalBetId,
	DateTimeOffset ExternalCreatedOn,
	DateTimeOffset ExternalClosedOn,
	GameBets.Common.Enums.BetState BetState,
	decimal Bet,
	decimal ValidBet,
	decimal Settle,
	decimal Commission,
	decimal Turnover,
	decimal PlayerWinLoss,
	JsonNode AdditionalData);
