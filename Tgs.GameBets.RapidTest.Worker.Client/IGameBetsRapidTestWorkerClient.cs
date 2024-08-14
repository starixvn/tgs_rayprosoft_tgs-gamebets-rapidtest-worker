namespace Tgs.GameBets.RapidTest.Worker.Client;

public interface IGameBetsRapidTestWorkerClient
{
	public ValueTask HandleAsync(Common.ViewModels.GameBetRapidTestWorkerHandleViewModel source, CancellationToken cancellationToken = default);
}
