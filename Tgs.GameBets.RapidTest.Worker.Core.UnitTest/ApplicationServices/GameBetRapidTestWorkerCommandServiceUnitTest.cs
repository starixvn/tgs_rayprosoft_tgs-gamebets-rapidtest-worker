using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Tgs.ServiceUtility;
using Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

namespace Tgs.GameBets.RapidTest.Worker.Core.UnitTest.ApplicationServices;

public class GameBetRapidTestWorkerCommandServiceUnitTest
{
	[Fact]
	public async Task Update_Is_True()
	{
		var faceLogger = NullLogger<GameBetRapidTestWorkerCommandService>.Instance;
		var fakeGameBetRapidTestService = Substitute.For<IGetService<GameBetRapidTestCommand, bool>>();
		var fakeGameRoundService = Substitute.For<IGetService<GameRoundCommand, Guid>>();
		var fakeGameBetService = Substitute.For<ICommandService<GameBetCommand>>();

		var command = new GameBetRapidTestWorkerCommand(
			GameProviderId: Guid.NewGuid(),
			GameProviderCode: "KY",
			GameProviderConfigurationId: Guid.NewGuid(),
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			ResellerCode: "TGS",
			LicenseeId: Guid.NewGuid(),
			LicenseeCode: "TGS",
			BrandId: Guid.NewGuid(),
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			ExternalPayoffOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			ExternalResettleOn: DateTimeOffset.UnixEpoch,
			BetState: GameBets.Common.Enums.BetState.Win,
			BetType: GameBets.Common.Enums.BetType.Normal,
			FreeGameState: GameBets.Common.Enums.FreeGameState.None,
			GambleState: GameBets.Common.Enums.GambleState.None,
			CashoutState: GameBets.Common.Enums.CashoutState.None,
			ResettleState: GameBets.Common.Enums.ResettleState.None,
			JackpotState: GameBets.Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 8.0m,
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: 4.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeGameBetRapidTestService.GetAsync(
			query: Arg.Is(new GameBetRapidTestCommand(
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
				BetState: command.BetState,
				BetType: command.BetType,
				FreeGameState: command.FreeGameState,
				GambleState: command.GambleState,
				CashoutState: command.CashoutState,
				ResettleState: command.ResettleState,
				JackpotState: command.JackpotState,
				CurrencyRatio: command.CurrencyRatio,
				Bet: command.Bet,
				ValidBet: command.ValidBet,
				Settle: command.Settle,
				Commission: command.Commission,
				Turnover: command.Turnover,
				WinLoss: command.WinLoss,
				Jackpot: command.Jackpot,
				AdditionalData: command.AdditionalData)),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var id = Guid.NewGuid();
		_ = fakeGameRoundService.GetAsync(
			query: Arg.Is(new GameRoundCommand(
				GameProviderId: command.GameProviderId,
				GameProviderConfigurationId: command.GameProviderConfigurationId,
				GameId: command.GameId,
				BrandId: command.BrandId,
				PlayerCode: command.PlayerCode,
				CurrencyCode: command.CurrencyCode,
				WalletId: command.WalletId,
				ExternalBetId: command.ExternalBetId,
				ExternalCreatedOn: command.ExternalCreatedOn,
				ExternalClosedOn: command.ExternalClosedOn,
				BetState: command.BetState,
				Bet: command.Bet,
				ValidBet: command.ValidBet,
				Settle: command.Settle,
				Commission: command.Commission,
				Turnover: command.Turnover,
				PlayerWinLoss: command.WinLoss,
				AdditionalData: command.AdditionalData)),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameBetRapidTestWorkerCommandService(
			faceLogger,
			fakeGameBetRapidTestService,
			fakeGameRoundService,
			fakeGameBetService);

		await sut.ExecuteAsync(command: command);

		_ = fakeGameBetService
			.Received(1)
			.ExecuteAsync(
			command: Arg.Is(new GameBetCommand(
				Id: id,
				GameProviderId: command.GameProviderId,
				GameProviderConfigurationId: command.GameProviderConfigurationId,
				GameId: command.GameId,
				ResellerId: command.ResellerId,
				LicenseeId: command.LicenseeId,
				BrandId: command.BrandId,
				PlayerCode: command.PlayerCode,
				CurrencyCode: command.CurrencyCode,
				WalletId: command.WalletId,
				ExternalRoundId: command.ExternalRoundId,
				ExternalBetId: command.ExternalBetId,
				ExternalCreatedOn: command.ExternalCreatedOn,
				ExternalClosedOn: command.ExternalClosedOn,
				ExternalPayoffOn: command.ExternalPayoffOn,
				ExternalResettleOn: command.ExternalResettleOn,
				BetState: command.BetState,
				BetType: command.BetType,
				FreeGameState: command.FreeGameState,
				GambleState: command.GambleState,
				CashoutState: command.CashoutState,
				ResettleState: command.ResettleState,
				JackpotState: command.JackpotState,
				CurrencyRatio: command.CurrencyRatio,
				Bet: command.Bet,
				ValidBet: command.ValidBet,
				Settle: command.Settle,
				Commission: command.Commission,
				Turnover: command.Turnover,
				WinLoss: command.WinLoss,
				Jackpot: command.Jackpot,
				AdditionalData: command.AdditionalData)),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task Update_Is_False()
	{
		var faceLogger = NullLogger<GameBetRapidTestWorkerCommandService>.Instance;
		var fakeGameBetRapidTestService = Substitute.For<IGetService<GameBetRapidTestCommand, bool>>();
		var fakeGameRoundService = Substitute.For<IGetService<GameRoundCommand, Guid>>();
		var fakeGameBetService = Substitute.For<ICommandService<GameBetCommand>>();

		var command = new GameBetRapidTestWorkerCommand(
			GameProviderId: Guid.NewGuid(),
			GameProviderCode: "KY",
			GameProviderConfigurationId: Guid.NewGuid(),
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			ResellerCode: "TGS",
			LicenseeId: Guid.NewGuid(),
			LicenseeCode: "TGS",
			BrandId: Guid.NewGuid(),
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			ExternalPayoffOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			ExternalResettleOn: DateTimeOffset.UnixEpoch,
			BetState: GameBets.Common.Enums.BetState.Win,
			BetType: GameBets.Common.Enums.BetType.Normal,
			FreeGameState: GameBets.Common.Enums.FreeGameState.None,
			GambleState: GameBets.Common.Enums.GambleState.None,
			CashoutState: GameBets.Common.Enums.CashoutState.None,
			ResettleState: GameBets.Common.Enums.ResettleState.None,
			JackpotState: GameBets.Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 8.0m,
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: 4.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeGameBetRapidTestService.GetAsync(
			query: Arg.Is(new GameBetRapidTestCommand(
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
				BetState: command.BetState,
				BetType: command.BetType,
				FreeGameState: command.FreeGameState,
				GambleState: command.GambleState,
				CashoutState: command.CashoutState,
				ResettleState: command.ResettleState,
				JackpotState: command.JackpotState,
				CurrencyRatio: command.CurrencyRatio,
				Bet: command.Bet,
				ValidBet: command.ValidBet,
				Settle: command.Settle,
				Commission: command.Commission,
				Turnover: command.Turnover,
				WinLoss: command.WinLoss,
				Jackpot: command.Jackpot,
				AdditionalData: command.AdditionalData)),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(false);

		var sut = new GameBetRapidTestWorkerCommandService(
			faceLogger,
			fakeGameBetRapidTestService,
			fakeGameRoundService,
			fakeGameBetService);

		await sut.ExecuteAsync(command: command);

		_ = fakeGameRoundService
			.Received(0)
			.GetAsync(
			query: Arg.Any<GameRoundCommand>(),
			cancellationToken: Arg.Any<CancellationToken>());

		_ = fakeGameBetService
			.Received(0)
			.ExecuteAsync(
			command: Arg.Any<GameBetCommand>(),
			cancellationToken: Arg.Any<CancellationToken>());
	}
}
