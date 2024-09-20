using System.Text.Json.Nodes;

namespace Tgs.GameBets.RapidTest.Worker.Core.Models;
public record GameRoundCommand(
	Guid Id,
	Guid GameProviderId,
	Guid GameProviderConfigurationId,
	int GameId,
	Guid BrandId,
	string PlayerCode,
	string CurrencyCode,
	Guid WalletId,
	string ExternalBetId,
	DateTimeOffset ExternalCreatedOn,
	DateTimeOffset ExternalClosedOn,
	GameBets.Common.Enums.BetState BetState,
	decimal Bet,
	decimal ValidBet,
	decimal Settle,
	decimal WinLoss,
	decimal Turnover,
	decimal Commission,
	JsonNode AdditionalData,
	DateTimeOffset CreatedOn);
