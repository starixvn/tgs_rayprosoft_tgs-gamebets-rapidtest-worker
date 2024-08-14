using Microsoft.Extensions.Logging;
using Tgs.ServiceUtility;

namespace Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

internal class GameBetRapidTestWorkerCommandService(
	ILogger<GameBetRapidTestWorkerCommandService> logger,
	IGetService<GameBetRapidTestCommand, bool> gameBetRapidTestService,
	IGetService<GameRoundCommand, Guid> gameRoundService,
	ICommandService<GameBetCommand> gameBetService) : ICommandService<GameBetRapidTestWorkerCommand>
{
	public async ValueTask ExecuteAsync(GameBetRapidTestWorkerCommand command, CancellationToken cancellationToken = default)
	{
		var isUpdate = await gameBetRapidTestService.GetAsync(
			query: new GameBetRapidTestCommand(
				command.GameProviderCode,
				command.SubGameProviderCode,
				command.GameId,
				command.ResellerCode,
				command.LicenseeCode,
				command.BrandCode,
				command.PlayerCode,
				command.Username,
				command.CurrencyCode,
				command.WalletId,
				command.ExternalRoundId,
				command.ExternalBetId,
				command.ExternalGameId,
				command.BetState,
				command.BetType,
				command.FreeGameState,
				command.GambleState,
				command.CashoutState,
				command.ResettleState,
				command.JackpotState,
				command.CurrencyRatio,
				command.Bet,
				command.ValidBet,
				command.Settle,
				command.Commission,
				command.Turnover,
				command.WinLoss,
				command.Jackpot,
				command.AdditionalData),
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		if (!isUpdate)
		{
			logger.LogInformation("{logInformation}",
				new
				{
					LogOn = DateTime.Now.ToString("u"),
					Data = new
					{
						command.GameProviderCode,
						command.SubGameProviderCode,
						command.GameId,
						command.ResellerCode,
						command.LicenseeCode,
						command.BrandCode,
						command.PlayerCode,
						command.Username,
						command.WalletId,
						command.ExternalRoundId,
						command.ExternalBetId,
						ExternalCreatedOn = command.ExternalCreatedOn.ToString("u")
					},
					Message = "This game round data has not changed."
				});
			return;
		}

		logger.LogInformation("{logInformation}",
			new
			{
				LogOn = DateTime.Now.ToString("u"),
				Data = new
				{
					command.GameProviderCode,
					command.SubGameProviderCode,
					command.GameId,
					command.ResellerCode,
					command.LicenseeCode,
					command.BrandCode,
					command.PlayerCode,
					command.Username,
					command.WalletId,
					command.ExternalRoundId,
					command.ExternalBetId,
					ExternalCreatedOn = command.ExternalCreatedOn.ToString("u")
				},
				Message = "This game round data changes."
			});

		var id = await gameRoundService.GetAsync(
			query: new GameRoundCommand(
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
				AdditionalData: command.AdditionalData),
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		await gameBetService.ExecuteAsync(
			command: new GameBetCommand(
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
				AdditionalData: command.AdditionalData),
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}
}
