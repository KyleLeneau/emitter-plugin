namespace Bortle.NINA.Emitter.Configuration {

    public class WebhookSinkOptions {
        public bool Enabled { get; set; } = false;

        public string Url { get; set; } = string.Empty;

        public string BearerToken { get; set; } = string.Empty;

        public int TimeoutSeconds { get; set; } = 10;
    }
}