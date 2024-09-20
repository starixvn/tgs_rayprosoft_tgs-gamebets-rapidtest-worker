namespace Tgs.GameBets.RapidTest.Worker.Core;

public interface IGameBetsWorkerRepository
{
	ValueTask CommandAsync(Models.GameBetWorkCommand command, CancellationToken cancellationToken = default);
}
