using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Tgs.GameBets.RapidTest.Worker.Core;
using Tgs.GameBets.RapidTest.Worker.Core.Models;
using Tgs.GameRounds.Client;

namespace Tgs.GameBets.RapidTest.Worker.Repositories.UnitTest;

public class GameRoundsRepositoryUnitTest
{
	[Fact]
	public async Task BetState_Is_Waiting()
	{
		var faceLogger = NullLogger<GameRoundsRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var command = new GameRoundCommand(
			Id: Guid.NewGuid(),
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
			WinLoss: 0.0m,
			Turnover: 0.0m,
			Commission: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			},
			CreatedOn: DateTimeOffset.UtcNow);

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is<GameRounds.Common.ViewModels.RoundCommandViewModel>(compare =>
				compare.Id == command.Id &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.WalletId == command.WalletId &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.Status == GameRounds.Common.Enums.RoundStatus.Open &&
				compare.ClientStatus == GameRounds.Common.Enums.RoundClientStatus.Open &&
				compare.ExternalRoundId == command.ExternalBetId &&
				compare.ExternalCreatedOn == DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800") &&
				compare.ExternalClosedOn == null &&
				compare.BetAmount == command.Bet &&
				compare.ValidBetAmount == command.ValidBet &&
				compare.SettleAmount == command.Settle &&
				compare.Commission == command.Commission &&
				compare.Turnover == command.Turnover &&
				compare.PlayerWinLoss == command.WinLoss &&
				compare.AdditionalData != null &&
				compare.CreatedOn == command.CreatedOn),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundsRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_Running()
	{
		var faceLogger = NullLogger<GameRoundsRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var command = new GameRoundCommand(
			Id: Guid.NewGuid(),
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
			WinLoss: 0.0m,
			Turnover: 0.0m,
			Commission: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			},
			CreatedOn: DateTimeOffset.UtcNow);

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is<GameRounds.Common.ViewModels.RoundCommandViewModel>(compare =>
				compare.Id == command.Id &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.WalletId == command.WalletId &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.Status == GameRounds.Common.Enums.RoundStatus.Open &&
				compare.ClientStatus == GameRounds.Common.Enums.RoundClientStatus.Open &&
				compare.ExternalRoundId == command.ExternalBetId &&
				compare.ExternalCreatedOn == DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800") &&
				compare.ExternalClosedOn == null &&
				compare.BetAmount == command.Bet &&
				compare.ValidBetAmount == command.ValidBet &&
				compare.SettleAmount == command.Settle &&
				compare.Commission == command.Commission &&
				compare.Turnover == command.Turnover &&
				compare.PlayerWinLoss == command.WinLoss &&
				compare.AdditionalData != null &&
				compare.CreatedOn == command.CreatedOn),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundsRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_Void()
	{
		var faceLogger = NullLogger<GameRoundsRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var command = new GameRoundCommand(
			Id: Guid.NewGuid(),
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
			WinLoss: 0.0m,
			Turnover: 0.0m,
			Commission: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			},
			CreatedOn: DateTimeOffset.UtcNow);
		;

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is<GameRounds.Common.ViewModels.RoundCommandViewModel>(compare =>
				compare.Id == command.Id &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.WalletId == command.WalletId &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.Status == GameRounds.Common.Enums.RoundStatus.Closed &&
				compare.ClientStatus == GameRounds.Common.Enums.RoundClientStatus.Void &&
				compare.ExternalRoundId == command.ExternalBetId &&
				compare.ExternalCreatedOn == DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800") &&
				compare.ExternalClosedOn == DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800") &&
				compare.BetAmount == command.Bet &&
				compare.ValidBetAmount == command.ValidBet &&
				compare.SettleAmount == command.Settle &&
				compare.Commission == command.Commission &&
				compare.Turnover == command.Turnover &&
				compare.PlayerWinLoss == command.WinLoss &&
				compare.AdditionalData != null &&
				compare.CreatedOn == command.CreatedOn),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundsRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_Refund()
	{
		var faceLogger = NullLogger<GameRoundsRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var command = new GameRoundCommand(
			Id: Guid.NewGuid(),
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
			WinLoss: 0.0m,
			Turnover: 0.0m,
			Commission: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			},
			CreatedOn: DateTimeOffset.UtcNow);

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is<GameRounds.Common.ViewModels.RoundCommandViewModel>(compare =>
				compare.Id == command.Id &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.WalletId == command.WalletId &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.Status == GameRounds.Common.Enums.RoundStatus.Closed &&
				compare.ClientStatus == GameRounds.Common.Enums.RoundClientStatus.Void &&
				compare.ExternalRoundId == command.ExternalBetId &&
				compare.ExternalCreatedOn == DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800") &&
				compare.ExternalClosedOn == DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800") &&
				compare.BetAmount == command.Bet &&
				compare.ValidBetAmount == command.ValidBet &&
				compare.SettleAmount == command.Settle &&
				compare.Commission == command.Commission &&
				compare.Turnover == command.Turnover &&
				compare.PlayerWinLoss == command.WinLoss &&
				compare.AdditionalData != null &&
				compare.CreatedOn == command.CreatedOn),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundsRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_Cancel()
	{
		var faceLogger = NullLogger<GameRoundsRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var command = new GameRoundCommand(
			Id: Guid.NewGuid(),
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
			WinLoss: 0.0m,
			Turnover: 0.0m,
			Commission: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			},
			CreatedOn: DateTimeOffset.UtcNow);

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is<GameRounds.Common.ViewModels.RoundCommandViewModel>(compare =>
				compare.Id == command.Id &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.WalletId == command.WalletId &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.Status == GameRounds.Common.Enums.RoundStatus.Closed &&
				compare.ClientStatus == GameRounds.Common.Enums.RoundClientStatus.Void &&
				compare.ExternalRoundId == command.ExternalBetId &&
				compare.ExternalCreatedOn == DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800") &&
				compare.ExternalClosedOn == DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800") &&
				compare.BetAmount == command.Bet &&
				compare.ValidBetAmount == command.ValidBet &&
				compare.SettleAmount == command.Settle &&
				compare.Commission == command.Commission &&
				compare.Turnover == command.Turnover &&
				compare.PlayerWinLoss == command.WinLoss &&
				compare.AdditionalData != null &&
				compare.CreatedOn == command.CreatedOn),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundsRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_Win()
	{
		var faceLogger = NullLogger<GameRoundsRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var command = new GameRoundCommand(
			Id: Guid.NewGuid(),
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
			WinLoss: 4.0m,
			Turnover: 4.0m,
			Commission: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			},
			CreatedOn: DateTimeOffset.UtcNow);

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is<GameRounds.Common.ViewModels.RoundCommandViewModel>(compare =>
				compare.Id == command.Id &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.WalletId == command.WalletId &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.Status == GameRounds.Common.Enums.RoundStatus.Closed &&
				compare.ClientStatus == GameRounds.Common.Enums.RoundClientStatus.Win &&
				compare.ExternalRoundId == command.ExternalBetId &&
				compare.ExternalCreatedOn == DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800") &&
				compare.ExternalClosedOn == DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800") &&
				compare.BetAmount == command.Bet &&
				compare.ValidBetAmount == command.ValidBet &&
				compare.SettleAmount == command.Settle &&
				compare.Commission == command.Commission &&
				compare.Turnover == command.Turnover &&
				compare.PlayerWinLoss == command.WinLoss &&
				compare.AdditionalData != null &&
				compare.CreatedOn == command.CreatedOn),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundsRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_Loss()
	{
		var faceLogger = NullLogger<GameRoundsRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var command = new GameRoundCommand(
			Id: Guid.NewGuid(),
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
			WinLoss: -4.0m,
			Turnover: 4.0m,
			Commission: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			},
			CreatedOn: DateTimeOffset.UtcNow);

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is<GameRounds.Common.ViewModels.RoundCommandViewModel>(compare =>
				compare.Id == command.Id &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.WalletId == command.WalletId &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.Status == GameRounds.Common.Enums.RoundStatus.Closed &&
				compare.ClientStatus == GameRounds.Common.Enums.RoundClientStatus.Loss &&
				compare.ExternalRoundId == command.ExternalBetId &&
				compare.ExternalCreatedOn == DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800") &&
				compare.ExternalClosedOn == DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800") &&
				compare.BetAmount == command.Bet &&
				compare.ValidBetAmount == command.ValidBet &&
				compare.SettleAmount == command.Settle &&
				compare.Commission == command.Commission &&
				compare.Turnover == command.Turnover &&
				compare.PlayerWinLoss == command.WinLoss &&
				compare.AdditionalData != null &&
				compare.CreatedOn == command.CreatedOn),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundsRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_Draw()
	{
		var faceLogger = NullLogger<GameRoundsRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var command = new GameRoundCommand(
			Id: Guid.NewGuid(),
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
			WinLoss: 0.0m,
			Turnover: 4.0m,
			Commission: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			},
			CreatedOn: DateTimeOffset.UtcNow);

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is<GameRounds.Common.ViewModels.RoundCommandViewModel>(compare =>
				compare.Id == command.Id &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.WalletId == command.WalletId &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.Status == GameRounds.Common.Enums.RoundStatus.Closed &&
				compare.ClientStatus == GameRounds.Common.Enums.RoundClientStatus.Draw &&
				compare.ExternalRoundId == command.ExternalBetId &&
				compare.ExternalCreatedOn == DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800") &&
				compare.ExternalClosedOn == DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800") &&
				compare.BetAmount == command.Bet &&
				compare.ValidBetAmount == command.ValidBet &&
				compare.SettleAmount == command.Settle &&
				compare.Commission == command.Commission &&
				compare.Turnover == command.Turnover &&
				compare.PlayerWinLoss == command.WinLoss &&
				compare.AdditionalData != null &&
				compare.CreatedOn == command.CreatedOn),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundsRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_HalfWin()
	{
		var faceLogger = NullLogger<GameRoundsRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var command = new GameRoundCommand(
			Id: Guid.NewGuid(),
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
			WinLoss: -2.0m,
			Turnover: 4.0m,
			Commission: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			},
			CreatedOn: DateTimeOffset.UtcNow);

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is<GameRounds.Common.ViewModels.RoundCommandViewModel>(compare =>
				compare.Id == command.Id &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.WalletId == command.WalletId &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.Status == GameRounds.Common.Enums.RoundStatus.Closed &&
				compare.ClientStatus == GameRounds.Common.Enums.RoundClientStatus.Win &&
				compare.ExternalRoundId == command.ExternalBetId &&
				compare.ExternalCreatedOn == DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800") &&
				compare.ExternalClosedOn == DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800") &&
				compare.BetAmount == command.Bet &&
				compare.ValidBetAmount == command.ValidBet &&
				compare.SettleAmount == command.Settle &&
				compare.Commission == command.Commission &&
				compare.Turnover == command.Turnover &&
				compare.PlayerWinLoss == command.WinLoss &&
				compare.AdditionalData != null &&
				compare.CreatedOn == command.CreatedOn),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundsRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task BetState_Is_HalfLoss()
	{
		var faceLogger = NullLogger<GameRoundsRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var command = new GameRoundCommand(
			Id: Guid.NewGuid(),
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
			WinLoss: -2.0m,
			Turnover: 4.0m,
			Commission: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			},
			CreatedOn: DateTimeOffset.UtcNow);

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is<GameRounds.Common.ViewModels.RoundCommandViewModel>(compare =>
				compare.Id == command.Id &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.WalletId == command.WalletId &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.Status == GameRounds.Common.Enums.RoundStatus.Closed &&
				compare.ClientStatus == GameRounds.Common.Enums.RoundClientStatus.Loss &&
				compare.ExternalRoundId == command.ExternalBetId &&
				compare.ExternalCreatedOn == DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800") &&
				compare.ExternalClosedOn == DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800") &&
				compare.BetAmount == command.Bet &&
				compare.ValidBetAmount == command.ValidBet &&
				compare.SettleAmount == command.Settle &&
				compare.Commission == command.Commission &&
				compare.Turnover == command.Turnover &&
				compare.PlayerWinLoss == command.WinLoss &&
				compare.AdditionalData != null &&
				compare.CreatedOn == command.CreatedOn),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(id);

