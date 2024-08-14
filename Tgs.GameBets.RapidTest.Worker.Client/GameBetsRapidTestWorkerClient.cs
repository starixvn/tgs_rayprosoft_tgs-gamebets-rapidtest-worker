using System.Net.Http.Json;
using System.Text.Json;

using System.Text.Json.Serialization;

namespace Tgs.GameBets.RapidTest.Worker.Client;

internal class GameBetsRapidTestWorkerClient(
	HttpClient httpClient) : IGameBetsRapidTestWorkerClient
{
	private static readonly JsonSerializerOptions _SerializerOptions = new()
	{
		WriteIndented = false,
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		Converters = { new JsonStringEnumConverter() }
	};

	public async ValueTask HandleAsync(Common.ViewModels.GameBetRapidTestWorkerHandleViewModel source, CancellationToken cancellationToken = default)
	{
		var requestContent = JsonContent.Create(source, options: _SerializerOptions);

		var httpResponseMessage = await httpClient.PostAsync(
			"/worker",
			requestContent,
			cancellationToken)
			.ConfigureAwait(false);

		_ = httpResponseMessage.EnsureSuccessStatusCode();
	}
}
