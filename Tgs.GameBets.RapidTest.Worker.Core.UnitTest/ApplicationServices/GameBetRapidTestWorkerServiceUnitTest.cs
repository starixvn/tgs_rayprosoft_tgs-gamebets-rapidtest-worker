using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

namespace Tgs.GameBets.RapidTest.Worker.Core.UnitTest.ApplicationServices;

public class GameBetRapidTestWorkerServiceUnitTest
{
	[Fact]
	public async Task Update_Is_True()
	{
		var fakeLogger = NullLogger<GameBetRapidTestWorkerService>.Instance;
		var fakeGameBetsRapidTestRepository = Substitute.For<IGameBetsRapidTestRepository>();
		var fakeGameBetMajorRepository = Substitute.For<IGameBetMajorRepository>();
		var fakeGameRoundsRepository = Substitute.For<IGameRoundsRepository>();
		var fakeGameBetsWorkerRepository = Substitute.For<IGameBetsWorkerRepository>();

		var request = new GameBetRapidTestWorkerRequest(
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
			WinLoss: 4.0m,
			Turnover: 4.0m,
			Commission: 0.0m,
			Contribute: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeGameBetsRapidTestRepository.CommandAsync(
			command: Arg.Is<Models.GameBetRapidTestCommand>(compare =>
				compare.GameProviderCode == request.GameProviderCode &&
				compare.SubGameProviderCode == request.SubGameProviderCode &&
				compare.GameId == request.GameId &&
				compare.ResellerCode == request.ResellerCode &&
				compare.LicenseeCode == request.LicenseeCode &&
				compare.BrandCode == request.BrandCode &&
				compare.PlayerCode == request.PlayerCode &&
				compare.Username == request.Username &&
				compare.CurrencyCode == request.CurrencyCode &&
				compare.WalletId == request.WalletId &&
				compare.ExternalRoundId == request.ExternalRoundId &&
				compare.ExternalBetId == request.ExternalBetId &&
				compare.ExternalGameId == request.ExternalGameId &&
				compare.BetState == request.BetState &&
				compare.BetType == request.BetType &&
				compare.FreeGameState == request.FreeGameState &&
				compare.GambleState == request.GambleState &&
				compare.CashoutState == request.CashoutState &&
				compare.ResettleState == request.ResettleState &&
				compare.JackpotState == request.JackpotState &&
				compare.CurrencyRatio == request.CurrencyRatio &&
				compare.Bet == request.Bet &&
				compare.ValidBet == request.ValidBet &&
				compare.Settle == request.Settle &&
				compare.WinLoss == request.WinLoss &&
				compare.Turnover == request.Turnover &&
				compare.Commission == request.Commission &&
				compare.Jackpot == request.Jackpot &&
				compare.AdditionalData != null),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var id = Guid.NewGuid();
		var currentTime = DateTimeOffset.Parse("2023-08-08 12:35:56.000 +0800");
		var gameBetMajorInfo = new GameBets.Common.ViewModels.GameBetInfoViewModel(
			Id: id,
			GameProviderId: request.GameProviderId,
			GameProviderConfigurationId: request.GameProviderConfigurationId,
			GameId: request.GameId,
			ResellerId: request.ResellerId,
			LicenseeId: request.LicenseeId,
			BrandId: request.BrandId,
			PlayerCode: request.PlayerCode,
			CurrencyCode: request.CurrencyCode,
			WalletId: request.WalletId,
			ExternalRoundId: request.ExternalRoundId,
			ExternalBetId: request.ExternalBetId,
			ExternalCreatedOn: request.ExternalCreatedOn,
			ExternalClosedOn: request.ExternalClosedOn,
			ExternalPayoffOn: request.ExternalPayoffOn,
			ExternalResettleOn: request.ExternalResettleOn,
			BetState: request.BetState,
			BetType: request.BetType,
			FreeGameState: request.FreeGameState,
			GambleState: request.GambleState,
			CashoutState: request.CashoutState,
			ResettleState: request.ResettleState,
			JackpotState: request.JackpotState,
			CurrencyRatio: request.CurrencyRatio,
			Bet: request.Bet,
			ValidBet: request.ValidBet,
			Settle: request.Settle,
			WinLoss: request.WinLoss,
			Turnover: request.Turnover,
			Commission: request.Commission,
			Contribute: request.Contribute,
			Jackpot: request.Jackpot,
			CreatedOn: currentTime,
			ClosedOn: currentTime,
			UpdatedOn: currentTime,
			ResetState: GameBets.Common.Enums.ResetState.None,
			ResetOn: DateTimeOffset.UnixEpoch,
			AdditionalData: request.AdditionalData,
			ExtendedData: new JsonObject { });
		_ = fakeGameBetMajorRepository.CommandAsync(
			command: Arg.Is<Models.GameBetMajorCommand>(compare =>
				compare.GameProviderId == request.GameProviderId &&
				compare.GameProviderConfigurationId == request.GameProviderConfigurationId &&
				compare.GameId == request.GameId &&
				compare.ResellerId == request.ResellerId &&
				compare.LicenseeId == request.LicenseeId &&
				compare.BrandId == request.BrandId &&
				compare.PlayerCode == request.PlayerCode &&
				compare.CurrencyCode == request.CurrencyCode &&
				compare.WalletId == request.WalletId &&
				compare.ExternalRoundId == request.ExternalRoundId &&
				compare.ExternalBetId == request.ExternalBetId &&
				compare.ExternalCreatedOn == request.ExternalCreatedOn &&
				compare.ExternalClosedOn == request.ExternalClosedOn &&
				compare.ExternalPayoffOn == request.ExternalPayoffOn &&
				compare.ExternalResettleOn == request.ExternalResettleOn &&
				compare.BetState == request.BetState &&
				compare.BetType == request.BetType &&
				compare.FreeGameState == request.FreeGameState &&
				compare.GambleState == request.GambleState &&
				compare.CashoutState == request.CashoutState &&
				compare.ResettleState == request.ResettleState &&
				compare.JackpotState == request.JackpotState &&
				compare.CurrencyRatio == request.CurrencyRatio &&
				compare.Bet == request.Bet &&
				compare.ValidBet == request.ValidBet &&
				compare.Settle == request.Settle &&
				compare.WinLoss == request.WinLoss &&
				compare.Turnover == request.Turnover &&
				compare.Commission == request.Commission &&
				compare.Contribute == request.Contribute &&
				compare.Jackpot == request.Jackpot &&
				compare.AdditionalData != null),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(gameBetMajorInfo);

		_ = fakeGameRoundsRepository.CommandAsync(
			command: Arg.Is<Models.GameRoundCommand>(compare =>
				compare.Id == gameBetMajorInfo.Id &&
				compare.GameProviderId == gameBetMajorInfo.GameProviderId &&
				compare.GameProviderConfigurationId == gameBetMajorInfo.GameProviderConfigurationId &&
				compare.GameId == gameBetMajorInfo.GameId &&
				compare.BrandId == gameBetMajorInfo.BrandId &&
				compare.PlayerCode == gameBetMajorInfo.PlayerCode &&
				compare.CurrencyCode == gameBetMajorInfo.CurrencyCode &&
				compare.WalletId == gameBetMajorInfo.WalletId &&
				compare.ExternalBetId == gameBetMajorInfo.ExternalBetId &&
				compare.ExternalCreatedOn == gameBetMajorInfo.ExternalCreatedOn &&
				compare.ExternalClosedOn == gameBetMajorInfo.ExternalClosedOn &&
				compare.BetState == gameBetMajorInfo.BetState &&
				compare.Bet == gameBetMajorInfo.Bet &&
				compare.ValidBet == gameBetMajorInfo.ValidBet &&
				compare.Settle == gameBetMajorInfo.Settle &&
				compare.WinLoss == gameBetMajorInfo.WinLoss &&
				compare.Turnover == gameBetMajorInfo.Turnover &&
				compare.Commission == gameBetMajorInfo.Commission &&
				compare.AdditionalData != null &&
				compare.CreatedOn == gameBetMajorInfo.CreatedOn),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameBetRapidTestWorkerService(
			fakeLogger,
			fakeGameBetsRapidTestRepository,
			fakeGameBetMajorRepository,
			fakeGameRoundsRepository,
			fakeGameBetsWorkerRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		await sut.Handle(request, token);

		_ = fakeGameBetsWorkerRepository
			.Received(1)
			.CommandAsync(
			command: Arg.Is<Models.GameBetWorkCommand>(compare =>
				compare.Id == id &&
				compare.GameProviderId == gameBetMajorInfo.GameProviderId &&
				compare.GameProviderConfigurationId == gameBetMajorInfo.GameProviderConfigurationId &&
				compare.GameId == gameBetMajorInfo.GameId &&
				compare.ResellerId == gameBetMajorInfo.ResellerId &&
				compare.LicenseeId == gameBetMajorInfo.LicenseeId &&
				compare.BrandId == gameBetMajorInfo.BrandId &&
				compare.PlayerCode == gameBetMajorInfo.PlayerCode &&
				compare.CurrencyCode == gameBetMajorInfo.CurrencyCode &&
				compare.WalletId == gameBetMajorInfo.WalletId &&
				compare.ExternalRoundId == gameBetMajorInfo.ExternalRoundId &&
				compare.ExternalBetId == gameBetMajorInfo.ExternalBetId &&
				compare.ExternalCreatedOn == gameBetMajorInfo.ExternalCreatedOn &&
				compare.ExternalClosedOn == gameBetMajorInfo.ExternalClosedOn &&
				compare.ExternalPayoffOn == gameBetMajorInfo.ExternalPayoffOn &&
				compare.ExternalResettleOn == gameBetMajorInfo.ExternalResettleOn &&
				compare.BetState == gameBetMajorInfo.BetState &&
				compare.BetType == gameBetMajorInfo.BetType &&
				compare.FreeGameState == gameBetMajorInfo.FreeGameState &&
				compare.GambleState == gameBetMajorInfo.GambleState &&
				compare.CashoutState == gameBetMajorInfo.CashoutState &&
				compare.ResettleState == gameBetMajorInfo.ResettleState &&
				compare.JackpotState == gameBetMajorInfo.JackpotState &&
				compare.CurrencyRatio == gameBetMajorInfo.CurrencyRatio &&
				compare.Bet == gameBetMajorInfo.Bet &&
				compare.ValidBet == gameBetMajorInfo.ValidBet &&
				compare.Settle == gameBetMajorInfo.Settle &&
				compare.WinLoss == gameBetMajorInfo.WinLoss &&
				compare.Turnover == gameBetMajorInfo.Turnover &&
				compare.Commission == gameBetMajorInfo.Commission &&
				compare.Contribute == gameBetMajorInfo.Contribute &&
				compare.Jackpot == gameBetMajorInfo.Jackpot &&
				compare.AdditionalData != null),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task Update_Is_False()
	{
		var fakeLogger = NullLogger<GameBetRapidTestWorkerService>.Instance;
		var fakeGameBetsRapidTestRepository = Substitute.For<IGameBetsRapidTestRepository>();
		var fakeGameBetMajorRepository = Substitute.For<IGameBetMajorRepository>();
		var fakeGameRoundsRepository = Substitute.For<IGameRoundsRepository>();
		var fakeGameBetsWorkerRepository = Substitute.For<IGameBetsWorkerRepository>();

		var request = new GameBetRapidTestWorkerRequest(
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
			WinLoss: 4.0m,
			Turnover: 4.0m,
			Commission: 0.0m,
			Contribute: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeGameBetsRapidTestRepository.CommandAsync(
			command: Arg.Is<Models.GameBetRapidTestCommand>(compare =>
				compare.GameProviderCode == request.GameProviderCode &&
				compare.SubGameProviderCode == request.SubGameProviderCode &&
				compare.GameId == request.GameId &&
				compare.ResellerCode == request.ResellerCode &&
				compare.LicenseeCode == request.LicenseeCode &&
				compare.BrandCode == request.BrandCode &&
				compare.PlayerCode == request.PlayerCode &&
				compare.Username == request.Username &&
				compare.CurrencyCode == request.CurrencyCode &&
				compare.WalletId == request.WalletId &&
				compare.ExternalRoundId == request.ExternalRoundId &&
				compare.ExternalBetId == request.ExternalBetId &&
				compare.ExternalGameId == request.ExternalGameId &&
				compare.BetState == request.BetState &&
				compare.BetType == request.BetType &&
				compare.FreeGameState == request.FreeGameState &&
				compare.GambleState == request.GambleState &&
				compare.CashoutState == request.CashoutState &&
				compare.ResettleState == request.ResettleState &&
				compare.JackpotState == request.JackpotState &&
				compare.CurrencyRatio == request.CurrencyRatio &&
				compare.Bet == request.Bet &&
				compare.ValidBet == request.ValidBet &&
				compare.Settle == request.Settle &&
				compare.WinLoss == request.WinLoss &&
				compare.Turnover == request.Turnover &&
				compare.Commission == request.Commission &&
				compare.Jackpot == request.Jackpot &&
				compare.AdditionalData != null),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(false);

		var sut = new GameBetRapidTestWorkerService(
			fakeLogger,
			fakeGameBetsRapidTestRepository,
			fakeGameBetMajorRepository,
			fakeGameRoundsRepository,
			fakeGameBetsWorkerRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		await sut.Handle(request, token);

		_ = fakeGameBetMajorRepository
			.Received(0)
			.CommandAsync(
			command: Arg.Any<Models.GameBetMajorCommand>(),
			cancellationToken: Arg.Any<CancellationToken>());

		_ = fakeGameRoundsRepository
			.Received(0)
			.CommandAsync(
			command: Arg.Any<Models.GameRoundCommand>(),
			cancellationToken: Arg.Any<CancellationToken>());

		_ = fakeGameBetsWorkerRepository
			.Received(0)
			.CommandAsync(
			command: Arg.Any<Models.GameBetWorkCommand>(),
			cancellationToken: Arg.Any<CancellationToken>());
	}
}
