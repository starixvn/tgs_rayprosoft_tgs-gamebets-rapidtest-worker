using Tgs.GameBets.RapidTest.Worker.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddGameBetsWorkerClient(this IServiceCollection services, Action<IHttpClientBuilder> httpClientConfigure)
	{
		httpClientConfigure(services.AddHttpClient<IGameBetsRapidTestWorkerClient, GameBetsRapidTestWorkerClient>());

		return services;
	}
}
