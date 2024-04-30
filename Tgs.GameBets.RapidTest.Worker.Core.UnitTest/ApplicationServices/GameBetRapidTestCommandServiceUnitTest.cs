using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Tgs.ServiceUtility;
using Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;
using Google.Protobuf.WellKnownTypes;
using Valhalla.MessageQueue;
using Microsoft.Extensions.Options;
using Tgs.GameRounds.Client;
using Tgs.GameBets.RapidTest.Client;

namespace Tgs.GameBets.RapidTest.Worker.Core.UnitTest.ApplicationServices;

public class GameBetRapidTestCommandServiceUnitTest
{
	[Fact]
	public async Task BetState_Is_Waiting()
	{
		var faceLogger = NullLogger<GameBetRapidTestCommandService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var query = new GameBetRapidTestCommand(
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
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: query.GameProviderCode,
			SubGameProviderCode: query.SubGameProviderCode,
			GameId: query.GameId,
			ResellerCode: query.ResellerCode,
			LicenseeCode: query.LicenseeCode,
			BrandCode: query.BrandCode,
			PlayerCode: query.PlayerCode,
			Username: query.Username,
			CurrencyCode: query.CurrencyCode,
			WalletId: query.WalletId,
			ExternalRoundId: query.ExternalRoundId,
			ExternalBetId: query.ExternalBetId,
			ExternalGameId: query.ExternalGameId,
			BetState: Common.Enums.BetState.Waiting,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: query.CurrencyRatio,
			Bet: query.Bet,
			ValidBet: query.ValidBet,
			Settle: query.Settle,
			Commission: query.Commission,
			Turnover: query.Turnover,
			WinLoss: query.WinLoss,
			Jackpot: query.Jackpot,
			AdditionalData: query.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetRapidTestCommandService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_Running()
	{
		var faceLogger = NullLogger<GameBetRapidTestCommandService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var query = new GameBetRapidTestCommand(
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
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: query.GameProviderCode,
			SubGameProviderCode: query.SubGameProviderCode,
			GameId: query.GameId,
			ResellerCode: query.ResellerCode,
			LicenseeCode: query.LicenseeCode,
			BrandCode: query.BrandCode,
			PlayerCode: query.PlayerCode,
			Username: query.Username,
			CurrencyCode: query.CurrencyCode,
			WalletId: query.WalletId,
			ExternalRoundId: query.ExternalRoundId,
			ExternalBetId: query.ExternalBetId,
			ExternalGameId: query.ExternalGameId,
			BetState: Common.Enums.BetState.Running,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: query.CurrencyRatio,
			Bet: query.Bet,
			ValidBet: query.ValidBet,
			Settle: query.Settle,
			Commission: query.Commission,
			Turnover: query.Turnover,
			WinLoss: query.WinLoss,
			Jackpot: query.Jackpot,
			AdditionalData: query.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetRapidTestCommandService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_Void()
	{
		var faceLogger = NullLogger<GameBetRapidTestCommandService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var query = new GameBetRapidTestCommand(
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
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: query.GameProviderCode,
			SubGameProviderCode: query.SubGameProviderCode,
			GameId: query.GameId,
			ResellerCode: query.ResellerCode,
			LicenseeCode: query.LicenseeCode,
			BrandCode: query.BrandCode,
			PlayerCode: query.PlayerCode,
			Username: query.Username,
			CurrencyCode: query.CurrencyCode,
			WalletId: query.WalletId,
			ExternalRoundId: query.ExternalRoundId,
			ExternalBetId: query.ExternalBetId,
			ExternalGameId: query.ExternalGameId,
			BetState: Common.Enums.BetState.Void,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: query.CurrencyRatio,
			Bet: query.Bet,
			ValidBet: query.ValidBet,
			Settle: query.Settle,
			Commission: query.Commission,
			Turnover: query.Turnover,
			WinLoss: query.WinLoss,
			Jackpot: query.Jackpot,
			AdditionalData: query.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetRapidTestCommandService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_Refund()
	{
		var faceLogger = NullLogger<GameBetRapidTestCommandService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var query = new GameBetRapidTestCommand(
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
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: query.GameProviderCode,
			SubGameProviderCode: query.SubGameProviderCode,
			GameId: query.GameId,
			ResellerCode: query.ResellerCode,
			LicenseeCode: query.LicenseeCode,
			BrandCode: query.BrandCode,
			PlayerCode: query.PlayerCode,
			Username: query.Username,
			CurrencyCode: query.CurrencyCode,
			WalletId: query.WalletId,
			ExternalRoundId: query.ExternalRoundId,
			ExternalBetId: query.ExternalBetId,
			ExternalGameId: query.ExternalGameId,
			BetState: Common.Enums.BetState.Refund,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: query.CurrencyRatio,
			Bet: query.Bet,
			ValidBet: query.ValidBet,
			Settle: query.Settle,
			Commission: query.Commission,
			Turnover: query.Turnover,
			WinLoss: query.WinLoss,
			Jackpot: query.Jackpot,
			AdditionalData: query.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetRapidTestCommandService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_Cancel()
	{
		var faceLogger = NullLogger<GameBetRapidTestCommandService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var query = new GameBetRapidTestCommand(
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
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: query.GameProviderCode,
			SubGameProviderCode: query.SubGameProviderCode,
			GameId: query.GameId,
			ResellerCode: query.ResellerCode,
			LicenseeCode: query.LicenseeCode,
			BrandCode: query.BrandCode,
			PlayerCode: query.PlayerCode,
			Username: query.Username,
			CurrencyCode: query.CurrencyCode,
			WalletId: query.WalletId,
			ExternalRoundId: query.ExternalRoundId,
			ExternalBetId: query.ExternalBetId,
			ExternalGameId: query.ExternalGameId,
			BetState: Common.Enums.BetState.Cancel,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: query.CurrencyRatio,
			Bet: query.Bet,
			ValidBet: query.ValidBet,
			Settle: query.Settle,
			Commission: query.Commission,
			Turnover: query.Turnover,
			WinLoss: query.WinLoss,
			Jackpot: query.Jackpot,
			AdditionalData: query.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetRapidTestCommandService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_Win()
	{
		var faceLogger = NullLogger<GameBetRapidTestCommandService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var query = new GameBetRapidTestCommand(
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
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: 4.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: query.GameProviderCode,
			SubGameProviderCode: query.SubGameProviderCode,
			GameId: query.GameId,
			ResellerCode: query.ResellerCode,
			LicenseeCode: query.LicenseeCode,
			BrandCode: query.BrandCode,
			PlayerCode: query.PlayerCode,
			Username: query.Username,
			CurrencyCode: query.CurrencyCode,
			WalletId: query.WalletId,
			ExternalRoundId: query.ExternalRoundId,
			ExternalBetId: query.ExternalBetId,
			ExternalGameId: query.ExternalGameId,
			BetState: Common.Enums.BetState.Win,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: query.CurrencyRatio,
			Bet: query.Bet,
			ValidBet: query.ValidBet,
			Settle: query.Settle,
			Commission: query.Commission,
			Turnover: query.Turnover,
			WinLoss: query.WinLoss,
			Jackpot: query.Jackpot,
			AdditionalData: query.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetRapidTestCommandService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_Loss()
	{
		var faceLogger = NullLogger<GameBetRapidTestCommandService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var query = new GameBetRapidTestCommand(
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
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: -4.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: query.GameProviderCode,
			SubGameProviderCode: query.SubGameProviderCode,
			GameId: query.GameId,
			ResellerCode: query.ResellerCode,
			LicenseeCode: query.LicenseeCode,
			BrandCode: query.BrandCode,
			PlayerCode: query.PlayerCode,
			Username: query.Username,
			CurrencyCode: query.CurrencyCode,
			WalletId: query.WalletId,
			ExternalRoundId: query.ExternalRoundId,
			ExternalBetId: query.ExternalBetId,
			ExternalGameId: query.ExternalGameId,
			BetState: Common.Enums.BetState.Loss,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: query.CurrencyRatio,
			Bet: query.Bet,
			ValidBet: query.ValidBet,
			Settle: query.Settle,
			Commission: query.Commission,
			Turnover: query.Turnover,
			WinLoss: query.WinLoss,
			Jackpot: query.Jackpot,
			AdditionalData: query.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetRapidTestCommandService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_Draw()
	{
		var faceLogger = NullLogger<GameBetRapidTestCommandService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var query = new GameBetRapidTestCommand(
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
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: query.GameProviderCode,
			SubGameProviderCode: query.SubGameProviderCode,
			GameId: query.GameId,
			ResellerCode: query.ResellerCode,
			LicenseeCode: query.LicenseeCode,
			BrandCode: query.BrandCode,
			PlayerCode: query.PlayerCode,
			Username: query.Username,
			CurrencyCode: query.CurrencyCode,
			WalletId: query.WalletId,
			ExternalRoundId: query.ExternalRoundId,
			ExternalBetId: query.ExternalBetId,
			ExternalGameId: query.ExternalGameId,
			BetState: Common.Enums.BetState.Draw,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: query.CurrencyRatio,
			Bet: query.Bet,
			ValidBet: query.ValidBet,
			Settle: query.Settle,
			Commission: query.Commission,
			Turnover: query.Turnover,
			WinLoss: query.WinLoss,
			Jackpot: query.Jackpot,
			AdditionalData: query.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetRapidTestCommandService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_HalfWin()
	{
		var faceLogger = NullLogger<GameBetRapidTestCommandService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var query = new GameBetRapidTestCommand(
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
			Commission: 0.0m,
			Turnover: 2.0m,
			WinLoss: -2.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: query.GameProviderCode,
			SubGameProviderCode: query.SubGameProviderCode,
			GameId: query.GameId,
			ResellerCode: query.ResellerCode,
			LicenseeCode: query.LicenseeCode,
			BrandCode: query.BrandCode,
			PlayerCode: query.PlayerCode,
			Username: query.Username,
			CurrencyCode: query.CurrencyCode,
			WalletId: query.WalletId,
			ExternalRoundId: query.ExternalRoundId,
			ExternalBetId: query.ExternalBetId,
			ExternalGameId: query.ExternalGameId,
			BetState: Common.Enums.BetState.HalfWin,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: query.CurrencyRatio,
			Bet: query.Bet,
			ValidBet: query.ValidBet,
			Settle: query.Settle,
			Commission: query.Commission,
			Turnover: query.Turnover,
			WinLoss: query.WinLoss,
			Jackpot: query.Jackpot,
			AdditionalData: query.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetRapidTestCommandService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.True(actual);
	}

	[Fact]
	public async Task BetState_Is_HalfLoss()
	{
		var faceLogger = NullLogger<GameBetRapidTestCommandService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var query = new GameBetRapidTestCommand(
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
			Commission: 0.0m,
			Turnover: 2.0m,
			WinLoss: -2.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: query.GameProviderCode,
			SubGameProviderCode: query.SubGameProviderCode,
			GameId: query.GameId,
			ResellerCode: query.ResellerCode,
			LicenseeCode: query.LicenseeCode,
			BrandCode: query.BrandCode,
			PlayerCode: query.PlayerCode,
			Username: query.Username,
			CurrencyCode: query.CurrencyCode,
			WalletId: query.WalletId,
			ExternalRoundId: query.ExternalRoundId,
			ExternalBetId: query.ExternalBetId,
			ExternalGameId: query.ExternalGameId,
			BetState: Common.Enums.BetState.HalfLoss,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: query.CurrencyRatio,
			Bet: query.Bet,
			ValidBet: query.ValidBet,
			Settle: query.Settle,
			Commission: query.Commission,
			Turnover: query.Turnover,
			WinLoss: query.WinLoss,
			Jackpot: query.Jackpot,
			AdditionalData: query.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetRapidTestCommandService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.True(actual);
	}

	[Fact]
	public async Task Policy_Retry()
	{
		var faceLogger = NullLogger<GameBetRapidTestCommandService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsRapidTestClient>();

		var query = new GameBetRapidTestCommand(
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
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: 4.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		_ = fakeClient.GameBetRapidTestCommandAsync(
			source: Arg.Is(new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: query.GameProviderCode,
			SubGameProviderCode: query.SubGameProviderCode,
			GameId: query.GameId,
			ResellerCode: query.ResellerCode,
			LicenseeCode: query.LicenseeCode,
			BrandCode: query.BrandCode,
			PlayerCode: query.PlayerCode,
			Username: query.Username,
			CurrencyCode: query.CurrencyCode,
			WalletId: query.WalletId,
			ExternalRoundId: query.ExternalRoundId,
			ExternalBetId: query.ExternalBetId,
			ExternalGameId: query.ExternalGameId,
			BetState: Common.Enums.BetState.Win,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: query.CurrencyRatio,
			Bet: query.Bet,
			ValidBet: query.ValidBet,
			Settle: query.Settle,
			Commission: query.Commission,
			Turnover: query.Turnover,
			WinLoss: query.WinLoss,
			Jackpot: query.Jackpot,
			AdditionalData: query.AdditionalData.ToJsonString())),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(ValueTask.FromException<bool>(new HttpRequestException()), ValueTask.FromResult(true));

		var sut = new GameBetRapidTestCommandService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.True(actual);
	}
}
