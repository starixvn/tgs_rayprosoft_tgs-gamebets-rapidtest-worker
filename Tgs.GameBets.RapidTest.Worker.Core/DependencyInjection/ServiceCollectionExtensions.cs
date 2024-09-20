using System.Reflection;
using Tgs.GameBets.RapidTest.Worker.Core;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddGameBetsRapidTestWorkerCore(
		this IServiceCollection services)
		=> services
		.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
		.AddSingleton<IGameBetRapidTestWorkerDispatcher, GameBetRapidTestWorkerDispatcher>();
}
