using System.Text.Json;
using Tgs.GameBets.RapidTest.Worker.Core;
using Valhalla.MessageQueue;

namespace Tgs.GameBets.RapidTest.Worker.Messages;

internal class GameBetsRapidTestWorkerExpiryMessageHandler(
	ILogger<GameBetsRapidTestWorkerExpiryMessageHandler> logger,
	IGameBetRapidTestWorkerDispatcher dispatcher) : IMessageHandler<Common.Protos.GameBetsRapidTestWorkerrExpiryRequest>
{
	public ValueTask HandleAsync(string subject, Common.Protos.GameBetsRapidTestWorkerrExpiryRequest data, IEnumerable<MessageHeaderValue>? headerValues, CancellationToken cancellationToken = default)
	{
		logger.LogInformation("{logInformation}",
			JsonSerializer.Serialize(new
			{
				LogOn = DateTime.Now.ToString("u"),
				Subject = subject,
				Data = new
				{
					data.GameProviderCode,
					data.GameId,
					data.ResellerCode,
					data.LicenseeCode,
					data.BrandCode
				},
				Message = "This receive message handler."
			}));

		dispatcher.ExtendExpiry(
			resellerCode: data.ResellerCode,
			licenseeCode: data.LicenseeCode,
			brandCode: data.BrandCode,
			gameProviderCode: data.GameProviderCode,
			gameId: data.GameId,
			cancellationToken: cancellationToken);

		return ValueTask.CompletedTask;
	}
}
