namespace Tgs.GameBets.RapidTest.Worker.Core;

public interface IGameRoundsRepository
{
	ValueTask<Guid> CommandAsync(Models.GameRoundCommand command, CancellationToken cancellationToken = default);
}
