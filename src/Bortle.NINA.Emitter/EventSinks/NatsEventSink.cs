using Bortle.NINA.Emitter.Configuration;
using Bortle.NINA.Emitter.Events;
using CloudNative.CloudEvents;
using CloudNative.CloudEvents.SystemTextJson;
using NATS.Client.Core;
using NATS.Net;
using NINA.Core.Utility;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.EventSinks {

    public class NatsEventSink : IEventSink {
        private const string TypePrefix = "io.nina.";

        private readonly NatsSinkOptions options;
        private readonly JsonEventFormatter formatter = new JsonEventFormatter();
        private NatsClient client;
        private ConnectionState state = ConnectionState.Disconnected;

        public NatsEventSink(NatsSinkOptions options) {
            this.options = options;
        }

        public string Name => "nats";

        public ConnectionState State {
            get => this.state;
            private set {
                if (this.state == value) {
                    return;
                }
                this.state = value;
                this.StateChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<ConnectionState> StateChanged;

        public async Task ConnectAsync(CancellationToken ct) {
            this.State = ConnectionState.Connecting;

            try {
                Logger.Info($"NatsEventProducer: connecting to {options.Url}");
                var opts = NatsOpts.Default with { Url = this.options.Url };
                if (this.options.RequiresAuthentication) {
                    opts = opts with { AuthOpts = opts.AuthOpts with { Username = this.options.Username, Password = this.options.Password } };
                }

                this.client = new NatsClient(opts);
                await this.client.ConnectAsync();
                Logger.Info("NatsEventProducer: connected");
                this.State = ConnectionState.Connected;
            } catch (Exception ex) {
                Logger.Error($"NatsEventProducer: connect to '{options.Url}' failed: {ex.Message}");
                this.State = ConnectionState.Faulted;
                throw;
            }
        }

        public async Task DisconnectAsync(CancellationToken ct) {
            if (this.client != null) {
                await this.client.DisposeAsync();
                this.client = null;
            }
            this.State = ConnectionState.Disconnected;
        }

        public async Task SendAsync(CloudEvent evt, CancellationToken ct) {
            if (this.client == null) {
                throw new InvalidOperationException("NatsSink is not connected.");
            }

            var subject = BuildSubject(this.options.SubjectPrefix, evt);
            var bytes = this.formatter.EncodeStructuredModeMessage(evt, out _);

            try {
                await this.client.PublishAsync(subject, bytes.ToArray(), cancellationToken: ct);
                Logger.Info($"publish event: '{subject}' body: {Encoding.UTF8.GetString(bytes.ToArray())}");
                this.State = ConnectionState.Connected;
            } catch (Exception) {
                this.State = ConnectionState.Faulted;
                throw;
            }
        }

        internal static string BuildSubject(string subjectPrefix, CloudEvent evt) {
            // Source is "//nina/{instanceId}"; the domain.eventType suffix of Type lines up
            // with the NATS subject levels, e.g. "io.nina.weather.device-info" -> "weather.device-info".
            var instanceId = evt.Source.OriginalString.TrimStart('/').Split('/').ElementAtOrDefault(1) ?? "unknown";
            var suffix = evt.Type.StartsWith(TypePrefix) ? evt.Type.Substring(TypePrefix.Length) : evt.Type;

            return $"{subjectPrefix}.{instanceId}.{suffix}";
        }
    }
}