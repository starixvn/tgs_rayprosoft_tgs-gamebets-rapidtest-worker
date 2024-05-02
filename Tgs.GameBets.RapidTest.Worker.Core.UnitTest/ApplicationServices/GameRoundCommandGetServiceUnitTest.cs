using Castle.Core.Logging;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.ExceptionExtensions;
using System.Text.Json.Nodes;
using Tgs.GameRounds.Client;
using Valhalla.MessageQueue;
using Tgs.GameBets.RapidTest.Worker.Core;
using Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

namespace Tgs.GameBets.RapidTest.Worker.Core.UnitTest.ApplicationServices;

public class GameRoundCommandGetServiceUnitTest
{
	[Fact]
	public async Task BetState_Is_Waiting()
	{
		var faceLogger = NullLogger<GameRoundCommandGetService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var query = new GameRoundCommand(
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.UnixEpoch,
			BetState: GameBets.Common.Enums.BetState.Waiting,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 0.0m,
			Commission: 0.0m,
			Turnover: 0.0m,
			PlayerWinLoss: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is(new GameRounds.Common.ViewModels.RoundCommandViewModel(
				GameProviderId: query.GameProviderId,
				GameProviderConfigurationId: query.GameProviderConfigurationId,
				GameId: query.GameId,
				BrandId: query.BrandId,
				PlayerCode: query.PlayerCode,
				WalletId: query.WalletId,
				CurrencyCode: query.CurrencyCode,
				Status: GameRounds.Common.Enums.RoundStatus.Open,
				ClientStatus: GameRounds.Common.Enums.RoundClientStatus.Open,
				ExternalRoundId: query.ExternalBetId,
				ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
				ExternalClosedOn: (DateTimeOffset?)null,
				BetAmount: query.Bet,
				ValidBetAmount: query.ValidBet,
				SettleAmount: query.Settle,
				Commission: query.Commission,
				Turnover: query.Turnover,
				PlayerWinLoss: query.PlayerWinLoss,
				AdditionalData: query.AdditionalData)),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundCommandGetService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_Running()
	{
		var faceLogger = NullLogger<GameRoundCommandGetService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var query = new GameRoundCommand(
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.UnixEpoch,
			BetState: GameBets.Common.Enums.BetState.Running,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 0.0m,
			Commission: 0.0m,
			Turnover: 0.0m,
			PlayerWinLoss: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is(new GameRounds.Common.ViewModels.RoundCommandViewModel(
				GameProviderId: query.GameProviderId,
				GameProviderConfigurationId: query.GameProviderConfigurationId,
				GameId: query.GameId,
				BrandId: query.BrandId,
				PlayerCode: query.PlayerCode,
				WalletId: query.WalletId,
				CurrencyCode: query.CurrencyCode,
				Status: GameRounds.Common.Enums.RoundStatus.Open,
				ClientStatus: GameRounds.Common.Enums.RoundClientStatus.Open,
				ExternalRoundId: query.ExternalBetId,
				ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
				ExternalClosedOn: null,
				BetAmount: query.Bet,
				ValidBetAmount: query.ValidBet,
				SettleAmount: query.Settle,
				Commission: query.Commission,
				Turnover: query.Turnover,
				PlayerWinLoss: query.PlayerWinLoss,
				AdditionalData: query.AdditionalData)),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundCommandGetService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_Void()
	{
		var faceLogger = NullLogger<GameRoundCommandGetService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var query = new GameRoundCommand(
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			BetState: GameBets.Common.Enums.BetState.Void,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 0.0m,
			Commission: 0.0m,
			Turnover: 0.0m,
			PlayerWinLoss: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});
		;

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is(new GameRounds.Common.ViewModels.RoundCommandViewModel(
				GameProviderId: query.GameProviderId,
				GameProviderConfigurationId: query.GameProviderConfigurationId,
				GameId: query.GameId,
				BrandId: query.BrandId,
				PlayerCode: query.PlayerCode,
				WalletId: query.WalletId,
				CurrencyCode: query.CurrencyCode,
				Status: GameRounds.Common.Enums.RoundStatus.Closed,
				ClientStatus: GameRounds.Common.Enums.RoundClientStatus.Void,
				ExternalRoundId: query.ExternalBetId,
				ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
				ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
				BetAmount: query.Bet,
				ValidBetAmount: query.ValidBet,
				SettleAmount: query.Settle,
				Commission: query.Commission,
				Turnover: query.Turnover,
				PlayerWinLoss: query.PlayerWinLoss,
				AdditionalData: query.AdditionalData)),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundCommandGetService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_Refund()
	{
		var faceLogger = NullLogger<GameRoundCommandGetService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var query = new GameRoundCommand(
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			BetState: GameBets.Common.Enums.BetState.Refund,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 0.0m,
			Commission: 0.0m,
			Turnover: 0.0m,
			PlayerWinLoss: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is(new GameRounds.Common.ViewModels.RoundCommandViewModel(
				GameProviderId: query.GameProviderId,
				GameProviderConfigurationId: query.GameProviderConfigurationId,
				GameId: query.GameId,
				BrandId: query.BrandId,
				PlayerCode: query.PlayerCode,
				WalletId: query.WalletId,
				CurrencyCode: query.CurrencyCode,
				Status: GameRounds.Common.Enums.RoundStatus.Closed,
				ClientStatus: GameRounds.Common.Enums.RoundClientStatus.Void,
				ExternalRoundId: query.ExternalBetId,
				ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
				ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
				BetAmount: query.Bet,
				ValidBetAmount: query.ValidBet,
				SettleAmount: query.Settle,
				Commission: query.Commission,
				Turnover: query.Turnover,
				PlayerWinLoss: query.PlayerWinLoss,
				AdditionalData: query.AdditionalData)),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundCommandGetService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_Cancel()
	{
		var faceLogger = NullLogger<GameRoundCommandGetService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var query = new GameRoundCommand(
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			BetState: GameBets.Common.Enums.BetState.Cancel,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 0.0m,
			Commission: 0.0m,
			Turnover: 0.0m,
			PlayerWinLoss: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is(new GameRounds.Common.ViewModels.RoundCommandViewModel(
				GameProviderId: query.GameProviderId,
				GameProviderConfigurationId: query.GameProviderConfigurationId,
				GameId: query.GameId,
				BrandId: query.BrandId,
				PlayerCode: query.PlayerCode,
				WalletId: query.WalletId,
				CurrencyCode: query.CurrencyCode,
				Status: GameRounds.Common.Enums.RoundStatus.Closed,
				ClientStatus: GameRounds.Common.Enums.RoundClientStatus.Void,
				ExternalRoundId: query.ExternalBetId,
				ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
				ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
				BetAmount: query.Bet,
				ValidBetAmount: query.ValidBet,
				SettleAmount: query.Settle,
				Commission: query.Commission,
				Turnover: query.Turnover,
				PlayerWinLoss: query.PlayerWinLoss,
				AdditionalData: query.AdditionalData)),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundCommandGetService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_Win()
	{
		var faceLogger = NullLogger<GameRoundCommandGetService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var query = new GameRoundCommand(
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			BetState: GameBets.Common.Enums.BetState.Win,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 8.0m,
			Commission: 0.0m,
			Turnover: 4.0m,
			PlayerWinLoss: 4.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is(new GameRounds.Common.ViewModels.RoundCommandViewModel(
				GameProviderId: query.GameProviderId,
				GameProviderConfigurationId: query.GameProviderConfigurationId,
				GameId: query.GameId,
				BrandId: query.BrandId,
				PlayerCode: query.PlayerCode,
				WalletId: query.WalletId,
				CurrencyCode: query.CurrencyCode,
				Status: GameRounds.Common.Enums.RoundStatus.Closed,
				ClientStatus: GameRounds.Common.Enums.RoundClientStatus.Win,
				ExternalRoundId: query.ExternalBetId,
				ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
				ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
				BetAmount: query.Bet,
				ValidBetAmount: query.ValidBet,
				SettleAmount: query.Settle,
				Commission: query.Commission,
				Turnover: query.Turnover,
				PlayerWinLoss: query.PlayerWinLoss,
				AdditionalData: query.AdditionalData)),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundCommandGetService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_Loss()
	{
		var faceLogger = NullLogger<GameRoundCommandGetService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var query = new GameRoundCommand(
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			BetState: GameBets.Common.Enums.BetState.Loss,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 0.0m,
			Commission: 0.0m,
			Turnover: 4.0m,
			PlayerWinLoss: -4.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is(new GameRounds.Common.ViewModels.RoundCommandViewModel(
				GameProviderId: query.GameProviderId,
				GameProviderConfigurationId: query.GameProviderConfigurationId,
				GameId: query.GameId,
				BrandId: query.BrandId,
				PlayerCode: query.PlayerCode,
				WalletId: query.WalletId,
				CurrencyCode: query.CurrencyCode,
				Status: GameRounds.Common.Enums.RoundStatus.Closed,
				ClientStatus: GameRounds.Common.Enums.RoundClientStatus.Loss,
				ExternalRoundId: query.ExternalBetId,
				ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
				ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
				BetAmount: query.Bet,
				ValidBetAmount: query.ValidBet,
				SettleAmount: query.Settle,
				Commission: query.Commission,
				Turnover: query.Turnover,
				PlayerWinLoss: query.PlayerWinLoss,
				AdditionalData: query.AdditionalData)),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundCommandGetService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_Draw()
	{
		var faceLogger = NullLogger<GameRoundCommandGetService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var query = new GameRoundCommand(
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			BetState: GameBets.Common.Enums.BetState.Draw,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 4.0m,
			Commission: 0.0m,
			Turnover: 4.0m,
			PlayerWinLoss: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is(new GameRounds.Common.ViewModels.RoundCommandViewModel(
				GameProviderId: query.GameProviderId,
				GameProviderConfigurationId: query.GameProviderConfigurationId,
				GameId: query.GameId,
				BrandId: query.BrandId,
				PlayerCode: query.PlayerCode,
				WalletId: query.WalletId,
				CurrencyCode: query.CurrencyCode,
				Status: GameRounds.Common.Enums.RoundStatus.Closed,
				ClientStatus: GameRounds.Common.Enums.RoundClientStatus.Draw,
				ExternalRoundId: query.ExternalBetId,
				ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
				ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
				BetAmount: query.Bet,
				ValidBetAmount: query.ValidBet,
				SettleAmount: query.Settle,
				Commission: query.Commission,
				Turnover: query.Turnover,
				PlayerWinLoss: query.PlayerWinLoss,
				AdditionalData: query.AdditionalData)),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundCommandGetService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_HalfWin()
	{
		var faceLogger = NullLogger<GameRoundCommandGetService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var query = new GameRoundCommand(
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			BetState: GameBets.Common.Enums.BetState.HalfWin,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 2.0m,
			Commission: 0.0m,
			Turnover: 4.0m,
			PlayerWinLoss: -2.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is(new GameRounds.Common.ViewModels.RoundCommandViewModel(
				GameProviderId: query.GameProviderId,
				GameProviderConfigurationId: query.GameProviderConfigurationId,
				GameId: query.GameId,
				BrandId: query.BrandId,
				PlayerCode: query.PlayerCode,
				WalletId: query.WalletId,
				CurrencyCode: query.CurrencyCode,
				Status: GameRounds.Common.Enums.RoundStatus.Closed,
				ClientStatus: GameRounds.Common.Enums.RoundClientStatus.Win,
				ExternalRoundId: query.ExternalBetId,
				ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
				ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
				BetAmount: query.Bet,
				ValidBetAmount: query.ValidBet,
				SettleAmount: query.Settle,
				Commission: query.Commission,
				Turnover: query.Turnover,
				PlayerWinLoss: query.PlayerWinLoss,
				AdditionalData: query.AdditionalData)),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundCommandGetService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_HalfLoss()
	{
		var faceLogger = NullLogger<GameRoundCommandGetService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var query = new GameRoundCommand(
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			BetState: GameBets.Common.Enums.BetState.HalfLoss,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 2.0m,
			Commission: 0.0m,
			Turnover: 4.0m,
			PlayerWinLoss: -2.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is(new GameRounds.Common.ViewModels.RoundCommandViewModel(
				GameProviderId: query.GameProviderId,
				GameProviderConfigurationId: query.GameProviderConfigurationId,
				GameId: query.GameId,
				BrandId: query.BrandId,
				PlayerCode: query.PlayerCode,
				WalletId: query.WalletId,
				CurrencyCode: query.CurrencyCode,
				Status: GameRounds.Common.Enums.RoundStatus.Closed,
				ClientStatus: GameRounds.Common.Enums.RoundClientStatus.Loss,
				ExternalRoundId: query.ExternalBetId,
				ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
				ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
				BetAmount: query.Bet,
				ValidBetAmount: query.ValidBet,
				SettleAmount: query.Settle,
				Commission: query.Commission,
				Turnover: query.Turnover,
				PlayerWinLoss: query.PlayerWinLoss,
				AdditionalData: query.AdditionalData)),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundCommandGetService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task Policy_Retry()
	{
		var faceLogger = NullLogger<GameRoundCommandGetService>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var query = new GameRoundCommand(
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			BetState: GameBets.Common.Enums.BetState.Win,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 8.0m,
			Commission: 0.0m,
			Turnover: 4.0m,
			PlayerWinLoss: 4.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is(new GameRounds.Common.ViewModels.RoundCommandViewModel(
				GameProviderId: query.GameProviderId,
				GameProviderConfigurationId: query.GameProviderConfigurationId,
				GameId: query.GameId,
				BrandId: query.BrandId,
				PlayerCode: query.PlayerCode,
				WalletId: query.WalletId,
				CurrencyCode: query.CurrencyCode,
				Status: GameRounds.Common.Enums.RoundStatus.Closed,
				ClientStatus: GameRounds.Common.Enums.RoundClientStatus.Win,
				ExternalRoundId: query.ExternalBetId,
				ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
				ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
				BetAmount: query.Bet,
				ValidBetAmount: query.ValidBet,
				SettleAmount: query.Settle,
				Commission: query.Commission,
				Turnover: query.Turnover,
				PlayerWinLoss: query.PlayerWinLoss,
				AdditionalData: query.AdditionalData)),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(ValueTask.FromException<Guid>(new HttpRequestException()), ValueTask.FromResult(id));

		var sut = new GameRoundCommandGetService(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.GetAsync(query: query);

		Assert.Equal(id, actual);
	}
}
