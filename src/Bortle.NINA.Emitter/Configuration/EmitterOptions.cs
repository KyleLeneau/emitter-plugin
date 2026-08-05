using NINA.Profile.Interfaces;

namespace Bortle.NINA.Emitter.Configuration {

    public class EmitterOptions {
        public NatsSinkOptions Nats { get; set; } = new NatsSinkOptions();

        public WebhookSinkOptions Webhook { get; set; } = new WebhookSinkOptions();

        public static EmitterOptions Load(IPluginOptionsAccessor accessor) {
            var defaults = new EmitterOptions();

            return new EmitterOptions {
                Nats = new NatsSinkOptions {
                    Enabled = accessor.GetValueBoolean(nameof(NatsSinkOptions.Enabled), defaults.Nats.Enabled),
                    Url = accessor.GetValueString(nameof(NatsSinkOptions.Url), defaults.Nats.Url),
                    SubjectPrefix = accessor.GetValueString(nameof(NatsSinkOptions.SubjectPrefix), defaults.Nats.SubjectPrefix)
                },
                Webhook = new WebhookSinkOptions {
                    Enabled = accessor.GetValueBoolean(nameof(WebhookSinkOptions.Enabled), defaults.Webhook.Enabled),
                    Url = accessor.GetValueString(nameof(WebhookSinkOptions.Url), defaults.Webhook.Url),
                    BearerToken = accessor.GetValueString(nameof(WebhookSinkOptions.BearerToken), defaults.Webhook.BearerToken),
                    TimeoutSeconds = accessor.GetValueInt32(nameof(WebhookSinkOptions.TimeoutSeconds), defaults.Webhook.TimeoutSeconds)
                }
            };
        }

        public void Save(IPluginOptionsAccessor accessor) {
            accessor.SetValueBoolean(nameof(NatsSinkOptions.Enabled), this.Nats.Enabled);
            accessor.SetValueString(nameof(NatsSinkOptions.Url), this.Nats.Url);
            accessor.SetValueString(nameof(NatsSinkOptions.SubjectPrefix), this.Nats.SubjectPrefix);

            accessor.SetValueBoolean(nameof(WebhookSinkOptions.Enabled), this.Webhook.Enabled);
            accessor.SetValueString(nameof(WebhookSinkOptions.Url), this.Webhook.Url);
            accessor.SetValueString(nameof(WebhookSinkOptions.BearerToken), this.Webhook.BearerToken);
            accessor.SetValueInt32(nameof(WebhookSinkOptions.TimeoutSeconds), this.Webhook.TimeoutSeconds);
        }
    }
}