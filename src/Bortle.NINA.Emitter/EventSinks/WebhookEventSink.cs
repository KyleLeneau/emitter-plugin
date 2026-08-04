using Bortle.NINA.Emitter.Configuration;
using Bortle.NINA.Emitter.Events;
using CloudNative.CloudEvents;
using CloudNative.CloudEvents.SystemTextJson;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.EventSinks {

    public class WebhookEventSink : IEventSink {
        private readonly WebhookSinkOptions options;
        private readonly JsonEventFormatter formatter = new JsonEventFormatter();
        private HttpClient httpClient;
        private ConnectionState state = ConnectionState.Disconnected;

        public WebhookEventSink(WebhookSinkOptions options) {
            this.options = options;
        }

        public string Name => "webhook";

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

        public Task ConnectAsync(CancellationToken ct) {
            this.httpClient = new HttpClient {
                Timeout = TimeSpan.FromSeconds(this.options.TimeoutSeconds)
            };

            if (!string.IsNullOrEmpty(this.options.BearerToken)) {
                this.httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", this.options.BearerToken);
            }

            // Webhooks are stateless HTTP calls, not a persistent connection - mark ready immediately.
            this.State = ConnectionState.Connected;
            return Task.CompletedTask;
        }

        public Task DisconnectAsync(CancellationToken ct) {
            this.httpClient?.Dispose();
            this.httpClient = null;
            this.State = ConnectionState.Disconnected;
            return Task.CompletedTask;
        }

        public async Task SendAsync(CloudEvent evt, CancellationToken ct) {
            if (this.httpClient == null) {
                throw new InvalidOperationException("WebhookSink is not connected.");
            }

            var bytes = this.formatter.EncodeStructuredModeMessage(evt, out var contentType);
            using var content = new ByteArrayContent(bytes.ToArray());
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType.MediaType) {
                CharSet = contentType.CharSet
            };

            try {
                var response = await this.httpClient.PostAsync(this.options.Url, content, ct);
                response.EnsureSuccessStatusCode();
                this.State = ConnectionState.Connected;
            } catch (Exception) {
                this.State = ConnectionState.Faulted;
                throw;
            }
        }
    }
}