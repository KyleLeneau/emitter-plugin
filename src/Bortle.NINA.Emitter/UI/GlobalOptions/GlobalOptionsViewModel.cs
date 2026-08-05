using Bortle.NINA.Emitter.Configuration;
using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.EventSinks;
using CommunityToolkit.Mvvm.Input;
using NINA.Core.Utility;
using NINA.Profile.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.UI.GlobalOptions {

    /// <summary>
    /// Backs the plugin's "Emitter_Options" DataTemplate (Options.xaml). Owns editable copies of
    /// each sink's options plus the commands to persist them and apply them live against the
    /// running <see cref="IEventEmitter"/>, keeping EmitterPlugin itself free of UI concerns.
    /// </summary>
    public class GlobalOptionsViewModel : IDisposable {
        private readonly IPluginOptionsAccessor pluginSettings;
        private readonly IEventEmitter eventEmitter;

        public GlobalOptionsViewModel(IPluginOptionsAccessor pluginSettings, IEventEmitter eventEmitter) {
            this.pluginSettings = pluginSettings;
            this.eventEmitter = eventEmitter;

            this.Nats = new NatsSinkOptionsViewModel(EmitterOptions.LoadNats(pluginSettings));
            this.Nats.AttachSink(FindSink("nats"));

            this.Webhook = new WebhookSinkOptionsViewModel(EmitterOptions.LoadWebhook(pluginSettings));
            this.Webhook.AttachSink(FindSink("webhook"));

            this.SaveNatsCommand = new AsyncRelayCommand(SaveNatsAsync);
            this.SaveWebhookCommand = new AsyncRelayCommand(SaveWebhookAsync);
        }

        public NatsSinkOptionsViewModel Nats { get; }

        public WebhookSinkOptionsViewModel Webhook { get; }

        public IAsyncRelayCommand SaveNatsCommand { get; }

        public IAsyncRelayCommand SaveWebhookCommand { get; }

        private IEventSink FindSink(string name) => this.eventEmitter.Sinks.FirstOrDefault(s => s.Name == name);

        private async Task SaveNatsAsync() {
            this.Nats.ErrorMessage = null;
            this.Nats.IsSaving = true;
            try {
                var options = this.Nats.ToOptions();
                EmitterOptions.SaveNats(this.pluginSettings, options);

                if (options.Enabled) {
                    var sink = new NatsEventSink(options);
                    await this.eventEmitter.SetSinkAsync(sink);
                    this.Nats.AttachSink(sink);
                } else {
                    await this.eventEmitter.RemoveSinkAsync("nats");
                    this.Nats.AttachSink(null);
                }
            } catch (Exception ex) {
                Logger.Error(ex);
                this.Nats.ErrorMessage = ex.Message;
            } finally {
                this.Nats.IsSaving = false;
            }
        }

        private async Task SaveWebhookAsync() {
            this.Webhook.ErrorMessage = null;
            this.Webhook.IsSaving = true;
            try {
                var options = this.Webhook.ToOptions();
                EmitterOptions.SaveWebhook(this.pluginSettings, options);

                if (options.Enabled) {
                    var sink = new WebhookEventSink(options);
                    await this.eventEmitter.SetSinkAsync(sink);
                    this.Webhook.AttachSink(sink);
                } else {
                    await this.eventEmitter.RemoveSinkAsync("webhook");
                    this.Webhook.AttachSink(null);
                }
            } catch (Exception ex) {
                Logger.Error(ex);
                this.Webhook.ErrorMessage = ex.Message;
            } finally {
                this.Webhook.IsSaving = false;
            }
        }

        public void Dispose() {
            this.Nats.Dispose();
            this.Webhook.Dispose();
        }
    }
}