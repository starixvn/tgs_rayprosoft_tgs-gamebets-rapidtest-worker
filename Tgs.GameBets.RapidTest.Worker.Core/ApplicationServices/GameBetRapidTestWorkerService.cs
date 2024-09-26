using MediatR;
using Microsoft.Extensions.Logging;
using Tgs.GameBets.RapidTest.Worker.Core.Models;

namespace Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

internal class GameBetRapidTestWorkerService(
	ILogger<GameBetRapidTestWorkerService> logger,
	IGameBetsRapidTestRepository gameBetsRapidTestRepository,
	IGameBetMajorRepository gameBetMajorRepository,
	IGameRoundsRepository gameRoundsRepository,
	IGameBetsWorkerRepository gameBetsWorkerRepository) : IRequestHandler<GameBetRapidTestWorkerRequest>
{
	public async Task Handle(GameBetRapidTestWorkerRequest request, CancellationToken cancellationToken)
	{
		var isUpdate = await gameBetsRapidTestRepository.CommandAsync(
			command: new GameBetRapidTestCommand(
				GameProviderCode: request.GameProviderCode,
				SubGameProviderCode: request.SubGameProviderCode,
				GameId: request.GameId,
				ResellerCode: request.ResellerCode,
				LicenseeCode: request.LicenseeCode,
				BrandCode: request.BrandCode,
				PlayerCode: request.PlayerCode,
				Username: request.Username,
				CurrencyCode: request.CurrencyCode,
				WalletId: request.WalletId,
				ExternalRoundId: request.ExternalRoundId,
				ExternalBetId: request.ExternalBetId,
				ExternalGameId: request.ExternalGameId,
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
				AdditionalData: request.AdditionalData),
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
						request.GameProviderCode,
						request.SubGameProviderCode,
						request.GameId,
						request.ResellerCode,
						request.LicenseeCode,
						request.BrandCode,
						request.PlayerCode,
						request.Username,
						request.WalletId,
						request.ExternalRoundId,
						request.ExternalBetId,
						ExternalCreatedOn = request.ExternalCreatedOn.ToString("u")
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
					request.GameProviderCode,
					request.SubGameProviderCode,
					request.GameId,
					request.ResellerCode,
					request.LicenseeCode,
					request.BrandCode,
					request.PlayerCode,
					request.Username,
					request.WalletId,
					request.ExternalRoundId,
					request.ExternalBetId,
					ExternalCreatedOn = request.ExternalCreatedOn.ToString("u")
				},
				Message = "This game round data changes."
			});

		var gameBetMajorInfo = await gameBetMajorRepository.CommandAsync(
			command: new GameBetMajorCommand(
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
				AdditionalData: request.AdditionalData),
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		var id = await gameRoundsRepository.CommandAsync(
			command: new GameRoundCommand(
				Id: gameBetMajorInfo.Id,
				GameProviderId: gameBetMajorInfo.GameProviderId,
				GameProviderConfigurationId: gameBetMajorInfo.GameProviderConfigurationId,
				GameId: gameBetMajorInfo.GameId,
				BrandId: gameBetMajorInfo.BrandId,
				PlayerCode: gameBetMajorInfo.PlayerCode,
				CurrencyCode: gameBetMajorInfo.CurrencyCode,
				WalletId: gameBetMajorInfo.WalletId,
				ExternalBetId: gameBetMajorInfo.ExternalBetId,
				ExternalCreatedOn: gameBetMajorInfo.ExternalCreatedOn,
				ExternalClosedOn: gameBetMajorInfo.ExternalClosedOn,
				BetState: gameBetMajorInfo.BetState,
				Bet: gameBetMajorInfo.Bet,
				ValidBet: gameBetMajorInfo.ValidBet,
				Settle: gameBetMajorInfo.Settle,
				WinLoss: gameBetMajorInfo.WinLoss,
				Turnover: gameBetMajorInfo.Turnover,
				Commission: gameBetMajorInfo.Commission,
				AdditionalData: gameBetMajorInfo.AdditionalData,
				CreatedOn: gameBetMajorInfo.CreatedOn),
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		await gameBetsWorkerRepository.CommandAsync(
			command: new GameBetWorkCommand(
				GameBetId: id,
				GameProviderId: gameBetMajorInfo.GameProviderId,
				GameProviderConfigurationId: gameBetMajorInfo.GameProviderConfigurationId,
				GameId: gameBetMajorInfo.GameId,
				ResellerId: gameBetMajorInfo.ResellerId,
				LicenseeId: gameBetMajorInfo.LicenseeId,
				BrandId: gameBetMajorInfo.BrandId,
				PlayerCode: gameBetMajorInfo.PlayerCode,
				CurrencyCode: gameBetMajorInfo.CurrencyCode,
				WalletId: gameBetMajorInfo.WalletId,
				ExternalRoundId: gameBetMajorInfo.ExternalRoundId,
				ExternalBetId: gameBetMajorInfo.ExternalBetId,
				ExternalCreatedOn: gameBetMajorInfo.ExternalCreatedOn,
				ExternalClosedOn: gameBetMajorInfo.ExternalClosedOn,
				ExternalPayoffOn: gameBetMajorInfo.ExternalPayoffOn,
				ExternalResettleOn: gameBetMajorInfo.ExternalResettleOn,
				BetState: gameBetMajorInfo.BetState,
				BetType: gameBetMajorInfo.BetType,
				FreeGameState: gameBetMajorInfo.FreeGameState,
				GambleState: gameBetMajorInfo.GambleState,
				CashoutState: gameBetMajorInfo.CashoutState,
				ResettleState: gameBetMajorInfo.ResettleState,
				JackpotState: gameBetMajorInfo.JackpotState,
				CurrencyRatio: gameBetMajorInfo.CurrencyRatio,
				Bet: gameBetMajorInfo.Bet,
				ValidBet: gameBetMajorInfo.ValidBet,
				Settle: gameBetMajorInfo.Settle,
				WinLoss: gameBetMajorInfo.WinLoss,
				Turnover: gameBetMajorInfo.Turnover,
				Commission: gameBetMajorInfo.Commission,
				Contribute: gameBetMajorInfo.Contribute,
				Jackpot: gameBetMajorInfo.Jackpot,
				CreatedOn: gameBetMajorInfo.CreatedOn,
				ClosedOn: gameBetMajorInfo.ClosedOn,
				AdditionalData: gameBetMajorInfo.AdditionalData,
				ExtendedData: gameBetMajorInfo.ExtendedData),
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}
}
