using Tgs.GameBets.RapidTest.Worker.Core;
using Tgs.GameBets.RapidTest.Worker.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddGameBetsRapidTestWorkerRepository(
		this IServiceCollection services)
		=> services
		.AddTransient<IGameBetsRapidTestRepository, GameBetsRapidTestRepository>()
		.AddTransient<IGameBetMajorRepository, GameBetMajorRepository>()
		.AddTransient<IGameRoundsRepository, GameRoundsRepository>()
		.AddTransient<IGameBetsWorkerRepository, GameBetsWorkerRepository>();
}
