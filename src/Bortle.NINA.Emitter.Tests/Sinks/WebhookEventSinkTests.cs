using Bortle.NINA.Emitter.Configuration;
using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.EventSinks;
using CloudNative.CloudEvents;

namespace Bortle.NINA.Emitter.Tests.EventSinks {

    public class WebhookEventSinkTests {
        [Fact]
        public void State_IsDisconnected_BeforeConnecting() {
            var sink = new WebhookEventSink(new WebhookSinkOptions { Url = "https://example.invalid/webhook" });

            Assert.Equal(ConnectionState.Disconnected, sink.State);
        }

        [Fact]
        public async Task ConnectAsync_TransitionsStateToConnected() {
            var sink = new WebhookEventSink(new WebhookSinkOptions { Url = "https://example.invalid/webhook" });

            await sink.ConnectAsync(CancellationToken.None);

            Assert.Equal(ConnectionState.Connected, sink.State);
        }

        [Fact]
        public async Task DisconnectAsync_TransitionsStateBackToDisconnected() {
            var sink = new WebhookEventSink(new WebhookSinkOptions { Url = "https://example.invalid/webhook" });
            await sink.ConnectAsync(CancellationToken.None);

            await sink.DisconnectAsync(CancellationToken.None);

            Assert.Equal(ConnectionState.Disconnected, sink.State);
        }

        [Fact]
        public async Task SendAsync_BeforeConnecting_Throws() {
            var sink = new WebhookEventSink(new WebhookSinkOptions { Url = "https://example.invalid/webhook" });
            var evt = new CloudEvent {
                Type = "io.nina.weather.device-info",
                Source = new Uri("//nina/test-host", UriKind.RelativeOrAbsolute),
                Id = Guid.NewGuid().ToString(),
                Time = DateTimeOffset.UtcNow,
                DataContentType = "application/json"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => sink.SendAsync(evt, CancellationToken.None));
        }
    }
}
