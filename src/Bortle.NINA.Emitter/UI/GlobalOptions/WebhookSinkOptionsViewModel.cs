using Bortle.NINA.Emitter.Configuration;

namespace Bortle.NINA.Emitter.UI.GlobalOptions {

    public class WebhookSinkOptionsViewModel : SinkOptionsViewModelBase {
        private string url;
        private string bearerToken;
        private int timeoutSeconds;

        public WebhookSinkOptionsViewModel(WebhookSinkOptions options) {
            this.Enabled = options.Enabled;
            this.url = options.Url;
            this.bearerToken = options.BearerToken;
            this.timeoutSeconds = options.TimeoutSeconds;
        }

        public string Url {
            get => this.url;
            set => SetProperty(ref this.url, value);
        }

        public string BearerToken {
            get => this.bearerToken;
            set => SetProperty(ref this.bearerToken, value);
        }

        public int TimeoutSeconds {
            get => this.timeoutSeconds;
            set => SetProperty(ref this.timeoutSeconds, value);
        }

        public WebhookSinkOptions ToOptions() {
            return new WebhookSinkOptions {
                Enabled = this.Enabled,
                Url = this.Url,
                BearerToken = this.BearerToken,
                TimeoutSeconds = this.TimeoutSeconds
            };
        }
    }
}