namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddGameBetsRapidTestWorker(
		this IServiceCollection services)
		=> services.AddGameBetsRapidTestWorkerCore();
}
