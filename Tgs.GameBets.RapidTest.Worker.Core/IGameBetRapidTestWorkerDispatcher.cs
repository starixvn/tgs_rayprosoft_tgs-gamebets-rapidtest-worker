using Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

namespace Tgs.GameBets.RapidTest.Worker.Core;

public interface IGameBetRapidTestWorkerDispatcher
{
	ValueTask PushCommandAsync(GameBetRapidTestWorkerRequest command, CancellationToken cancellationToken = default);

	void ExtendExpiry(string resellerCode, string licenseeCode, string brandCode, string gameProviderCode, int gameId, CancellationToken cancellationToken = default);
}
