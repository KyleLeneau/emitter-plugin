namespace Bortle.NINA.Emitter.Configuration {

    public class NatsSinkOptions {
        public bool Enabled { get; set; } = false;

        public string Url { get; set; } = "nats://localhost:4222";

        public string SubjectPrefix { get; set; } = "nina";
    }
}