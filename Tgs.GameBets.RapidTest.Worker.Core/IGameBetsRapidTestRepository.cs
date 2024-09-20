namespace Tgs.GameBets.RapidTest.Worker.Core;

public interface IGameBetsRapidTestRepository
{
	ValueTask<bool> CommandAsync(Models.GameBetRapidTestCommand command, CancellationToken cancellationToken = default);
}
