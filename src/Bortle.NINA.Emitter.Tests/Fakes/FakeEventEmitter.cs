using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.EventSinks;

namespace Bortle.NINA.Emitter.Tests.Fakes
{

    public record EnqueuedCall(string Domain, string EventType, object? Data);

    public class FakeEventEmitter : IEventEmitter
    {
        public List<EnqueuedCall> Calls { get; } = new List<EnqueuedCall>();

        public IReadOnlyList<IEventSink> Sinks => Array.Empty<IEventSink>();

        public Task StartAsync(CancellationToken ct = default) => Task.CompletedTask;

        public void Enqueue<T>(string domain, string eventType, T data)
        {
            this.Calls.Add(new EnqueuedCall(domain, eventType, data));
        }

        public Task SetSinkAsync(IEventSink sink, CancellationToken ct = default) => Task.CompletedTask;

        public Task RemoveSinkAsync(string name, CancellationToken ct = default) => Task.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
