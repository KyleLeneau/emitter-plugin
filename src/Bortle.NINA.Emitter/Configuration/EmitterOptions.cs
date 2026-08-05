using NINA.Profile.Interfaces;

namespace Bortle.NINA.Emitter.Configuration {

    public class EmitterOptions {
        public NatsSinkOptions Nats { get; set; } = new NatsSinkOptions();

        public WebhookSinkOptions Webhook { get; set; } = new WebhookSinkOptions();

        public static EmitterOptions Load(IPluginOptionsAccessor accessor) {
            return new EmitterOptions {
                Nats = LoadNats(accessor),
                Webhook = LoadWebhook(accessor)
            };
        }

        private const string NatsKeyPrefix = "Nats.";
        private const string WebhookKeyPrefix = "Webhook.";

        public static NatsSinkOptions LoadNats(IPluginOptionsAccessor accessor) {
            var defaults = new NatsSinkOptions();

            return new NatsSinkOptions {
                Enabled = accessor.GetValueBoolean(NatsKeyPrefix + nameof(NatsSinkOptions.Enabled), defaults.Enabled),
                Url = accessor.GetValueString(NatsKeyPrefix + nameof(NatsSinkOptions.Url), defaults.Url),
                Username = accessor.GetValueString(NatsKeyPrefix + nameof(NatsSinkOptions.Username), defaults.Username),
                Password = accessor.GetValueString(NatsKeyPrefix + nameof(NatsSinkOptions.Password), defaults.Password),
                SubjectPrefix = accessor.GetValueString(NatsKeyPrefix + nameof(NatsSinkOptions.SubjectPrefix), defaults.SubjectPrefix)
            };
        }

        public static WebhookSinkOptions LoadWebhook(IPluginOptionsAccessor accessor) {
            var defaults = new WebhookSinkOptions();

            return new WebhookSinkOptions {
                Enabled = accessor.GetValueBoolean(WebhookKeyPrefix + nameof(WebhookSinkOptions.Enabled), defaults.Enabled),
                Url = accessor.GetValueString(WebhookKeyPrefix + nameof(WebhookSinkOptions.Url), defaults.Url),
                BearerToken = accessor.GetValueString(WebhookKeyPrefix + nameof(WebhookSinkOptions.BearerToken), defaults.BearerToken),
                TimeoutSeconds = accessor.GetValueInt32(WebhookKeyPrefix + nameof(WebhookSinkOptions.TimeoutSeconds), defaults.TimeoutSeconds)
            };
        }

        public static void SaveNats(IPluginOptionsAccessor accessor, NatsSinkOptions options) {
            accessor.SetValueBoolean(NatsKeyPrefix + nameof(NatsSinkOptions.Enabled), options.Enabled);
            accessor.SetValueString(NatsKeyPrefix + nameof(NatsSinkOptions.Url), options.Url);
            accessor.SetValueString(NatsKeyPrefix + nameof(NatsSinkOptions.Username), options.Username);
            accessor.SetValueString(NatsKeyPrefix + nameof(NatsSinkOptions.Password), options.Password);
            accessor.SetValueString(NatsKeyPrefix + nameof(NatsSinkOptions.SubjectPrefix), options.SubjectPrefix);
        }

        public static void SaveWebhook(IPluginOptionsAccessor accessor, WebhookSinkOptions options) {
            accessor.SetValueBoolean(WebhookKeyPrefix + nameof(WebhookSinkOptions.Enabled), options.Enabled);
            accessor.SetValueString(WebhookKeyPrefix + nameof(WebhookSinkOptions.Url), options.Url);
            accessor.SetValueString(WebhookKeyPrefix + nameof(WebhookSinkOptions.BearerToken), options.BearerToken);
            accessor.SetValueInt32(WebhookKeyPrefix + nameof(WebhookSinkOptions.TimeoutSeconds), options.TimeoutSeconds);
        }

        public void Save(IPluginOptionsAccessor accessor) {
            SaveNats(accessor, this.Nats);
            SaveWebhook(accessor, this.Webhook);
        }
    }
}