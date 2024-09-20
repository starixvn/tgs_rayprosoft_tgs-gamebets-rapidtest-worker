namespace Tgs.GameBets.RapidTest.Worker.Core;

public interface IGameBetMajorRepository
{
	ValueTask<GameBets.Common.ViewModels.GameBetMajorInfoViewModel> CommandAsync(Models.GameBetMajorCommand command, CancellationToken cancellationToken = default);
}
