using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameBets.RapidTest.Worker.Client;

using Valhalla.MessageQueue;

namespace Tgs.GameBets.RapidTest.Worker.IntegrationTest;

public class GameBetsRapidTestWorkerClientUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var source = new Common.ViewModels.GameBetRapidTestWorkerHandleViewModel
		{
			GameProviderId = Guid.NewGuid(),
			GameProviderCode = "LY",
			SubGameProviderCode = "LY",
			GameProviderConfigurationId = Guid.NewGuid(),
			GameId = 1,
			ResellerId = Guid.NewGuid(),
			ResellerCode = "TGS",
			LicenseeId = Guid.NewGuid(),
			LicenseeCode = "TGS",
			BrandId = Guid.NewGuid(),
			BrandCode = "GPI",
			PlayerCode = "test",
			Username = "test",
			CurrencyCode = "CNY",
			WalletId = Guid.NewGuid(),
			ExternalRoundId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(),
			ExternalBetId = Guid.NewGuid().ToString(),
			ExternalGameId = "901",
			ExternalCreatedOn = DateTimeOffset.UtcNow.AddSeconds(-10),
			ExternalClosedOn = DateTimeOffset.UtcNow,
			ExternalPayoffOn = DateTimeOffset.UtcNow,
			ExternalResettleOn = DateTimeOffset.UnixEpoch,
			BetState = Common.Protos.BetState.Win,
			BetType = Common.Protos.BetType.Normal,
			FreeGameState = Common.Protos.FreeGameState.None,
			GambleState = Common.Protos.GambleState.None,
			CashoutState = Common.Protos.CashoutState.None,
			ResettleState = Common.Protos.ResettleState.None,
			JackpotState = Common.Protos.JackpotState.None,
			CurrencyRatio = 1.0m,
			Bet = 2.0m,
			ValidBet = 2.0m,
			Settle = 4.0m,
			Commission = 0.0m,
			Turnover = 2.0m,
			WinLoss = 2.0m,
			Jackpot = 0.0m,
			AdditionalData = "{\"AllBet\": 2, \"GameID\": \"888-1722937124-92240583-21\", \"KindID\": 910, \"Profit\": 2, \"ChairID\": 0, \"Revenue\": 0, \"TableID\": 182020001, \"Accounts\": \"200995_4un4b9l34304\", \"LineCode\": \"200995_GPI\", \"ServerID\": 9101, \"CardValue\": \"0c32050b06001\", \"CellScore\": 2, \"ChannelID\": 200995, \"UserCount\": 1, \"GameEndTime\": \"2024-08-06T17:39:03+08:00\", \"GameStartTime\": \"2024-08-06T17:38:44+08:00\"}"
		};

		var fakeMessageSender = Substitute.For<IMessageSender>();
		var web = new GameBetsRapidTestWorkerApplication(builder => _ = builder.ConfigureServices(services => _ = services.AddTransient(_ => fakeMessageSender)));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestWorkerClient(httpClient);

		await client.HandleAsync(source);

		_ = fakeMessageSender
			.Received(1)
			.PublishAsync(
			subject: Arg.Is(GameBetsRapidTestWorkerNatsConst.GameBetsRapidTestWorkerRequest),
			data: Arg.Is(new Common.Protos.GameBetsRapidTestWorkerRequest
			{
				GameProviderId = source.GameProviderId,
				GameProviderCode = source.GameProviderCode,
				SubGameProviderCode = source.SubGameProviderCode,
				GameProviderConfigurationId = source.GameProviderConfigurationId,
				GameId = source.GameId,
				ResellerId = source.ResellerId,
				ResellerCode = source.ResellerCode,
				LicenseeId = source.LicenseeId,
				LicenseeCode = source.LicenseeCode,
				BrandId = source.BrandId,
				BrandCode = source.BrandCode,
				PlayerCode = source.PlayerCode,
				Username = source.Username,
				CurrencyCode = source.CurrencyCode,
				WalletId = source.WalletId,
				ExternalRoundId = source.ExternalRoundId,
				ExternalBetId = source.ExternalBetId,
				ExternalGameId = source.ExternalGameId,
				ExternalCreatedOn = source.ExternalCreatedOn.ToTimestamp(),
				ExternalClosedOn = source.ExternalClosedOn.ToTimestamp(),
				ExternalPayoffOn = source.ExternalPayoffOn.ToTimestamp(),
				ExternalResettleOn = source.ExternalResettleOn.ToTimestamp(),
				BetState = source.BetState,
				BetType = source.BetType,
				FreeGameState = source.FreeGameState,
				GambleState = source.GambleState,
				CashoutState = source.CashoutState,
				ResettleState = source.ResettleState,
				JackpotState = source.JackpotState,
				CurrencyRatio = source.CurrencyRatio,
				Bet = source.Bet,
				ValidBet = source.ValidBet,
				Settle = source.Settle,
				Commission = source.Commission,
				Turnover = source.Turnover,
				WinLoss = source.WinLoss,
				Jackpot = source.Jackpot,
				AdditionalData = source.AdditionalData
			}),
			cancellationToken: Arg.Any<CancellationToken>());

		_ = fakeMessageSender
			.Received(1)
			.PublishAsync(
			subject: Arg.Is(GameBetsRapidTestWorkerNatsConst.GameBetsRapidTestWorkerExpiryRequest),
			data: Arg.Is(new Common.Protos.GameBetsRapidTestWorkerrExpiryRequest
			{
				GameProviderCode = source.GameProviderCode,
				GameId = source.GameId,
				ResellerCode = source.ResellerCode,
				LicenseeCode = source.LicenseeCode,
				BrandCode = source.BrandCode
			}),
			cancellationToken: Arg.Any<CancellationToken>());
	}
}
