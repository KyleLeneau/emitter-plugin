using Bortle.NINA.Emitter.EventSinks;
using CloudNative.CloudEvents;
using NINA.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Events {

    public class EventEmitterService : IEventEmitter {
        private const int DefaultQueueCapacity = 1000;

        private readonly List<SinkWorker> workers;
        private readonly string instanceId;

        public EventEmitterService(IEnumerable<IEventSink> sinks, string instanceId = null, int queueCapacity = DefaultQueueCapacity) {
            this.instanceId = (instanceId ?? Environment.MachineName).ToLowerInvariant();
            this.workers = sinks.Select(sink => new SinkWorker(sink, queueCapacity)).ToList();
        }

        public IReadOnlyList<IEventSink> Sinks => this.workers.Select(w => w.Sink).ToList();

        public async Task StartAsync(CancellationToken ct = default) {
            foreach (var worker in this.workers) {
                try {
                    await worker.Sink.ConnectAsync(ct);
                } catch (Exception ex) {
                    Logger.Error(ex);
                }
            }
        }

        public void Enqueue<T>(string domain, string eventType, T data) {
            var evt = new CloudEvent {
                Type = $"io.nina.{domain}.{eventType}",
                Source = new Uri($"//nina/{this.instanceId}", UriKind.RelativeOrAbsolute),
                Id = Guid.NewGuid().ToString(),
                Time = DateTimeOffset.UtcNow,
                DataContentType = "application/json",
                Data = data
            };

            // Fan out to each sink's own queue so a slow/backed-up sink only drops its own
            // events instead of blocking or starving delivery to the other sinks.
            foreach (var worker in this.workers) {
                if (!worker.TryEnqueue(evt)) {
                    Logger.Warning($"Emitter queue rejected event {evt.Type} for sink {worker.Sink.GetType().Name}");
                }
            }
        }

        public async ValueTask DisposeAsync() {
            await Task.WhenAll(this.workers.Select(w => w.DisposeAsync().AsTask()));

            foreach (var worker in this.workers) {
                try {
                    await worker.Sink.DisconnectAsync(CancellationToken.None);
                } catch (Exception ex) {
                    Logger.Error(ex);
                }
            }
        }

        /// <summary>
        /// Owns an independent bounded queue and consumer loop for a single sink, so that
        /// delivery to one sink can never block or slow down delivery to another.
        /// </summary>
        private sealed class SinkWorker : IAsyncDisposable {
            private readonly Channel<CloudEvent> channel;
            private readonly CancellationTokenSource cts = new CancellationTokenSource();
            private readonly Task consumerLoop;

            public SinkWorker(IEventSink sink, int queueCapacity) {
                this.Sink = sink;
                this.channel = Channel.CreateBounded<CloudEvent>(new BoundedChannelOptions(queueCapacity) {
                    FullMode = BoundedChannelFullMode.DropOldest,
                    SingleReader = true,
                    SingleWriter = false
                });
                this.consumerLoop = Task.Run(() => this.ProcessQueueAsync(this.cts.Token));
            }

            public IEventSink Sink { get; }

            public bool TryEnqueue(CloudEvent evt) => this.channel.Writer.TryWrite(evt);

            private async Task ProcessQueueAsync(CancellationToken ct) {
                try {
                    await foreach (var evt in this.channel.Reader.ReadAllAsync(ct)) {
                        try {
                            await this.Sink.SendAsync(evt, ct);
                        } catch (Exception ex) {
                            Logger.Error(ex);
                        }
                    }
                } catch (OperationCanceledException) {
                    // expected during shutdown
                }
            }

            public async ValueTask DisposeAsync() {
                this.channel.Writer.TryComplete();
                this.cts.Cancel();

                try {
                    await this.consumerLoop;
                } catch (Exception ex) {
                    Logger.Error(ex);
                }

                this.cts.Dispose();
            }
        }
    }
}
