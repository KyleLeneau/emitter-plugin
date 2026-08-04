using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.EventSinks;
using CloudNative.CloudEvents;
using System.Collections.Concurrent;

namespace Bortle.NINA.Emitter.Tests.Fakes {

    public class FakeEventSink : IEventSink {
        private readonly Func<CloudEvent, CancellationToken, Task>? onSend;

        public FakeEventSink(string name = "fake", Func<CloudEvent, CancellationToken, Task>? onSend = null) {
            this.Name = name;
            this.onSend = onSend;
        }

        public string Name { get; }

        public ConnectionState State { get; private set; } = ConnectionState.Disconnected;

        public event EventHandler<ConnectionState>? StateChanged;

        public ConcurrentQueue<CloudEvent> Received { get; } = new ConcurrentQueue<CloudEvent>();

        public int ConnectCallCount { get; private set; }

        public int DisconnectCallCount { get; private set; }

        public Task ConnectAsync(CancellationToken ct) {
            this.ConnectCallCount++;
            this.SetState(ConnectionState.Connected);
            return Task.CompletedTask;
        }

        public Task DisconnectAsync(CancellationToken ct) {
            this.DisconnectCallCount++;
            this.SetState(ConnectionState.Disconnected);
            return Task.CompletedTask;
        }

        public async Task SendAsync(CloudEvent evt, CancellationToken ct) {
            this.Received.Enqueue(evt);
            if (this.onSend != null) {
                await this.onSend(evt, ct);
            }
        }

        private void SetState(ConnectionState state) {
            this.State = state;
            this.StateChanged?.Invoke(this, state);
        }
    }
}
