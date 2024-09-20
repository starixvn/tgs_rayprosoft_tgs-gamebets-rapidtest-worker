using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Tgs.GameBets.RapidTest.Client;
using Tgs.GameBets.RapidTest.Worker.Core;
using Tgs.GameBets.RapidTest.Worker.Core.Models;

namespace Tgs.GameBets.RapidTest.Worker.Repositories.UnitTest;

public class GameBetsRapidTestRepositoryUnitTest
{
	[Fact]
	public async Task BetState_Is_Waiting()
	{
		var faceLogger = NullLogger<GameBetsRapidTestRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var command = new GameBetRapidTestCommand(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "Demo",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: GameBets.Common.Enums.BetState.Waiting,
			BetType: GameBets.Common.Enums.BetType.Normal,
			FreeGameState: GameBets.Common.Enums.FreeGameState.None,
			GambleState: GameBets.Common.Enums.GambleState.None,
			CashoutState: GameBets.Common.Enums.CashoutState.None,
			ResettleState: GameBets.Common.Enums.ResettleState.None,
			JackpotState: GameBets.Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 0.0m,
			WinLoss: 0.0m,
			Turnover: 0.0m,
			Commission: 0.0m,
			Contribute: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new RapidTest.Common.ViewModels.GameBetRapidTestCommandViewModel(
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
			BetState: RapidTest.Common.Enums.BetState.Waiting,
			BetType: RapidTest.Common.Enums.BetType.Normal,
			FreeGameState: RapidTest.Common.Enums.FreeGameState.None,
			GambleState: RapidTest.Common.Enums.GambleState.None,
			CashoutState: RapidTest.Common.Enums.CashoutState.None,
			ResettleState: RapidTest.Common.Enums.ResettleState.None,
			JackpotState: RapidTest.Common.Enums.JackpotState.None,
			CurrencyRatio: command.CurrencyRatio,
			Bet: command.Bet,
			ValidBet: command.ValidBet,
			Settle: command.Settle,
			Commission: command.Commission,
			Turnover: command.Turnover,
			WinLoss: command.WinLoss,
			Jackpot: command.Jackpot,
			AdditionalData: command.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetsRapidTestRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_Running()
	{
		var faceLogger = NullLogger<GameBetsRapidTestRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var command = new GameBetRapidTestCommand(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "Demo",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: GameBets.Common.Enums.BetState.Running,
			BetType: GameBets.Common.Enums.BetType.Normal,
			FreeGameState: GameBets.Common.Enums.FreeGameState.None,
			GambleState: GameBets.Common.Enums.GambleState.None,
			CashoutState: GameBets.Common.Enums.CashoutState.None,
			ResettleState: GameBets.Common.Enums.ResettleState.None,
			JackpotState: GameBets.Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 0.0m,
			WinLoss: 0.0m,
			Turnover: 0.0m,
			Commission: 0.0m,
			Contribute: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new RapidTest.Common.ViewModels.GameBetRapidTestCommandViewModel(
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
			BetState: RapidTest.Common.Enums.BetState.Running,
			BetType: RapidTest.Common.Enums.BetType.Normal,
			FreeGameState: RapidTest.Common.Enums.FreeGameState.None,
			GambleState: RapidTest.Common.Enums.GambleState.None,
			CashoutState: RapidTest.Common.Enums.CashoutState.None,
			ResettleState: RapidTest.Common.Enums.ResettleState.None,
			JackpotState: RapidTest.Common.Enums.JackpotState.None,
			CurrencyRatio: command.CurrencyRatio,
			Bet: command.Bet,
			ValidBet: command.ValidBet,
			Settle: command.Settle,
			Commission: command.Commission,
			Turnover: command.Turnover,
			WinLoss: command.WinLoss,
			Jackpot: command.Jackpot,
			AdditionalData: command.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetsRapidTestRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_Void()
	{
		var faceLogger = NullLogger<GameBetsRapidTestRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var command = new GameBetRapidTestCommand(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "Demo",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: GameBets.Common.Enums.BetState.Void,
			BetType: GameBets.Common.Enums.BetType.Normal,
			FreeGameState: GameBets.Common.Enums.FreeGameState.None,
			GambleState: GameBets.Common.Enums.GambleState.None,
			CashoutState: GameBets.Common.Enums.CashoutState.None,
			ResettleState: GameBets.Common.Enums.ResettleState.None,
			JackpotState: GameBets.Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 0.0m,
			WinLoss: 0.0m,
			Turnover: 0.0m,
			Commission: 0.0m,
			Contribute: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new RapidTest.Common.ViewModels.GameBetRapidTestCommandViewModel(
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
			BetState: RapidTest.Common.Enums.BetState.Void,
			BetType: RapidTest.Common.Enums.BetType.Normal,
			FreeGameState: RapidTest.Common.Enums.FreeGameState.None,
			GambleState: RapidTest.Common.Enums.GambleState.None,
			CashoutState: RapidTest.Common.Enums.CashoutState.None,
			ResettleState: RapidTest.Common.Enums.ResettleState.None,
			JackpotState: RapidTest.Common.Enums.JackpotState.None,
			CurrencyRatio: command.CurrencyRatio,
			Bet: command.Bet,
			ValidBet: command.ValidBet,
			Settle: command.Settle,
			Commission: command.Commission,
			Turnover: command.Turnover,
			WinLoss: command.WinLoss,
			Jackpot: command.Jackpot,
			AdditionalData: command.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetsRapidTestRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_Refund()
	{
		var faceLogger = NullLogger<GameBetsRapidTestRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var command = new GameBetRapidTestCommand(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "Demo",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: GameBets.Common.Enums.BetState.Refund,
			BetType: GameBets.Common.Enums.BetType.Normal,
			FreeGameState: GameBets.Common.Enums.FreeGameState.None,
			GambleState: GameBets.Common.Enums.GambleState.None,
			CashoutState: GameBets.Common.Enums.CashoutState.None,
			ResettleState: GameBets.Common.Enums.ResettleState.None,
			JackpotState: GameBets.Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 0.0m,
			WinLoss: 0.0m,
			Turnover: 0.0m,
			Commission: 0.0m,
			Contribute: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new RapidTest.Common.ViewModels.GameBetRapidTestCommandViewModel(
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
			BetState: RapidTest.Common.Enums.BetState.Refund,
			BetType: RapidTest.Common.Enums.BetType.Normal,
			FreeGameState: RapidTest.Common.Enums.FreeGameState.None,
			GambleState: RapidTest.Common.Enums.GambleState.None,
			CashoutState: RapidTest.Common.Enums.CashoutState.None,
			ResettleState: RapidTest.Common.Enums.ResettleState.None,
			JackpotState: RapidTest.Common.Enums.JackpotState.None,
			CurrencyRatio: command.CurrencyRatio,
			Bet: command.Bet,
			ValidBet: command.ValidBet,
			Settle: command.Settle,
			Commission: command.Commission,
			Turnover: command.Turnover,
			WinLoss: command.WinLoss,
			Jackpot: command.Jackpot,
			AdditionalData: command.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetsRapidTestRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_Cancel()
	{
		var faceLogger = NullLogger<GameBetsRapidTestRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var command = new GameBetRapidTestCommand(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "Demo",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: GameBets.Common.Enums.BetState.Cancel,
			BetType: GameBets.Common.Enums.BetType.Normal,
			FreeGameState: GameBets.Common.Enums.FreeGameState.None,
			GambleState: GameBets.Common.Enums.GambleState.None,
			CashoutState: GameBets.Common.Enums.CashoutState.None,
			ResettleState: GameBets.Common.Enums.ResettleState.None,
			JackpotState: GameBets.Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 0.0m,
			WinLoss: 0.0m,
			Turnover: 0.0m,
			Commission: 0.0m,
			Contribute: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new RapidTest.Common.ViewModels.GameBetRapidTestCommandViewModel(
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
			BetState: RapidTest.Common.Enums.BetState.Cancel,
			BetType: RapidTest.Common.Enums.BetType.Normal,
			FreeGameState: RapidTest.Common.Enums.FreeGameState.None,
			GambleState: RapidTest.Common.Enums.GambleState.None,
			CashoutState: RapidTest.Common.Enums.CashoutState.None,
			ResettleState: RapidTest.Common.Enums.ResettleState.None,
			JackpotState: RapidTest.Common.Enums.JackpotState.None,
			CurrencyRatio: command.CurrencyRatio,
			Bet: command.Bet,
			ValidBet: command.ValidBet,
			Settle: command.Settle,
			Commission: command.Commission,
			Turnover: command.Turnover,
			WinLoss: command.WinLoss,
			Jackpot: command.Jackpot,
			AdditionalData: command.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetsRapidTestRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_Win()
	{
		var faceLogger = NullLogger<GameBetsRapidTestRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var command = new GameBetRapidTestCommand(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "Demo",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
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

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new RapidTest.Common.ViewModels.GameBetRapidTestCommandViewModel(
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
			BetState: RapidTest.Common.Enums.BetState.Win,
			BetType: RapidTest.Common.Enums.BetType.Normal,
			FreeGameState: RapidTest.Common.Enums.FreeGameState.None,
			GambleState: RapidTest.Common.Enums.GambleState.None,
			CashoutState: RapidTest.Common.Enums.CashoutState.None,
			ResettleState: RapidTest.Common.Enums.ResettleState.None,
			JackpotState: RapidTest.Common.Enums.JackpotState.None,
			CurrencyRatio: command.CurrencyRatio,
			Bet: command.Bet,
			ValidBet: command.ValidBet,
			Settle: command.Settle,
			Commission: command.Commission,
			Turnover: command.Turnover,
			WinLoss: command.WinLoss,
			Jackpot: command.Jackpot,
			AdditionalData: command.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetsRapidTestRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_Loss()
	{
		var faceLogger = NullLogger<GameBetsRapidTestRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var command = new GameBetRapidTestCommand(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "Demo",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: GameBets.Common.Enums.BetState.Loss,
			BetType: GameBets.Common.Enums.BetType.Normal,
			FreeGameState: GameBets.Common.Enums.FreeGameState.None,
			GambleState: GameBets.Common.Enums.GambleState.None,
			CashoutState: GameBets.Common.Enums.CashoutState.None,
			ResettleState: GameBets.Common.Enums.ResettleState.None,
			JackpotState: GameBets.Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 0.0m,
			WinLoss: -4.0m,
			Turnover: 4.0m,
			Commission: 0.0m,
			Contribute: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new RapidTest.Common.ViewModels.GameBetRapidTestCommandViewModel(
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
			BetState: RapidTest.Common.Enums.BetState.Loss,
			BetType: RapidTest.Common.Enums.BetType.Normal,
			FreeGameState: RapidTest.Common.Enums.FreeGameState.None,
			GambleState: RapidTest.Common.Enums.GambleState.None,
			CashoutState: RapidTest.Common.Enums.CashoutState.None,
			ResettleState: RapidTest.Common.Enums.ResettleState.None,
			JackpotState: RapidTest.Common.Enums.JackpotState.None,
			CurrencyRatio: command.CurrencyRatio,
			Bet: command.Bet,
			ValidBet: command.ValidBet,
			Settle: command.Settle,
			Commission: command.Commission,
			Turnover: command.Turnover,
			WinLoss: command.WinLoss,
			Jackpot: command.Jackpot,
			AdditionalData: command.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetsRapidTestRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_Draw()
	{
		var faceLogger = NullLogger<GameBetsRapidTestRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var command = new GameBetRapidTestCommand(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "Demo",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: GameBets.Common.Enums.BetState.Draw,
			BetType: GameBets.Common.Enums.BetType.Normal,
			FreeGameState: GameBets.Common.Enums.FreeGameState.None,
			GambleState: GameBets.Common.Enums.GambleState.None,
			CashoutState: GameBets.Common.Enums.CashoutState.None,
			ResettleState: GameBets.Common.Enums.ResettleState.None,
			JackpotState: GameBets.Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 4.0m,
			WinLoss: 0.0m,
			Turnover: 0.0m,
			Commission: 0.0m,
			Contribute: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new RapidTest.Common.ViewModels.GameBetRapidTestCommandViewModel(
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
			BetState: RapidTest.Common.Enums.BetState.Draw,
			BetType: RapidTest.Common.Enums.BetType.Normal,
			FreeGameState: RapidTest.Common.Enums.FreeGameState.None,
			GambleState: RapidTest.Common.Enums.GambleState.None,
			CashoutState: RapidTest.Common.Enums.CashoutState.None,
			ResettleState: RapidTest.Common.Enums.ResettleState.None,
			JackpotState: RapidTest.Common.Enums.JackpotState.None,
			CurrencyRatio: command.CurrencyRatio,
			Bet: command.Bet,
			ValidBet: command.ValidBet,
			Settle: command.Settle,
			Commission: command.Commission,
			Turnover: command.Turnover,
			WinLoss: command.WinLoss,
			Jackpot: command.Jackpot,
			AdditionalData: command.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetsRapidTestRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_HalfWin()
	{
		var faceLogger = NullLogger<GameBetsRapidTestRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var command = new GameBetRapidTestCommand(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "Demo",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: GameBets.Common.Enums.BetState.HalfWin,
			BetType: GameBets.Common.Enums.BetType.Normal,
			FreeGameState: GameBets.Common.Enums.FreeGameState.None,
			GambleState: GameBets.Common.Enums.GambleState.None,
			CashoutState: GameBets.Common.Enums.CashoutState.None,
			ResettleState: GameBets.Common.Enums.ResettleState.None,
			JackpotState: GameBets.Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 2.0m,
			WinLoss: -2.0m,
			Turnover: 2.0m,
			Commission: 0.0m,
			Contribute: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new RapidTest.Common.ViewModels.GameBetRapidTestCommandViewModel(
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
			BetState: RapidTest.Common.Enums.BetState.HalfWin,
			BetType: RapidTest.Common.Enums.BetType.Normal,
			FreeGameState: RapidTest.Common.Enums.FreeGameState.None,
			GambleState: RapidTest.Common.Enums.GambleState.None,
			CashoutState: RapidTest.Common.Enums.CashoutState.None,
			ResettleState: RapidTest.Common.Enums.ResettleState.None,
			JackpotState: RapidTest.Common.Enums.JackpotState.None,
			CurrencyRatio: command.CurrencyRatio,
			Bet: command.Bet,
			ValidBet: command.ValidBet,
			Settle: command.Settle,
			Commission: command.Commission,
			Turnover: command.Turnover,
			WinLoss: command.WinLoss,
			Jackpot: command.Jackpot,
			AdditionalData: command.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetsRapidTestRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_HalfLoss()
	{
		var faceLogger = NullLogger<GameBetsRapidTestRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var command = new GameBetRapidTestCommand(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "Demo",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: GameBets.Common.Enums.BetState.HalfLoss,
			BetType: GameBets.Common.Enums.BetType.Normal,
			FreeGameState: GameBets.Common.Enums.FreeGameState.None,
			GambleState: GameBets.Common.Enums.GambleState.None,
			CashoutState: GameBets.Common.Enums.CashoutState.None,
			ResettleState: GameBets.Common.Enums.ResettleState.None,
			JackpotState: GameBets.Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 2.0m,
			WinLoss: -2.0m,
			Turnover: 2.0m,
			Commission: 0.0m,
			Contribute: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new RapidTest.Common.ViewModels.GameBetRapidTestCommandViewModel(
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
			BetState: RapidTest.Common.Enums.BetState.HalfLoss,
			BetType: RapidTest.Common.Enums.BetType.Normal,
			FreeGameState: RapidTest.Common.Enums.FreeGameState.None,
			GambleState: RapidTest.Common.Enums.GambleState.None,
			CashoutState: RapidTest.Common.Enums.CashoutState.None,
			ResettleState: RapidTest.Common.Enums.ResettleState.None,
			JackpotState: RapidTest.Common.Enums.JackpotState.None,
			CurrencyRatio: command.CurrencyRatio,
			Bet: command.Bet,
			ValidBet: command.ValidBet,
			Settle: command.Settle,
			Commission: command.Commission,
			Turnover: command.Turnover,
			WinLoss: command.WinLoss,
			Jackpot: command.Jackpot,
			AdditionalData: command.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetsRapidTestRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.True(actual);
	}

	[Fact]
	public async Task Policy_Retry()
	{
		var faceLogger = NullLogger<GameBetsRapidTestRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var command = new GameBetRapidTestCommand(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "Demo",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
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

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new RapidTest.Common.ViewModels.GameBetRapidTestCommandViewModel(
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
			BetState: RapidTest.Common.Enums.BetState.Win,
			BetType: RapidTest.Common.Enums.BetType.Normal,
			FreeGameState: RapidTest.Common.Enums.FreeGameState.None,
			GambleState: RapidTest.Common.Enums.GambleState.None,
			CashoutState: RapidTest.Common.Enums.CashoutState.None,
			ResettleState: RapidTest.Common.Enums.ResettleState.None,
			JackpotState: RapidTest.Common.Enums.JackpotState.None,
			CurrencyRatio: command.CurrencyRatio,
			Bet: command.Bet,
			ValidBet: command.ValidBet,
			Settle: command.Settle,
			Commission: command.Commission,
			Turnover: command.Turnover,
			WinLoss: command.WinLoss,
			Jackpot: command.Jackpot,
			AdditionalData: command.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(ValueTask.FromException<bool>(new HttpRequestException()), ValueTask.FromResult(true));

		var sut = new GameBetsRapidTestRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.True(actual);
	}
}
