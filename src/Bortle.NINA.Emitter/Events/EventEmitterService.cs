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

        private readonly List<IEventSink> sinks;
        private readonly string instanceId;
        private readonly Channel<CloudEvent> channel;
        private readonly CancellationTokenSource cts = new CancellationTokenSource();
        private readonly Task consumerLoop;

        public EventEmitterService(IEnumerable<IEventSink> sinks, string instanceId = null, int queueCapacity = DefaultQueueCapacity) {
            this.sinks = sinks.ToList();
            this.instanceId = (instanceId ?? Environment.MachineName).ToLowerInvariant();
            this.channel = Channel.CreateBounded<CloudEvent>(new BoundedChannelOptions(queueCapacity) {
                FullMode = BoundedChannelFullMode.DropOldest,
                SingleReader = true,
                SingleWriter = false
            });
            this.consumerLoop = Task.Run(() => this.ProcessQueueAsync(this.cts.Token));
        }

        public IReadOnlyList<IEventSink> Sinks => this.sinks;

        public async Task StartAsync(CancellationToken ct = default) {
            foreach (var sink in this.sinks) {
                try {
                    await sink.ConnectAsync(ct);
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

            if (!this.channel.Writer.TryWrite(evt)) {
                Logger.Warning($"Emitter queue rejected event {evt.Type}");
            }
        }

        private async Task ProcessQueueAsync(CancellationToken ct) {
            try {
                await foreach (var evt in this.channel.Reader.ReadAllAsync(ct)) {
                    var sendTasks = this.sinks.Select(sink => this.SendSafeAsync(sink, evt, ct));
                    await Task.WhenAll(sendTasks);
                }
            } catch (OperationCanceledException) {
                // expected during shutdown
            }
        }

        private async Task SendSafeAsync(IEventSink sink, CloudEvent evt, CancellationToken ct) {
            try {
                await sink.SendAsync(evt, ct);
            } catch (Exception ex) {
                Logger.Error(ex);
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

            foreach (var sink in this.sinks) {
                try {
                    await sink.DisconnectAsync(CancellationToken.None);
                } catch (Exception ex) {
                    Logger.Error(ex);
                }
            }

            this.cts.Dispose();
        }
    }
}
