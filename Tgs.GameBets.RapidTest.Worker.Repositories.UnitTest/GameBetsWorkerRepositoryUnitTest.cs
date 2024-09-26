using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Tgs.GameBets.RapidTest.Worker.Core;
using Tgs.GameBets.Worker.Client;
using Tgs.GameBets.RapidTest.Worker.Core.Models;

namespace Tgs.GameBets.RapidTest.Worker.Repositories.UnitTest;

public class GameBetsWorkerRepositoryUnitTest
{
	[Fact]
	public async Task BetState_Is_Waiting()
	{
		var fakeLogger = NullLogger<GameBetsWorkerRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsWorkerClient>();

		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalRoundId = externalCreatedOn.ToUnixTimeMilliseconds().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalClosedOn = DateTimeOffset.UnixEpoch;
		var externalPayoffOn = DateTimeOffset.UnixEpoch;
		var createdOn = DateTimeOffset.UtcNow.AddMinutes(-4);
		var closedOn = DateTimeOffset.UnixEpoch;

		var command = new GameBetWorkCommand(
			GameBetId: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: externalRoundId,
			ExternalBetId: externalBetId,
			ExternalCreatedOn: externalCreatedOn,
			ExternalClosedOn: externalClosedOn,
			ExternalPayoffOn: externalPayoffOn,
			ExternalResettleOn: DateTimeOffset.UnixEpoch,
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
			CreatedOn: createdOn,
			ClosedOn: closedOn,
			AdditionalData: new JsonObject
			{
				["ExternalRoundId"] = externalRoundId,
				["ExternalBetId"] = externalBetId
			},
			ExtendedData: new JsonObject { });

		var sut = new GameBetsWorkerRepository(
			fakeLogger,
			fakeOptions,
			fakeClient);

		await sut.CommandAsync(command: command);

		_ = fakeClient
			.Received(1)
			.HandleAsync(
			source: Arg.Is<GameBets.Worker.Common.ViewModels.GameBetHandleViewModel>(compare =>
				compare.GameBetId == command.GameBetId &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.ResellerId == command.ResellerId &&
				compare.LicenseeId == command.LicenseeId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.WalletId == command.WalletId &&
				compare.ExternalRoundId == command.ExternalRoundId &&
				compare.ExternalBetId == command.ExternalBetId &&
				compare.ExternalCreatedOn == command.ExternalCreatedOn &&
				compare.ExternalClosedOn == command.ExternalClosedOn &&
				compare.ExternalPayoffOn == command.ExternalPayoffOn &&
				compare.ExternalResettleOn == command.ExternalResettleOn &&
				compare.BetState == GameBets.Worker.Common.Protos.BetState.Waiting &&
				compare.BetType == GameBets.Worker.Common.Protos.BetType.Normal &&
				compare.FreeGameState == GameBets.Worker.Common.Protos.FreeGameState.None &&
				compare.GambleState == GameBets.Worker.Common.Protos.GambleState.None &&
				compare.CashoutState == GameBets.Worker.Common.Protos.CashoutState.None &&
				compare.ResettleState == GameBets.Worker.Common.Protos.ResettleState.None &&
				compare.JackpotState == GameBets.Worker.Common.Protos.JackpotState.None &&
				compare.CurrencyRatio == command.CurrencyRatio &&
				compare.Bet == command.Bet &&
				compare.ValidBet == command.ValidBet &&
				compare.Settle == command.Settle &&
				compare.WinLoss == command.WinLoss &&
				compare.Turnover == command.Turnover &&
				compare.Commission == command.Commission &&
				compare.Contribute == command.Contribute &&
				compare.Jackpot == command.Jackpot &&
				compare.CreatedOn == command.CreatedOn &&
				compare.ClosedOn == command.ClosedOn &&
				compare.AdditionalData == command.AdditionalData &&
				compare.ExtendedData == command.ExtendedData),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Running()
	{
		var fakeLogger = NullLogger<GameBetsWorkerRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsWorkerClient>();

		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalRoundId = externalCreatedOn.ToUnixTimeMilliseconds().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalClosedOn = DateTimeOffset.UnixEpoch;
		var externalPayoffOn = DateTimeOffset.UnixEpoch;
		var createdOn = DateTimeOffset.UtcNow.AddMinutes(-4);
		var closedOn = DateTimeOffset.UnixEpoch;

		var command = new GameBetWorkCommand(
			GameBetId: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: externalRoundId,
			ExternalBetId: externalBetId,
			ExternalCreatedOn: externalCreatedOn,
			ExternalClosedOn: externalClosedOn,
			ExternalPayoffOn: externalPayoffOn,
			ExternalResettleOn: DateTimeOffset.UnixEpoch,
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
			CreatedOn: createdOn,
			ClosedOn: closedOn,
			AdditionalData: new JsonObject
			{
				["ExternalRoundId"] = externalRoundId,
				["ExternalBetId"] = externalBetId
			},
			ExtendedData: new JsonObject { });

		var sut = new GameBetsWorkerRepository(
			fakeLogger,
			fakeOptions,
			fakeClient);

		await sut.CommandAsync(command: command);

		_ = fakeClient
			.Received(1)
			.HandleAsync(
			source: Arg.Is<GameBets.Worker.Common.ViewModels.GameBetHandleViewModel>(compare =>
				compare.GameBetId == command.GameBetId &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.ResellerId == command.ResellerId &&
				compare.LicenseeId == command.LicenseeId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.WalletId == command.WalletId &&
				compare.ExternalRoundId == command.ExternalRoundId &&
				compare.ExternalBetId == command.ExternalBetId &&
				compare.ExternalCreatedOn == command.ExternalCreatedOn &&
				compare.ExternalClosedOn == command.ExternalClosedOn &&
				compare.ExternalPayoffOn == command.ExternalPayoffOn &&
				compare.ExternalResettleOn == command.ExternalResettleOn &&
				compare.BetState == GameBets.Worker.Common.Protos.BetState.Running &&
				compare.BetType == GameBets.Worker.Common.Protos.BetType.Normal &&
				compare.FreeGameState == GameBets.Worker.Common.Protos.FreeGameState.None &&
				compare.GambleState == GameBets.Worker.Common.Protos.GambleState.None &&
				compare.CashoutState == GameBets.Worker.Common.Protos.CashoutState.None &&
				compare.ResettleState == GameBets.Worker.Common.Protos.ResettleState.None &&
				compare.JackpotState == GameBets.Worker.Common.Protos.JackpotState.None &&
				compare.CurrencyRatio == command.CurrencyRatio &&
				compare.Bet == command.Bet &&
				compare.ValidBet == command.ValidBet &&
				compare.Settle == command.Settle &&
				compare.WinLoss == command.WinLoss &&
				compare.Turnover == command.Turnover &&
				compare.Commission == command.Commission &&
				compare.Contribute == command.Contribute &&
				compare.Jackpot == command.Jackpot &&
				compare.CreatedOn == command.CreatedOn &&
				compare.ClosedOn == command.ClosedOn &&
				compare.AdditionalData == command.AdditionalData &&
				compare.ExtendedData == command.ExtendedData),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Void()
	{
		var fakeLogger = NullLogger<GameBetsWorkerRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsWorkerClient>();

		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalRoundId = externalCreatedOn.ToUnixTimeMilliseconds().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;
		var createdOn = DateTimeOffset.UtcNow.AddMinutes(-4);
		var closedOn = DateTimeOffset.UtcNow.AddSeconds(-4);

		var command = new GameBetWorkCommand(
			GameBetId: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: externalRoundId,
			ExternalBetId: externalBetId,
			ExternalCreatedOn: externalCreatedOn,
			ExternalClosedOn: externalClosedOn,
			ExternalPayoffOn: externalPayoffOn,
			ExternalResettleOn: DateTimeOffset.UnixEpoch,
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
			CreatedOn: createdOn,
			ClosedOn: closedOn,
			AdditionalData: new JsonObject
			{
				["ExternalRoundId"] = externalRoundId,
				["ExternalBetId"] = externalBetId
			},
			ExtendedData: new JsonObject { });

		var sut = new GameBetsWorkerRepository(
			fakeLogger,
			fakeOptions,
			fakeClient);

		await sut.CommandAsync(command: command);

		_ = fakeClient
			.Received(1)
			.HandleAsync(
			source: Arg.Is<GameBets.Worker.Common.ViewModels.GameBetHandleViewModel>(compare =>
				compare.GameBetId == command.GameBetId &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.ResellerId == command.ResellerId &&
				compare.LicenseeId == command.LicenseeId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.WalletId == command.WalletId &&
				compare.ExternalRoundId == command.ExternalRoundId &&
				compare.ExternalBetId == command.ExternalBetId &&
				compare.ExternalCreatedOn == command.ExternalCreatedOn &&
				compare.ExternalClosedOn == command.ExternalClosedOn &&
				compare.ExternalPayoffOn == command.ExternalPayoffOn &&
				compare.ExternalResettleOn == command.ExternalResettleOn &&
				compare.BetState == GameBets.Worker.Common.Protos.BetState.Void &&
				compare.BetType == GameBets.Worker.Common.Protos.BetType.Normal &&
				compare.FreeGameState == GameBets.Worker.Common.Protos.FreeGameState.None &&
				compare.GambleState == GameBets.Worker.Common.Protos.GambleState.None &&
				compare.CashoutState == GameBets.Worker.Common.Protos.CashoutState.None &&
				compare.ResettleState == GameBets.Worker.Common.Protos.ResettleState.None &&
				compare.JackpotState == GameBets.Worker.Common.Protos.JackpotState.None &&
				compare.CurrencyRatio == command.CurrencyRatio &&
				compare.Bet == command.Bet &&
				compare.ValidBet == command.ValidBet &&
				compare.Settle == command.Settle &&
				compare.WinLoss == command.WinLoss &&
				compare.Turnover == command.Turnover &&
				compare.Commission == command.Commission &&
				compare.Contribute == command.Contribute &&
				compare.Jackpot == command.Jackpot &&
				compare.CreatedOn == command.CreatedOn &&
				compare.ClosedOn == command.ClosedOn &&
				compare.AdditionalData == command.AdditionalData &&
				compare.ExtendedData == command.ExtendedData),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Refund()
	{
		var fakeLogger = NullLogger<GameBetsWorkerRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsWorkerClient>();

		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalRoundId = externalCreatedOn.ToUnixTimeMilliseconds().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;
		var createdOn = DateTimeOffset.UtcNow.AddMinutes(-4);
		var closedOn = DateTimeOffset.UtcNow.AddSeconds(-4);

		var command = new GameBetWorkCommand(
			GameBetId: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: externalRoundId,
			ExternalBetId: externalBetId,
			ExternalCreatedOn: externalCreatedOn,
			ExternalClosedOn: externalClosedOn,
			ExternalPayoffOn: externalPayoffOn,
			ExternalResettleOn: DateTimeOffset.UnixEpoch,
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
			CreatedOn: createdOn,
			ClosedOn: closedOn,
			AdditionalData: new JsonObject
			{
				["ExternalRoundId"] = externalRoundId,
				["ExternalBetId"] = externalBetId
			},
			ExtendedData: new JsonObject { });

		var sut = new GameBetsWorkerRepository(
			fakeLogger,
			fakeOptions,
			fakeClient);

		await sut.CommandAsync(command: command);

		_ = fakeClient
			.Received(1)
			.HandleAsync(
			source: Arg.Is<GameBets.Worker.Common.ViewModels.GameBetHandleViewModel>(compare =>
				compare.GameBetId == command.GameBetId &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.ResellerId == command.ResellerId &&
				compare.LicenseeId == command.LicenseeId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.WalletId == command.WalletId &&
				compare.ExternalRoundId == command.ExternalRoundId &&
				compare.ExternalBetId == command.ExternalBetId &&
				compare.ExternalCreatedOn == command.ExternalCreatedOn &&
				compare.ExternalClosedOn == command.ExternalClosedOn &&
				compare.ExternalPayoffOn == command.ExternalPayoffOn &&
				compare.ExternalResettleOn == command.ExternalResettleOn &&
				compare.BetState == GameBets.Worker.Common.Protos.BetState.Refund &&
				compare.BetType == GameBets.Worker.Common.Protos.BetType.Normal &&
				compare.FreeGameState == GameBets.Worker.Common.Protos.FreeGameState.None &&
				compare.GambleState == GameBets.Worker.Common.Protos.GambleState.None &&
				compare.CashoutState == GameBets.Worker.Common.Protos.CashoutState.None &&
				compare.ResettleState == GameBets.Worker.Common.Protos.ResettleState.None &&
				compare.JackpotState == GameBets.Worker.Common.Protos.JackpotState.None &&
				compare.CurrencyRatio == command.CurrencyRatio &&
				compare.Bet == command.Bet &&
				compare.ValidBet == command.ValidBet &&
				compare.Settle == command.Settle &&
				compare.WinLoss == command.WinLoss &&
				compare.Turnover == command.Turnover &&
				compare.Commission == command.Commission &&
				compare.Contribute == command.Contribute &&
				compare.Jackpot == command.Jackpot &&
				compare.CreatedOn == command.CreatedOn &&
				compare.ClosedOn == command.ClosedOn &&
				compare.AdditionalData == command.AdditionalData &&
				compare.ExtendedData == command.ExtendedData),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Refund_And_Cashout()
	{
		var fakeLogger = NullLogger<GameBetsWorkerRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsWorkerClient>();

		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalRoundId = externalCreatedOn.ToUnixTimeMilliseconds().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;
		var createdOn = DateTimeOffset.UtcNow.AddMinutes(-4);
		var closedOn = DateTimeOffset.UtcNow.AddSeconds(-4);

		var command = new GameBetWorkCommand(
			GameBetId: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: externalRoundId,
			ExternalBetId: externalBetId,
			ExternalCreatedOn: externalCreatedOn,
			ExternalClosedOn: externalClosedOn,
			ExternalPayoffOn: externalPayoffOn,
			ExternalResettleOn: DateTimeOffset.UnixEpoch,
			BetState: GameBets.Common.Enums.BetState.Refund,
			BetType: GameBets.Common.Enums.BetType.Normal,
			FreeGameState: GameBets.Common.Enums.FreeGameState.None,
			GambleState: GameBets.Common.Enums.GambleState.None,
			CashoutState: GameBets.Common.Enums.CashoutState.Cashout,
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
			CreatedOn: createdOn,
			ClosedOn: closedOn,
			AdditionalData: new JsonObject
			{
				["ExternalRoundId"] = externalRoundId,
				["ExternalBetId"] = externalBetId
			},
			ExtendedData: new JsonObject { });

		var sut = new GameBetsWorkerRepository(
			fakeLogger,
			fakeOptions,
			fakeClient);

		await sut.CommandAsync(command: command);

		_ = fakeClient
			.Received(1)
			.HandleAsync(
			source: Arg.Is<GameBets.Worker.Common.ViewModels.GameBetHandleViewModel>(compare =>
				compare.GameBetId == command.GameBetId &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.ResellerId == command.ResellerId &&
				compare.LicenseeId == command.LicenseeId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.WalletId == command.WalletId &&
				compare.ExternalRoundId == command.ExternalRoundId &&
				compare.ExternalBetId == command.ExternalBetId &&
				compare.ExternalCreatedOn == command.ExternalCreatedOn &&
				compare.ExternalClosedOn == command.ExternalClosedOn &&
				compare.ExternalPayoffOn == command.ExternalPayoffOn &&
				compare.ExternalResettleOn == command.ExternalResettleOn &&
				compare.BetState == GameBets.Worker.Common.Protos.BetState.Refund &&
				compare.BetType == GameBets.Worker.Common.Protos.BetType.Normal &&
				compare.FreeGameState == GameBets.Worker.Common.Protos.FreeGameState.None &&
				compare.GambleState == GameBets.Worker.Common.Protos.GambleState.None &&
				compare.CashoutState == GameBets.Worker.Common.Protos.CashoutState.Cashout &&
				compare.ResettleState == GameBets.Worker.Common.Protos.ResettleState.None &&
				compare.JackpotState == GameBets.Worker.Common.Protos.JackpotState.None &&
				compare.CurrencyRatio == command.CurrencyRatio &&
				compare.Bet == command.Bet &&
				compare.ValidBet == command.ValidBet &&
				compare.Settle == command.Settle &&
				compare.WinLoss == command.WinLoss &&
				compare.Turnover == command.Turnover &&
				compare.Commission == command.Commission &&
				compare.Contribute == command.Contribute &&
				compare.Jackpot == command.Jackpot &&
				compare.CreatedOn == command.CreatedOn &&
				compare.ClosedOn == command.ClosedOn &&
				compare.AdditionalData == command.AdditionalData &&
				compare.ExtendedData == command.ExtendedData),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Cancel()
	{
		var fakeLogger = NullLogger<GameBetsWorkerRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsWorkerClient>();

		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalRoundId = externalCreatedOn.ToUnixTimeMilliseconds().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;
		var createdOn = DateTimeOffset.UtcNow.AddMinutes(-4);
		var closedOn = DateTimeOffset.UtcNow.AddSeconds(-4);

		var command = new GameBetWorkCommand(
			GameBetId: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: externalRoundId,
			ExternalBetId: externalBetId,
			ExternalCreatedOn: externalCreatedOn,
			ExternalClosedOn: externalClosedOn,
			ExternalPayoffOn: externalPayoffOn,
			ExternalResettleOn: DateTimeOffset.UnixEpoch,
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
			CreatedOn: createdOn,
			ClosedOn: closedOn,
			AdditionalData: new JsonObject
			{
				["ExternalRoundId"] = externalRoundId,
				["ExternalBetId"] = externalBetId
			},
			ExtendedData: new JsonObject { });

		var sut = new GameBetsWorkerRepository(
			fakeLogger,
			fakeOptions,
			fakeClient);

		await sut.CommandAsync(command: command);

		_ = fakeClient
			.Received(1)
			.HandleAsync(
			source: Arg.Is<GameBets.Worker.Common.ViewModels.GameBetHandleViewModel>(compare =>
				compare.GameBetId == command.GameBetId &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.ResellerId == command.ResellerId &&
				compare.LicenseeId == command.LicenseeId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.WalletId == command.WalletId &&
				compare.ExternalRoundId == command.ExternalRoundId &&
				compare.ExternalBetId == command.ExternalBetId &&
				compare.ExternalCreatedOn == command.ExternalCreatedOn &&
				compare.ExternalClosedOn == command.ExternalClosedOn &&
				compare.ExternalPayoffOn == command.ExternalPayoffOn &&
				compare.ExternalResettleOn == command.ExternalResettleOn &&
				compare.BetState == GameBets.Worker.Common.Protos.BetState.Cancel &&
				compare.BetType == GameBets.Worker.Common.Protos.BetType.Normal &&
				compare.FreeGameState == GameBets.Worker.Common.Protos.FreeGameState.None &&
				compare.GambleState == GameBets.Worker.Common.Protos.GambleState.None &&
				compare.CashoutState == GameBets.Worker.Common.Protos.CashoutState.None &&
				compare.ResettleState == GameBets.Worker.Common.Protos.ResettleState.None &&
				compare.JackpotState == GameBets.Worker.Common.Protos.JackpotState.None &&
				compare.CurrencyRatio == command.CurrencyRatio &&
				compare.Bet == command.Bet &&
				compare.ValidBet == command.ValidBet &&
				compare.Settle == command.Settle &&
				compare.WinLoss == command.WinLoss &&
				compare.Turnover == command.Turnover &&
				compare.Commission == command.Commission &&
				compare.Contribute == command.Contribute &&
				compare.Jackpot == command.Jackpot &&
				compare.CreatedOn == command.CreatedOn &&
				compare.ClosedOn == command.ClosedOn &&
				compare.AdditionalData == command.AdditionalData &&
				compare.ExtendedData == command.ExtendedData),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Win()
	{
		var fakeLogger = NullLogger<GameBetsWorkerRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsWorkerClient>();

		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalRoundId = externalCreatedOn.ToUnixTimeMilliseconds().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;
		var createdOn = DateTimeOffset.UtcNow.AddMinutes(-4);
		var closedOn = DateTimeOffset.UtcNow.AddSeconds(-4);

		var command = new GameBetWorkCommand(
			GameBetId: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: externalRoundId,
			ExternalBetId: externalBetId,
			ExternalCreatedOn: externalCreatedOn,
			ExternalClosedOn: externalClosedOn,
			ExternalPayoffOn: externalPayoffOn,
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
			Commission: 0.1m,
			Contribute: 0.01m,
			Jackpot: 0.0m,
			CreatedOn: createdOn,
			ClosedOn: closedOn,
			AdditionalData: new JsonObject
			{
				["ExternalRoundId"] = externalRoundId,
				["ExternalBetId"] = externalBetId
			},
			ExtendedData: new JsonObject { });

		var sut = new GameBetsWorkerRepository(
			fakeLogger,
			fakeOptions,
			fakeClient);

		await sut.CommandAsync(command: command);

		_ = fakeClient
			.Received(1)
			.HandleAsync(
			source: Arg.Is<GameBets.Worker.Common.ViewModels.GameBetHandleViewModel>(compare =>
				compare.GameBetId == command.GameBetId &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.ResellerId == command.ResellerId &&
				compare.LicenseeId == command.LicenseeId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.WalletId == command.WalletId &&
				compare.ExternalRoundId == command.ExternalRoundId &&
				compare.ExternalBetId == command.ExternalBetId &&
				compare.ExternalCreatedOn == command.ExternalCreatedOn &&
				compare.ExternalClosedOn == command.ExternalClosedOn &&
				compare.ExternalPayoffOn == command.ExternalPayoffOn &&
				compare.ExternalResettleOn == command.ExternalResettleOn &&
				compare.BetState == GameBets.Worker.Common.Protos.BetState.Win &&
				compare.BetType == GameBets.Worker.Common.Protos.BetType.Normal &&
				compare.FreeGameState == GameBets.Worker.Common.Protos.FreeGameState.None &&
				compare.GambleState == GameBets.Worker.Common.Protos.GambleState.None &&
				compare.CashoutState == GameBets.Worker.Common.Protos.CashoutState.None &&
				compare.ResettleState == GameBets.Worker.Common.Protos.ResettleState.None &&
				compare.JackpotState == GameBets.Worker.Common.Protos.JackpotState.None &&
				compare.CurrencyRatio == command.CurrencyRatio &&
				compare.Bet == command.Bet &&
				compare.ValidBet == command.ValidBet &&
				compare.Settle == command.Settle &&
				compare.WinLoss == command.WinLoss &&
				compare.Turnover == command.Turnover &&
				compare.Commission == command.Commission &&
				compare.Contribute == command.Contribute &&
				compare.Jackpot == command.Jackpot &&
				compare.CreatedOn == command.CreatedOn &&
				compare.ClosedOn == command.ClosedOn &&
				compare.AdditionalData == command.AdditionalData &&
				compare.ExtendedData == command.ExtendedData),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Win_And_FreeGame()
	{
		var fakeLogger = NullLogger<GameBetsWorkerRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsWorkerClient>();

		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalRoundId = externalCreatedOn.ToUnixTimeMilliseconds().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;
		var createdOn = DateTimeOffset.UtcNow.AddMinutes(-4);
		var closedOn = DateTimeOffset.UtcNow.AddSeconds(-4);

		var command = new GameBetWorkCommand(
			GameBetId: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: externalRoundId,
			ExternalBetId: externalBetId,
			ExternalCreatedOn: externalCreatedOn,
			ExternalClosedOn: externalClosedOn,
			ExternalPayoffOn: externalPayoffOn,
			ExternalResettleOn: DateTimeOffset.UnixEpoch,
			BetState: GameBets.Common.Enums.BetState.Win,
			BetType: GameBets.Common.Enums.BetType.Normal,
			FreeGameState: GameBets.Common.Enums.FreeGameState.FreeGame,
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
			Commission: 0.1m,
			Contribute: 0.01m,
			Jackpot: 0.0m,
			CreatedOn: createdOn,
			ClosedOn: closedOn,
			AdditionalData: new JsonObject
			{
				["ExternalRoundId"] = externalRoundId,
				["ExternalBetId"] = externalBetId
			},
			ExtendedData: new JsonObject { });

		var sut = new GameBetsWorkerRepository(
			fakeLogger,
			fakeOptions,
			fakeClient);

		await sut.CommandAsync(command: command);

		_ = fakeClient
			.Received(1)
			.HandleAsync(
			source: Arg.Is<GameBets.Worker.Common.ViewModels.GameBetHandleViewModel>(compare =>
				compare.GameBetId == command.GameBetId &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.ResellerId == command.ResellerId &&
				compare.LicenseeId == command.LicenseeId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.WalletId == command.WalletId &&
				compare.ExternalRoundId == command.ExternalRoundId &&
				compare.ExternalBetId == command.ExternalBetId &&
				compare.ExternalCreatedOn == command.ExternalCreatedOn &&
				compare.ExternalClosedOn == command.ExternalClosedOn &&
				compare.ExternalPayoffOn == command.ExternalPayoffOn &&
				compare.ExternalResettleOn == command.ExternalResettleOn &&
				compare.BetState == GameBets.Worker.Common.Protos.BetState.Win &&
				compare.BetType == GameBets.Worker.Common.Protos.BetType.Normal &&
				compare.FreeGameState == GameBets.Worker.Common.Protos.FreeGameState.FreeGame &&
				compare.GambleState == GameBets.Worker.Common.Protos.GambleState.None &&
				compare.CashoutState == GameBets.Worker.Common.Protos.CashoutState.None &&
				compare.ResettleState == GameBets.Worker.Common.Protos.ResettleState.None &&
				compare.JackpotState == GameBets.Worker.Common.Protos.JackpotState.None &&
				compare.CurrencyRatio == command.CurrencyRatio &&
				compare.Bet == command.Bet &&
				compare.ValidBet == command.ValidBet &&
				compare.Settle == command.Settle &&
				compare.WinLoss == command.WinLoss &&
				compare.Turnover == command.Turnover &&
				compare.Commission == command.Commission &&
				compare.Contribute == command.Contribute &&
				compare.Jackpot == command.Jackpot &&
				compare.CreatedOn == command.CreatedOn &&
				compare.ClosedOn == command.ClosedOn &&
				compare.AdditionalData == command.AdditionalData &&
				compare.ExtendedData == command.ExtendedData),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Win_And_Jackpot()
	{
		var fakeLogger = NullLogger<GameBetsWorkerRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsWorkerClient>();

		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalRoundId = externalCreatedOn.ToUnixTimeMilliseconds().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;
		var createdOn = DateTimeOffset.UtcNow.AddMinutes(-4);
		var closedOn = DateTimeOffset.UtcNow.AddSeconds(-4);

		var command = new GameBetWorkCommand(
			GameBetId: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: externalRoundId,
			ExternalBetId: externalBetId,
			ExternalCreatedOn: externalCreatedOn,
			ExternalClosedOn: externalClosedOn,
			ExternalPayoffOn: externalPayoffOn,
			ExternalResettleOn: DateTimeOffset.UnixEpoch,
			BetState: GameBets.Common.Enums.BetState.Win,
			BetType: GameBets.Common.Enums.BetType.Normal,
			FreeGameState: GameBets.Common.Enums.FreeGameState.None,
			GambleState: GameBets.Common.Enums.GambleState.None,
			CashoutState: GameBets.Common.Enums.CashoutState.None,
			ResettleState: GameBets.Common.Enums.ResettleState.None,
			JackpotState: GameBets.Common.Enums.JackpotState.Jackpot,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 108.0m,
			WinLoss: 4.0m,
			Turnover: 4.0m,
			Commission: 0.1m,
			Contribute: 0.01m,
			Jackpot: 100.0m,
			CreatedOn: createdOn,
			ClosedOn: closedOn,
			AdditionalData: new JsonObject
			{
				["ExternalRoundId"] = externalRoundId,
				["ExternalBetId"] = externalBetId
			},
			ExtendedData: new JsonObject { });

		var sut = new GameBetsWorkerRepository(
			fakeLogger,
			fakeOptions,
			fakeClient);

		await sut.CommandAsync(command: command);

		_ = fakeClient
			.Received(1)
			.HandleAsync(
			source: Arg.Is<GameBets.Worker.Common.ViewModels.GameBetHandleViewModel>(compare =>
				compare.GameBetId == command.GameBetId &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.ResellerId == command.ResellerId &&
				compare.LicenseeId == command.LicenseeId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.WalletId == command.WalletId &&
				compare.ExternalRoundId == command.ExternalRoundId &&
				compare.ExternalBetId == command.ExternalBetId &&
				compare.ExternalCreatedOn == command.ExternalCreatedOn &&
				compare.ExternalClosedOn == command.ExternalClosedOn &&
				compare.ExternalPayoffOn == command.ExternalPayoffOn &&
				compare.ExternalResettleOn == command.ExternalResettleOn &&
				compare.BetState == GameBets.Worker.Common.Protos.BetState.Win &&
				compare.BetType == GameBets.Worker.Common.Protos.BetType.Normal &&
				compare.FreeGameState == GameBets.Worker.Common.Protos.FreeGameState.None &&
				compare.GambleState == GameBets.Worker.Common.Protos.GambleState.None &&
				compare.CashoutState == GameBets.Worker.Common.Protos.CashoutState.None &&
				compare.ResettleState == GameBets.Worker.Common.Protos.ResettleState.None &&
				compare.JackpotState == GameBets.Worker.Common.Protos.JackpotState.Jackpot &&
				compare.CurrencyRatio == command.CurrencyRatio &&
				compare.Bet == command.Bet &&
				compare.ValidBet == command.ValidBet &&
				compare.Settle == command.Settle &&
				compare.WinLoss == command.WinLoss &&
				compare.Turnover == command.Turnover &&
				compare.Commission == command.Commission &&
				compare.Contribute == command.Contribute &&
				compare.Jackpot == command.Jackpot &&
				compare.CreatedOn == command.CreatedOn &&
				compare.ClosedOn == command.ClosedOn &&
				compare.AdditionalData == command.AdditionalData &&
				compare.ExtendedData == command.ExtendedData),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Loss()
	{
		var fakeLogger = NullLogger<GameBetsWorkerRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsWorkerClient>();

		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalRoundId = externalCreatedOn.ToUnixTimeMilliseconds().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;
		var createdOn = DateTimeOffset.UtcNow.AddMinutes(-4);
		var closedOn = DateTimeOffset.UtcNow.AddSeconds(-4);

		var command = new GameBetWorkCommand(
			GameBetId: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: externalRoundId,
			ExternalBetId: externalBetId,
			ExternalCreatedOn: externalCreatedOn,
			ExternalClosedOn: externalClosedOn,
			ExternalPayoffOn: externalPayoffOn,
			ExternalResettleOn: DateTimeOffset.UnixEpoch,
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
			Commission: 0.1m,
			Contribute: 0.01m,
			Jackpot: 0.0m,
			CreatedOn: createdOn,
			ClosedOn: closedOn,
			AdditionalData: new JsonObject
			{
				["ExternalRoundId"] = externalRoundId,
				["ExternalBetId"] = externalBetId
			},
			ExtendedData: new JsonObject { });

		var sut = new GameBetsWorkerRepository(
			fakeLogger,
			fakeOptions,
			fakeClient);

		await sut.CommandAsync(command: command);

		_ = fakeClient
			.Received(1)
			.HandleAsync(
			source: Arg.Is<GameBets.Worker.Common.ViewModels.GameBetHandleViewModel>(compare =>
				compare.GameBetId == command.GameBetId &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.ResellerId == command.ResellerId &&
				compare.LicenseeId == command.LicenseeId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.WalletId == command.WalletId &&
				compare.ExternalRoundId == command.ExternalRoundId &&
				compare.ExternalBetId == command.ExternalBetId &&
				compare.ExternalCreatedOn == command.ExternalCreatedOn &&
				compare.ExternalClosedOn == command.ExternalClosedOn &&
				compare.ExternalPayoffOn == command.ExternalPayoffOn &&
				compare.ExternalResettleOn == command.ExternalResettleOn &&
				compare.BetState == GameBets.Worker.Common.Protos.BetState.Loss &&
				compare.BetType == GameBets.Worker.Common.Protos.BetType.Normal &&
				compare.FreeGameState == GameBets.Worker.Common.Protos.FreeGameState.None &&
				compare.GambleState == GameBets.Worker.Common.Protos.GambleState.None &&
				compare.CashoutState == GameBets.Worker.Common.Protos.CashoutState.None &&
				compare.ResettleState == GameBets.Worker.Common.Protos.ResettleState.None &&
				compare.JackpotState == GameBets.Worker.Common.Protos.JackpotState.None &&
				compare.CurrencyRatio == command.CurrencyRatio &&
				compare.Bet == command.Bet &&
				compare.ValidBet == command.ValidBet &&
				compare.Settle == command.Settle &&
				compare.WinLoss == command.WinLoss &&
				compare.Turnover == command.Turnover &&
				compare.Commission == command.Commission &&
				compare.Contribute == command.Contribute &&
				compare.Jackpot == command.Jackpot &&
				compare.CreatedOn == command.CreatedOn &&
				compare.ClosedOn == command.ClosedOn &&
				compare.AdditionalData == command.AdditionalData &&
				compare.ExtendedData == command.ExtendedData),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Draw()
	{
		var fakeLogger = NullLogger<GameBetsWorkerRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsWorkerClient>();

		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalRoundId = externalCreatedOn.ToUnixTimeMilliseconds().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;
		var createdOn = DateTimeOffset.UtcNow.AddMinutes(-4);
		var closedOn = DateTimeOffset.UtcNow.AddSeconds(-4);

		var command = new GameBetWorkCommand(
			GameBetId: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: externalRoundId,
			ExternalBetId: externalBetId,
			ExternalCreatedOn: externalCreatedOn,
			ExternalClosedOn: externalClosedOn,
			ExternalPayoffOn: externalPayoffOn,
			ExternalResettleOn: DateTimeOffset.UnixEpoch,
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
			Commission: 0.1m,
			Contribute: 0.01m,
			Jackpot: 0.0m,
			CreatedOn: createdOn,
			ClosedOn: closedOn,
			AdditionalData: new JsonObject
			{
				["ExternalRoundId"] = externalRoundId,
				["ExternalBetId"] = externalBetId
			},
			ExtendedData: new JsonObject { });

		var sut = new GameBetsWorkerRepository(
			fakeLogger,
			fakeOptions,
			fakeClient);

		await sut.CommandAsync(command: command);

		_ = fakeClient
			.Received(1)
			.HandleAsync(
			source: Arg.Is<GameBets.Worker.Common.ViewModels.GameBetHandleViewModel>(compare =>
				compare.GameBetId == command.GameBetId &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.ResellerId == command.ResellerId &&
				compare.LicenseeId == command.LicenseeId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.WalletId == command.WalletId &&
				compare.ExternalRoundId == command.ExternalRoundId &&
				compare.ExternalBetId == command.ExternalBetId &&
				compare.ExternalCreatedOn == command.ExternalCreatedOn &&
				compare.ExternalClosedOn == command.ExternalClosedOn &&
				compare.ExternalPayoffOn == command.ExternalPayoffOn &&
				compare.ExternalResettleOn == command.ExternalResettleOn &&
				compare.BetState == GameBets.Worker.Common.Protos.BetState.Draw &&
				compare.BetType == GameBets.Worker.Common.Protos.BetType.Normal &&
				compare.FreeGameState == GameBets.Worker.Common.Protos.FreeGameState.None &&
				compare.GambleState == GameBets.Worker.Common.Protos.GambleState.None &&
				compare.CashoutState == GameBets.Worker.Common.Protos.CashoutState.None &&
				compare.ResettleState == GameBets.Worker.Common.Protos.ResettleState.None &&
				compare.JackpotState == GameBets.Worker.Common.Protos.JackpotState.None &&
				compare.CurrencyRatio == command.CurrencyRatio &&
				compare.Bet == command.Bet &&
				compare.ValidBet == command.ValidBet &&
				compare.Settle == command.Settle &&
				compare.WinLoss == command.WinLoss &&
				compare.Turnover == command.Turnover &&
				compare.Commission == command.Commission &&
				compare.Contribute == command.Contribute &&
				compare.Jackpot == command.Jackpot &&
				compare.CreatedOn == command.CreatedOn &&
				compare.ClosedOn == command.ClosedOn &&
				compare.AdditionalData == command.AdditionalData &&
				compare.ExtendedData == command.ExtendedData),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_HalfWin()
	{
		var fakeLogger = NullLogger<GameBetsWorkerRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsWorkerClient>();

		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalRoundId = externalCreatedOn.ToUnixTimeMilliseconds().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;
		var createdOn = DateTimeOffset.UtcNow.AddMinutes(-4);
		var closedOn = DateTimeOffset.UtcNow.AddSeconds(-4);

		var command = new GameBetWorkCommand(
			GameBetId: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: externalRoundId,
			ExternalBetId: externalBetId,
			ExternalCreatedOn: externalCreatedOn,
			ExternalClosedOn: externalClosedOn,
			ExternalPayoffOn: externalPayoffOn,
			ExternalResettleOn: DateTimeOffset.UnixEpoch,
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
			Turnover: 4.0m,
			Commission: 0.1m,
			Contribute: 0.01m,
			Jackpot: 0.0m,
			CreatedOn: createdOn,
			ClosedOn: closedOn,
			AdditionalData: new JsonObject
			{
				["ExternalRoundId"] = externalRoundId,
				["ExternalBetId"] = externalBetId
			},
			ExtendedData: new JsonObject { });

		var sut = new GameBetsWorkerRepository(
			fakeLogger,
			fakeOptions,
			fakeClient);

		await sut.CommandAsync(command: command);

		_ = fakeClient
			.Received(1)
			.HandleAsync(
			source: Arg.Is<GameBets.Worker.Common.ViewModels.GameBetHandleViewModel>(compare =>
				compare.GameBetId == command.GameBetId &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.ResellerId == command.ResellerId &&
				compare.LicenseeId == command.LicenseeId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.WalletId == command.WalletId &&
				compare.ExternalRoundId == command.ExternalRoundId &&
				compare.ExternalBetId == command.ExternalBetId &&
				compare.ExternalCreatedOn == command.ExternalCreatedOn &&
				compare.ExternalClosedOn == command.ExternalClosedOn &&
				compare.ExternalPayoffOn == command.ExternalPayoffOn &&
				compare.ExternalResettleOn == command.ExternalResettleOn &&
				compare.BetState == GameBets.Worker.Common.Protos.BetState.HalfWin &&
				compare.BetType == GameBets.Worker.Common.Protos.BetType.Normal &&
				compare.FreeGameState == GameBets.Worker.Common.Protos.FreeGameState.None &&
				compare.GambleState == GameBets.Worker.Common.Protos.GambleState.None &&
				compare.CashoutState == GameBets.Worker.Common.Protos.CashoutState.None &&
				compare.ResettleState == GameBets.Worker.Common.Protos.ResettleState.None &&
				compare.JackpotState == GameBets.Worker.Common.Protos.JackpotState.None &&
				compare.CurrencyRatio == command.CurrencyRatio &&
				compare.Bet == command.Bet &&
				compare.ValidBet == command.ValidBet &&
				compare.Settle == command.Settle &&
				compare.WinLoss == command.WinLoss &&
				compare.Turnover == command.Turnover &&
				compare.Commission == command.Commission &&
				compare.Contribute == command.Contribute &&
				compare.Jackpot == command.Jackpot &&
				compare.CreatedOn == command.CreatedOn &&
				compare.ClosedOn == command.ClosedOn &&
				compare.AdditionalData == command.AdditionalData &&
				compare.ExtendedData == command.ExtendedData),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_HalfLoss()
	{
		var fakeLogger = NullLogger<GameBetsWorkerRepository>.Instance;
		var fakeOptions = Options.Create(new PolicySettingsOptions
		{
			RetryCount = 1,
			RetrySeconds = 1,
		});
		var fakeClient = Substitute.For<IGameBetsWorkerClient>();

		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalRoundId = externalCreatedOn.ToUnixTimeMilliseconds().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;
		var createdOn = DateTimeOffset.UtcNow.AddMinutes(-4);
		var closedOn = DateTimeOffset.UtcNow.AddSeconds(-4);

		var command = new GameBetWorkCommand(
			GameBetId: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: externalRoundId,
			ExternalBetId: externalBetId,
			ExternalCreatedOn: externalCreatedOn,
			ExternalClosedOn: externalClosedOn,
			ExternalPayoffOn: externalPayoffOn,
			ExternalResettleOn: DateTimeOffset.UnixEpoch,
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
			Turnover: 4.0m,
			Commission: 0.1m,
			Contribute: 0.01m,
			Jackpot: 0.0m,
			CreatedOn: createdOn,
			ClosedOn: closedOn,
			AdditionalData: new JsonObject
			{
				["ExternalRoundId"] = externalRoundId,
				["ExternalBetId"] = externalBetId
			},
			ExtendedData: new JsonObject { });

		var sut = new GameBetsWorkerRepository(
			fakeLogger,
			fakeOptions,
			fakeClient);

		await sut.CommandAsync(command: command);

		_ = fakeClient
			.Received(1)
			.HandleAsync(
			source: Arg.Is<GameBets.Worker.Common.ViewModels.GameBetHandleViewModel>(compare =>
				compare.GameBetId == command.GameBetId &&
				compare.GameProviderId == command.GameProviderId &&
				compare.GameProviderConfigurationId == command.GameProviderConfigurationId &&
				compare.GameId == command.GameId &&
				compare.ResellerId == command.ResellerId &&
				compare.LicenseeId == command.LicenseeId &&
				compare.BrandId == command.BrandId &&
				compare.PlayerCode == command.PlayerCode &&
				compare.CurrencyCode == command.CurrencyCode &&
				compare.WalletId == command.WalletId &&
				compare.ExternalRoundId == command.ExternalRoundId &&
				compare.ExternalBetId == command.ExternalBetId &&
				compare.ExternalCreatedOn == command.ExternalCreatedOn &&
				compare.ExternalClosedOn == command.ExternalClosedOn &&
				compare.ExternalPayoffOn == command.ExternalPayoffOn &&
				compare.ExternalResettleOn == command.ExternalResettleOn &&
				compare.BetState == GameBets.Worker.Common.Protos.BetState.HalfLoss &&
				compare.BetType == GameBets.Worker.Common.Protos.BetType.Normal &&
				compare.FreeGameState == GameBets.Worker.Common.Protos.FreeGameState.None &&
				compare.GambleState == GameBets.Worker.Common.Protos.GambleState.None &&
				compare.CashoutState == GameBets.Worker.Common.Protos.CashoutState.None &&
				compare.ResettleState == GameBets.Worker.Common.Protos.ResettleState.None &&
				compare.JackpotState == GameBets.Worker.Common.Protos.JackpotState.None &&
				compare.CurrencyRatio == command.CurrencyRatio &&
				compare.Bet == command.Bet &&
				compare.ValidBet == command.ValidBet &&
				compare.Settle == command.Settle &&
				compare.WinLoss == command.WinLoss &&
				compare.Turnover == command.Turnover &&
				compare.Commission == command.Commission &&
				compare.Contribute == command.Contribute &&
				compare.Jackpot == command.Jackpot &&
				compare.CreatedOn == command.CreatedOn &&
				compare.ClosedOn == command.ClosedOn &&
				compare.AdditionalData == command.AdditionalData &&
				compare.ExtendedData == command.ExtendedData),
			cancellationToken: Arg.Any<CancellationToken>());
	}
}
