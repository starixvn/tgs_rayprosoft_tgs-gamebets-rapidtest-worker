using System.Text.Json.Nodes;

using Google.Protobuf.WellKnownTypes;
using NSubstitute;
using Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

using Valhalla.MessageQueue;

namespace Tgs.GameBets.RapidTest.Worker.Core.UnitTest.ApplicationServices;

public class GameBetCommandServiceUnitTest
{
	[Fact]
	public async Task BetState_Is_Waiting()
	{
		var fakerIMessageSender = Substitute.For<IMessageSender>();

		var command = new GameBetCommand(
			Id: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.UnixEpoch,
			ExternalPayoffOn: DateTimeOffset.UnixEpoch,
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
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var sut = new GameBetCommandService(
			fakerIMessageSender);

		await sut.ExecuteAsync(command: command);

		_ = fakerIMessageSender
			.Received(0)
			.PublishAsync(
			subject: Arg.Is(GameBets.Worker.Common.GameBetsWorkerNatsConst.GameBetsWorkerRequest),
			data: Arg.Any<GameBets.Worker.Common.Protos.GameBetsWorkerRequest>(),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Running()
	{
		var fakerIMessageSender = Substitute.For<IMessageSender>();

		var command = new GameBetCommand(
			Id: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.UnixEpoch,
			ExternalPayoffOn: DateTimeOffset.UnixEpoch,
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
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var sut = new GameBetCommandService(
			fakerIMessageSender);

		await sut.ExecuteAsync(command: command);

		_ = fakerIMessageSender
			.Received(0)
			.PublishAsync(
			subject: Arg.Is(GameBets.Worker.Common.GameBetsWorkerNatsConst.GameBetsWorkerRequest),
			data: Arg.Any<GameBets.Worker.Common.Protos.GameBetsWorkerRequest>(),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Void()
	{
		var fakerIMessageSender = Substitute.For<IMessageSender>();

		var command = new GameBetCommand(
			Id: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			ExternalPayoffOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
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
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var sut = new GameBetCommandService(
			fakerIMessageSender);

		await sut.ExecuteAsync(command: command);

		_ = fakerIMessageSender
			.Received(1)
			.PublishAsync(
			subject: Arg.Is(GameBets.Worker.Common.GameBetsWorkerNatsConst.GameBetsWorkerRequest),
			data: Arg.Is(new GameBets.Worker.Common.Protos.GameBetsWorkerRequest
			{
				GameBetId = command.Id,
				GameProviderId = command.GameProviderId,
				GameProviderConfigurationId = command.GameProviderConfigurationId,
				GameId = command.GameId,
				ResellerId = command.ResellerId,
				LicenseeId = command.LicenseeId,
				BrandId = command.BrandId,
				PlayerCode = command.PlayerCode,
				CurrencyCode = command.CurrencyCode,
				WalletId = command.WalletId,
				ExternalRoundId = command.ExternalRoundId,
				ExternalBetId = command.ExternalBetId,
				ExternalCreatedOn = command.ExternalCreatedOn.ToTimestamp(),
				ExternalClosedOn = command.ExternalClosedOn.ToTimestamp(),
				ExternalPayoffOn = command.ExternalPayoffOn.ToTimestamp(),
				ExternalResettleOn = command.ExternalResettleOn.ToTimestamp(),
				BetState = GameBets.Worker.Common.Protos.BetState.Void,
				BetType = GameBets.Worker.Common.Protos.BetType.Normal,
				FreeGameState = GameBets.Worker.Common.Protos.FreeGameState.None,
				GambleState = GameBets.Worker.Common.Protos.GambleState.None,
				CashoutState = GameBets.Worker.Common.Protos.CashoutState.None,
				ResettleState = GameBets.Worker.Common.Protos.ResettleState.None,
				JackpotState = GameBets.Worker.Common.Protos.JackpotState.None,
				CurrencyRatio = command.CurrencyRatio,
				Bet = command.Bet,
				ValidBet = command.ValidBet,
				Settle = command.Settle,
				Commission = command.Commission,
				Turnover = command.Turnover,
				WinLoss = command.WinLoss,
				Jackpot = command.Jackpot,
				AdditionalData = command.AdditionalData.ToJsonString(),
			}),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Refund()
	{
		var fakerIMessageSender = Substitute.For<IMessageSender>();

		var command = new GameBetCommand(
			Id: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			ExternalPayoffOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
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
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var sut = new GameBetCommandService(
			fakerIMessageSender);

		await sut.ExecuteAsync(command: command);

		_ = fakerIMessageSender
			.Received(1)
			.PublishAsync(
			subject: Arg.Is(GameBets.Worker.Common.GameBetsWorkerNatsConst.GameBetsWorkerRequest),
			data: Arg.Is(new GameBets.Worker.Common.Protos.GameBetsWorkerRequest
			{
				GameBetId = command.Id,
				GameProviderId = command.GameProviderId,
				GameProviderConfigurationId = command.GameProviderConfigurationId,
				GameId = command.GameId,
				ResellerId = command.ResellerId,
				LicenseeId = command.LicenseeId,
				BrandId = command.BrandId,
				PlayerCode = command.PlayerCode,
				CurrencyCode = command.CurrencyCode,
				WalletId = command.WalletId,
				ExternalRoundId = command.ExternalRoundId,
				ExternalBetId = command.ExternalBetId,
				ExternalCreatedOn = command.ExternalCreatedOn.ToTimestamp(),
				ExternalClosedOn = command.ExternalClosedOn.ToTimestamp(),
				ExternalPayoffOn = command.ExternalPayoffOn.ToTimestamp(),
				ExternalResettleOn = command.ExternalResettleOn.ToTimestamp(),
				BetState = GameBets.Worker.Common.Protos.BetState.Refund,
				BetType = GameBets.Worker.Common.Protos.BetType.Normal,
				FreeGameState = GameBets.Worker.Common.Protos.FreeGameState.None,
				GambleState = GameBets.Worker.Common.Protos.GambleState.None,
				CashoutState = GameBets.Worker.Common.Protos.CashoutState.None,
				ResettleState = GameBets.Worker.Common.Protos.ResettleState.None,
				JackpotState = GameBets.Worker.Common.Protos.JackpotState.None,
				CurrencyRatio = command.CurrencyRatio,
				Bet = command.Bet,
				ValidBet = command.ValidBet,
				Settle = command.Settle,
				Commission = command.Commission,
				Turnover = command.Turnover,
				WinLoss = command.WinLoss,
				Jackpot = command.Jackpot,
				AdditionalData = command.AdditionalData.ToJsonString(),
			}),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Refund_And_Cashout()
	{
		var fakerIMessageSender = Substitute.For<IMessageSender>();

		var command = new GameBetCommand(
			Id: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			ExternalPayoffOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
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
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var sut = new GameBetCommandService(
			fakerIMessageSender);

		await sut.ExecuteAsync(command: command);

		_ = fakerIMessageSender
			.Received(1)
			.PublishAsync(
			subject: Arg.Is(GameBets.Worker.Common.GameBetsWorkerNatsConst.GameBetsWorkerRequest),
			data: Arg.Is(new GameBets.Worker.Common.Protos.GameBetsWorkerRequest
			{
				GameBetId = command.Id,
				GameProviderId = command.GameProviderId,
				GameProviderConfigurationId = command.GameProviderConfigurationId,
				GameId = command.GameId,
				ResellerId = command.ResellerId,
				LicenseeId = command.LicenseeId,
				BrandId = command.BrandId,
				PlayerCode = command.PlayerCode,
				CurrencyCode = command.CurrencyCode,
				WalletId = command.WalletId,
				ExternalRoundId = command.ExternalRoundId,
				ExternalBetId = command.ExternalBetId,
				ExternalCreatedOn = command.ExternalCreatedOn.ToTimestamp(),
				ExternalClosedOn = command.ExternalClosedOn.ToTimestamp(),
				ExternalPayoffOn = command.ExternalPayoffOn.ToTimestamp(),
				ExternalResettleOn = command.ExternalResettleOn.ToTimestamp(),
				BetState = GameBets.Worker.Common.Protos.BetState.Refund,
				BetType = GameBets.Worker.Common.Protos.BetType.Normal,
				FreeGameState = GameBets.Worker.Common.Protos.FreeGameState.None,
				GambleState = GameBets.Worker.Common.Protos.GambleState.None,
				CashoutState = GameBets.Worker.Common.Protos.CashoutState.Cashout,
				ResettleState = GameBets.Worker.Common.Protos.ResettleState.None,
				JackpotState = GameBets.Worker.Common.Protos.JackpotState.None,
				CurrencyRatio = command.CurrencyRatio,
				Bet = command.Bet,
				ValidBet = command.ValidBet,
				Settle = command.Settle,
				Commission = command.Commission,
				Turnover = command.Turnover,
				WinLoss = command.WinLoss,
				Jackpot = command.Jackpot,
				AdditionalData = command.AdditionalData.ToJsonString(),
			}),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Cancel()
	{
		var fakerIMessageSender = Substitute.For<IMessageSender>();

		var command = new GameBetCommand(
			Id: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			ExternalPayoffOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
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
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var sut = new GameBetCommandService(
			fakerIMessageSender);

		await sut.ExecuteAsync(command: command);

		_ = fakerIMessageSender
			.Received(1)
			.PublishAsync(
			subject: Arg.Is(GameBets.Worker.Common.GameBetsWorkerNatsConst.GameBetsWorkerRequest),
			data: Arg.Is(new GameBets.Worker.Common.Protos.GameBetsWorkerRequest
			{
				GameBetId = command.Id,
				GameProviderId = command.GameProviderId,
				GameProviderConfigurationId = command.GameProviderConfigurationId,
				GameId = command.GameId,
				ResellerId = command.ResellerId,
				LicenseeId = command.LicenseeId,
				BrandId = command.BrandId,
				PlayerCode = command.PlayerCode,
				CurrencyCode = command.CurrencyCode,
				WalletId = command.WalletId,
				ExternalRoundId = command.ExternalRoundId,
				ExternalBetId = command.ExternalBetId,
				ExternalCreatedOn = command.ExternalCreatedOn.ToTimestamp(),
				ExternalClosedOn = command.ExternalClosedOn.ToTimestamp(),
				ExternalPayoffOn = command.ExternalPayoffOn.ToTimestamp(),
				ExternalResettleOn = command.ExternalResettleOn.ToTimestamp(),
				BetState = GameBets.Worker.Common.Protos.BetState.Cancel,
				BetType = GameBets.Worker.Common.Protos.BetType.Normal,
				FreeGameState = GameBets.Worker.Common.Protos.FreeGameState.None,
				GambleState = GameBets.Worker.Common.Protos.GambleState.None,
				CashoutState = GameBets.Worker.Common.Protos.CashoutState.None,
				ResettleState = GameBets.Worker.Common.Protos.ResettleState.None,
				JackpotState = GameBets.Worker.Common.Protos.JackpotState.None,
				CurrencyRatio = command.CurrencyRatio,
				Bet = command.Bet,
				ValidBet = command.ValidBet,
				Settle = command.Settle,
				Commission = command.Commission,
				Turnover = command.Turnover,
				WinLoss = command.WinLoss,
				Jackpot = command.Jackpot,
				AdditionalData = command.AdditionalData.ToJsonString(),
			}),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Win()
	{
		var fakerIMessageSender = Substitute.For<IMessageSender>();

		var command = new GameBetCommand(
			Id: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
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

		var sut = new GameBetCommandService(
			fakerIMessageSender);

		await sut.ExecuteAsync(command: command);

		_ = fakerIMessageSender
			.Received(1)
			.PublishAsync(
			subject: Arg.Is(GameBets.Worker.Common.GameBetsWorkerNatsConst.GameBetsWorkerRequest),
			data: Arg.Is(new GameBets.Worker.Common.Protos.GameBetsWorkerRequest
			{
				GameBetId = command.Id,
				GameProviderId = command.GameProviderId,
				GameProviderConfigurationId = command.GameProviderConfigurationId,
				GameId = command.GameId,
				ResellerId = command.ResellerId,
				LicenseeId = command.LicenseeId,
				BrandId = command.BrandId,
				PlayerCode = command.PlayerCode,
				CurrencyCode = command.CurrencyCode,
				WalletId = command.WalletId,
				ExternalRoundId = command.ExternalRoundId,
				ExternalBetId = command.ExternalBetId,
				ExternalCreatedOn = command.ExternalCreatedOn.ToTimestamp(),
				ExternalClosedOn = command.ExternalClosedOn.ToTimestamp(),
				ExternalPayoffOn = command.ExternalPayoffOn.ToTimestamp(),
				ExternalResettleOn = command.ExternalResettleOn.ToTimestamp(),
				BetState = GameBets.Worker.Common.Protos.BetState.Win,
				BetType = GameBets.Worker.Common.Protos.BetType.Normal,
				FreeGameState = GameBets.Worker.Common.Protos.FreeGameState.None,
				GambleState = GameBets.Worker.Common.Protos.GambleState.None,
				CashoutState = GameBets.Worker.Common.Protos.CashoutState.None,
				ResettleState = GameBets.Worker.Common.Protos.ResettleState.None,
				JackpotState = GameBets.Worker.Common.Protos.JackpotState.None,
				CurrencyRatio = command.CurrencyRatio,
				Bet = command.Bet,
				ValidBet = command.ValidBet,
				Settle = command.Settle,
				Commission = command.Commission,
				Turnover = command.Turnover,
				WinLoss = command.WinLoss,
				Jackpot = command.Jackpot,
				AdditionalData = command.AdditionalData.ToJsonString(),
			}),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Win_And_FreeGame()
	{
		var fakerIMessageSender = Substitute.For<IMessageSender>();

		var command = new GameBetCommand(
			Id: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			ExternalPayoffOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
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
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: 4.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var sut = new GameBetCommandService(
			fakerIMessageSender);

		await sut.ExecuteAsync(command: command);

		_ = fakerIMessageSender
			.Received(1)
			.PublishAsync(
			subject: Arg.Is(GameBets.Worker.Common.GameBetsWorkerNatsConst.GameBetsWorkerRequest),
			data: Arg.Is(new GameBets.Worker.Common.Protos.GameBetsWorkerRequest
			{
				GameBetId = command.Id,
				GameProviderId = command.GameProviderId,
				GameProviderConfigurationId = command.GameProviderConfigurationId,
				GameId = command.GameId,
				ResellerId = command.ResellerId,
				LicenseeId = command.LicenseeId,
				BrandId = command.BrandId,
				PlayerCode = command.PlayerCode,
				CurrencyCode = command.CurrencyCode,
				WalletId = command.WalletId,
				ExternalRoundId = command.ExternalRoundId,
				ExternalBetId = command.ExternalBetId,
				ExternalCreatedOn = command.ExternalCreatedOn.ToTimestamp(),
				ExternalClosedOn = command.ExternalClosedOn.ToTimestamp(),
				ExternalPayoffOn = command.ExternalPayoffOn.ToTimestamp(),
				ExternalResettleOn = command.ExternalResettleOn.ToTimestamp(),
				BetState = GameBets.Worker.Common.Protos.BetState.Win,
				BetType = GameBets.Worker.Common.Protos.BetType.Normal,
				FreeGameState = GameBets.Worker.Common.Protos.FreeGameState.FreeGame,
				GambleState = GameBets.Worker.Common.Protos.GambleState.None,
				CashoutState = GameBets.Worker.Common.Protos.CashoutState.None,
				ResettleState = GameBets.Worker.Common.Protos.ResettleState.None,
				JackpotState = GameBets.Worker.Common.Protos.JackpotState.None,
				CurrencyRatio = command.CurrencyRatio,
				Bet = command.Bet,
				ValidBet = command.ValidBet,
				Settle = command.Settle,
				Commission = command.Commission,
				Turnover = command.Turnover,
				WinLoss = command.WinLoss,
				Jackpot = command.Jackpot,
				AdditionalData = command.AdditionalData.ToJsonString(),
			}),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Win_And_Jackpot()
	{
		var fakerIMessageSender = Substitute.For<IMessageSender>();

		var command = new GameBetCommand(
			Id: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
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
			JackpotState: GameBets.Common.Enums.JackpotState.Jackpot,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 108.0m,
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: 4.0m,
			Jackpot: 100.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var sut = new GameBetCommandService(
			fakerIMessageSender);

		await sut.ExecuteAsync(command: command);

		_ = fakerIMessageSender
			.Received(1)
			.PublishAsync(
			subject: Arg.Is(GameBets.Worker.Common.GameBetsWorkerNatsConst.GameBetsWorkerRequest),
			data: Arg.Is(new GameBets.Worker.Common.Protos.GameBetsWorkerRequest
			{
				GameBetId = command.Id,
				GameProviderId = command.GameProviderId,
				GameProviderConfigurationId = command.GameProviderConfigurationId,
				GameId = command.GameId,
				ResellerId = command.ResellerId,
				LicenseeId = command.LicenseeId,
				BrandId = command.BrandId,
				PlayerCode = command.PlayerCode,
				CurrencyCode = command.CurrencyCode,
				WalletId = command.WalletId,
				ExternalRoundId = command.ExternalRoundId,
				ExternalBetId = command.ExternalBetId,
				ExternalCreatedOn = command.ExternalCreatedOn.ToTimestamp(),
				ExternalClosedOn = command.ExternalClosedOn.ToTimestamp(),
				ExternalPayoffOn = command.ExternalPayoffOn.ToTimestamp(),
				ExternalResettleOn = command.ExternalResettleOn.ToTimestamp(),
				BetState = GameBets.Worker.Common.Protos.BetState.Win,
				BetType = GameBets.Worker.Common.Protos.BetType.Normal,
				FreeGameState = GameBets.Worker.Common.Protos.FreeGameState.None,
				GambleState = GameBets.Worker.Common.Protos.GambleState.None,
				CashoutState = GameBets.Worker.Common.Protos.CashoutState.None,
				ResettleState = GameBets.Worker.Common.Protos.ResettleState.None,
				JackpotState = GameBets.Worker.Common.Protos.JackpotState.Jackpot,
				CurrencyRatio = command.CurrencyRatio,
				Bet = command.Bet,
				ValidBet = command.ValidBet,
				Settle = command.Settle,
				Commission = command.Commission,
				Turnover = command.Turnover,
				WinLoss = command.WinLoss,
				Jackpot = command.Jackpot,
				AdditionalData = command.AdditionalData.ToJsonString(),
			}),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Loss()
	{
		var fakerIMessageSender = Substitute.For<IMessageSender>();

		var command = new GameBetCommand(
			Id: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			ExternalPayoffOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
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
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: -4.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var sut = new GameBetCommandService(
			fakerIMessageSender);

		await sut.ExecuteAsync(command: command);

		_ = fakerIMessageSender
			.Received(1)
			.PublishAsync(
			subject: Arg.Is(GameBets.Worker.Common.GameBetsWorkerNatsConst.GameBetsWorkerRequest),
			data: Arg.Is(new GameBets.Worker.Common.Protos.GameBetsWorkerRequest
			{
				GameBetId = command.Id,
				GameProviderId = command.GameProviderId,
				GameProviderConfigurationId = command.GameProviderConfigurationId,
				GameId = command.GameId,
				ResellerId = command.ResellerId,
				LicenseeId = command.LicenseeId,
				BrandId = command.BrandId,
				PlayerCode = command.PlayerCode,
				CurrencyCode = command.CurrencyCode,
				WalletId = command.WalletId,
				ExternalRoundId = command.ExternalRoundId,
				ExternalBetId = command.ExternalBetId,
				ExternalCreatedOn = command.ExternalCreatedOn.ToTimestamp(),
				ExternalClosedOn = command.ExternalClosedOn.ToTimestamp(),
				ExternalPayoffOn = command.ExternalPayoffOn.ToTimestamp(),
				ExternalResettleOn = command.ExternalResettleOn.ToTimestamp(),
				BetState = GameBets.Worker.Common.Protos.BetState.Loss,
				BetType = GameBets.Worker.Common.Protos.BetType.Normal,
				FreeGameState = GameBets.Worker.Common.Protos.FreeGameState.None,
				GambleState = GameBets.Worker.Common.Protos.GambleState.None,
				CashoutState = GameBets.Worker.Common.Protos.CashoutState.None,
				ResettleState = GameBets.Worker.Common.Protos.ResettleState.None,
				JackpotState = GameBets.Worker.Common.Protos.JackpotState.None,
				CurrencyRatio = command.CurrencyRatio,
				Bet = command.Bet,
				ValidBet = command.ValidBet,
				Settle = command.Settle,
				Commission = command.Commission,
				Turnover = command.Turnover,
				WinLoss = command.WinLoss,
				Jackpot = command.Jackpot,
				AdditionalData = command.AdditionalData.ToJsonString(),
			}),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_Draw()
	{
		var fakerIMessageSender = Substitute.For<IMessageSender>();

		var command = new GameBetCommand(
			Id: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			ExternalPayoffOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
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
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var sut = new GameBetCommandService(
			fakerIMessageSender);

		await sut.ExecuteAsync(command: command);

		_ = fakerIMessageSender
			.Received(1)
			.PublishAsync(
			subject: Arg.Is(GameBets.Worker.Common.GameBetsWorkerNatsConst.GameBetsWorkerRequest),
			data: Arg.Is(new GameBets.Worker.Common.Protos.GameBetsWorkerRequest
			{
				GameBetId = command.Id,
				GameProviderId = command.GameProviderId,
				GameProviderConfigurationId = command.GameProviderConfigurationId,
				GameId = command.GameId,
				ResellerId = command.ResellerId,
				LicenseeId = command.LicenseeId,
				BrandId = command.BrandId,
				PlayerCode = command.PlayerCode,
				CurrencyCode = command.CurrencyCode,
				WalletId = command.WalletId,
				ExternalRoundId = command.ExternalRoundId,
				ExternalBetId = command.ExternalBetId,
				ExternalCreatedOn = command.ExternalCreatedOn.ToTimestamp(),
				ExternalClosedOn = command.ExternalClosedOn.ToTimestamp(),
				ExternalPayoffOn = command.ExternalPayoffOn.ToTimestamp(),
				ExternalResettleOn = command.ExternalResettleOn.ToTimestamp(),
				BetState = GameBets.Worker.Common.Protos.BetState.Draw,
				BetType = GameBets.Worker.Common.Protos.BetType.Normal,
				FreeGameState = GameBets.Worker.Common.Protos.FreeGameState.None,
				GambleState = GameBets.Worker.Common.Protos.GambleState.None,
				CashoutState = GameBets.Worker.Common.Protos.CashoutState.None,
				ResettleState = GameBets.Worker.Common.Protos.ResettleState.None,
				JackpotState = GameBets.Worker.Common.Protos.JackpotState.None,
				CurrencyRatio = command.CurrencyRatio,
				Bet = command.Bet,
				ValidBet = command.ValidBet,
				Settle = command.Settle,
				Commission = command.Commission,
				Turnover = command.Turnover,
				WinLoss = command.WinLoss,
				Jackpot = command.Jackpot,
				AdditionalData = command.AdditionalData.ToJsonString(),
			}),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_HalfWin()
	{
		var fakerIMessageSender = Substitute.For<IMessageSender>();

		var command = new GameBetCommand(
			Id: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			ExternalPayoffOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
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
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: -2.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var sut = new GameBetCommandService(
			fakerIMessageSender);

		await sut.ExecuteAsync(command: command);

		_ = fakerIMessageSender
			.Received(1)
			.PublishAsync(
			subject: Arg.Is(GameBets.Worker.Common.GameBetsWorkerNatsConst.GameBetsWorkerRequest),
			data: Arg.Is(new GameBets.Worker.Common.Protos.GameBetsWorkerRequest
			{
				GameBetId = command.Id,
				GameProviderId = command.GameProviderId,
				GameProviderConfigurationId = command.GameProviderConfigurationId,
				GameId = command.GameId,
				ResellerId = command.ResellerId,
				LicenseeId = command.LicenseeId,
				BrandId = command.BrandId,
				PlayerCode = command.PlayerCode,
				CurrencyCode = command.CurrencyCode,
				WalletId = command.WalletId,
				ExternalRoundId = command.ExternalRoundId,
				ExternalBetId = command.ExternalBetId,
				ExternalCreatedOn = command.ExternalCreatedOn.ToTimestamp(),
				ExternalClosedOn = command.ExternalClosedOn.ToTimestamp(),
				ExternalPayoffOn = command.ExternalPayoffOn.ToTimestamp(),
				ExternalResettleOn = command.ExternalResettleOn.ToTimestamp(),
				BetState = GameBets.Worker.Common.Protos.BetState.HalfWin,
				BetType = GameBets.Worker.Common.Protos.BetType.Normal,
				FreeGameState = GameBets.Worker.Common.Protos.FreeGameState.None,
				GambleState = GameBets.Worker.Common.Protos.GambleState.None,
				CashoutState = GameBets.Worker.Common.Protos.CashoutState.None,
				ResettleState = GameBets.Worker.Common.Protos.ResettleState.None,
				JackpotState = GameBets.Worker.Common.Protos.JackpotState.None,
				CurrencyRatio = command.CurrencyRatio,
				Bet = command.Bet,
				ValidBet = command.ValidBet,
				Settle = command.Settle,
				Commission = command.Commission,
				Turnover = command.Turnover,
				WinLoss = command.WinLoss,
				Jackpot = command.Jackpot,
				AdditionalData = command.AdditionalData.ToJsonString(),
			}),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task BetState_Is_HalfLoss()
	{
		var fakerIMessageSender = Substitute.For<IMessageSender>();

		var command = new GameBetCommand(
			Id: Guid.NewGuid(),
			GameProviderId: Guid.NewGuid(),
			GameProviderConfigurationId: Guid.NewGuid(),
			GameId: 1000017,
			ResellerId: Guid.NewGuid(),
			LicenseeId: Guid.NewGuid(),
			BrandId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalCreatedOn: DateTimeOffset.Parse("2023-08-08 12:34:56.000 +0800"),
			ExternalClosedOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
			ExternalPayoffOn: DateTimeOffset.Parse("2023-08-08 12:35:18.000 +0800"),
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
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: -2.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			});

		var sut = new GameBetCommandService(
			fakerIMessageSender);

		await sut.ExecuteAsync(command: command);

		_ = fakerIMessageSender
			.Received(1)
			.PublishAsync(
			subject: Arg.Is(GameBets.Worker.Common.GameBetsWorkerNatsConst.GameBetsWorkerRequest),
			data: Arg.Is(new GameBets.Worker.Common.Protos.GameBetsWorkerRequest
			{
				GameBetId = command.Id,
				GameProviderId = command.GameProviderId,
				GameProviderConfigurationId = command.GameProviderConfigurationId,
				GameId = command.GameId,
				ResellerId = command.ResellerId,
				LicenseeId = command.LicenseeId,
				BrandId = command.BrandId,
				PlayerCode = command.PlayerCode,
				CurrencyCode = command.CurrencyCode,
				WalletId = command.WalletId,
				ExternalRoundId = command.ExternalRoundId,
				ExternalBetId = command.ExternalBetId,
				ExternalCreatedOn = command.ExternalCreatedOn.ToTimestamp(),
				ExternalClosedOn = command.ExternalClosedOn.ToTimestamp(),
				ExternalPayoffOn = command.ExternalPayoffOn.ToTimestamp(),
				ExternalResettleOn = command.ExternalResettleOn.ToTimestamp(),
				BetState = GameBets.Worker.Common.Protos.BetState.HalfLoss,
				BetType = GameBets.Worker.Common.Protos.BetType.Normal,
				FreeGameState = GameBets.Worker.Common.Protos.FreeGameState.None,
				GambleState = GameBets.Worker.Common.Protos.GambleState.None,
				CashoutState = GameBets.Worker.Common.Protos.CashoutState.None,
				ResettleState = GameBets.Worker.Common.Protos.ResettleState.None,
				JackpotState = GameBets.Worker.Common.Protos.JackpotState.None,
				CurrencyRatio = command.CurrencyRatio,
				Bet = command.Bet,
				ValidBet = command.ValidBet,
				Settle = command.Settle,
				Commission = command.Commission,
				Turnover = command.Turnover,
				WinLoss = command.WinLoss,
				Jackpot = command.Jackpot,
				AdditionalData = command.AdditionalData.ToJsonString(),
			}),
			cancellationToken: Arg.Any<CancellationToken>());
	}
}
