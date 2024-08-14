using System.Reflection;
using Tgs.GameBets.RapidTest.Worker.Core;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddGameBetsRapidTestWorkerCore(
		this IServiceCollection services)
		=> services
		.RegisterAllServiceUtilityTypes(
			Assembly.GetExecutingAssembly(),
			ServiceLifetime.Transient)
		.AddSingleton<IGameBetRapidTestWorkerDispatcher, GameBetRapidTestWorkerDispatcher>();
}