		var sut = new GameRoundsRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.Equal(id, actual);
	}

	[Fact]
	public async Task Policy_Retry()
	{
		var faceLogger = NullLogger<GameRoundsRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameRoundsClient>();

		var command = new GameRoundCommand(
			Id: Guid.NewGuid(),
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
			WinLoss: 4.0m,
			Turnover: 4.0m,
			Commission: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			},
			CreatedOn: DateTimeOffset.UtcNow);

		var id = Guid.NewGuid();
		_ = fakeClient.RoundCommandAsync(
			source: Arg.Is<GameRounds.Common.ViewModels.RoundCommandViewModel>(compare =>
				compare.Id == command.Id &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.WalletId == command.WalletId &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.Status == GameRounds.Common.Enums.RoundStatus.Closed &&
				compare.ClientStatus == GameRounds.Common.Enums.RoundClientStatus.Win &&
				compare.ExternalRoundId == command.ExternalBetId &&
				compare.ExternalCreatedOn == DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800") &&
				compare.ExternalClosedOn == DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800") &&
				compare.BetAmount == command.Bet &&
				compare.ValidBetAmount == command.ValidBet &&
				compare.SettleAmount == command.Settle &&
				compare.Commission == command.Commission &&
				compare.Turnover == command.Turnover &&
				compare.PlayerWinLoss == command.WinLoss &&
				compare.AdditionalData != null &&
				compare.CreatedOn == command.CreatedOn),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(
			ValueTask.FromException<Guid>(new HttpRequestException()),
			ValueTask.FromResult(id));

		var sut = new GameRoundsRepository(
			faceLogger,
			fakeOptions,
			fakeClient);

		var actual = await sut.CommandAsync(command: command);

		Assert.Equal(id, actual);
	}
}
