using Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

namespace Tgs.GameBets.RapidTest.Worker.Core;

public interface IRapidTestDispatcher
{
	ValueTask PushCommandAsync(GameBetRapidTestWorkerCommand command, CancellationToken cancellationToken = default);
}
