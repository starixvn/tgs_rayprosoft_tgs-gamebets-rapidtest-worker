using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Tgs.GameBets.RapidTest.Worker.Core.ApplicationServices;

namespace Tgs.GameBets.RapidTest.Worker.Core;

internal class GameBetRapidTestWorkerDispatcher(IServiceProvider serviceProvider) : IGameBetRapidTestWorkerDispatcher, IDisposable, IAsyncDisposable
{
	private readonly ConcurrentDictionary<string, GameBetRapidTestWorkerChannel> _counters = new();
	private readonly CancellationTokenSource _cancellationTokenSource = new();

	private bool _disposedValue;

	~GameBetRapidTestWorkerDispatcher()
	{
		Dispose(false);
	}

	public async ValueTask PushCommandAsync(GameBetRapidTestWorkerRequest command, CancellationToken cancellationToken = default)
	{
		var key = GetKey(command.ResellerCode, command.LicenseeCode, command.BrandCode, command.GameProviderCode, command.GameId);
		using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cancellationTokenSource.Token);
		var counter = _counters.GetOrAdd(
			key,
			key =>
			{
				var scope = serviceProvider.CreateScope();
				var newCounter = ActivatorUtilities.CreateInstance<GameBetRapidTestWorkerChannel>(
					scope.ServiceProvider,
					key);

				newCounter.Disposed += (_, _) =>
				{
					_ = _counters.TryRemove(newCounter.Key, out _);
					scope.Dispose();
				};

				return newCounter;
			});

		await counter.PrepareAsync(cts.Token).ConfigureAwait(false);
		await counter.PushCommandAsync(command, cts.Token).ConfigureAwait(false);
	}

	public ValueTask DisposeAsync()
	{
		Dispose(true);
		GC.SuppressFinalize(this);

		return ValueTask.CompletedTask;
	}

	public void Dispose()
	{
		// 請勿變更此程式碼。請將清除程式碼放入 'Dispose(bool disposing)' 方法
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	public void ExtendExpiry(string resellerCode, string licenseeCode, string brandCode, string gameProviderCode, int gameId, CancellationToken cancellationToken = default)
	{
		var key = GetKey(resellerCode, licenseeCode, brandCode, gameProviderCode, gameId);
		if (_counters.TryGetValue(key, out var counter))
			counter.ExtendExpiry();
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposedValue)
		{
			if (disposing)
			{
				_cancellationTokenSource.Cancel();
			}

			_cancellationTokenSource.Dispose();
			_disposedValue = true;
		}
	}

	private static string GetKey(string resellerCode, string licenseeCode, string brandCode, string gameProviderCode, int gameId)
	=> string.Concat(resellerCode, ":", licenseeCode, ":", brandCode, ":", gameProviderCode, ":", gameId);
}
