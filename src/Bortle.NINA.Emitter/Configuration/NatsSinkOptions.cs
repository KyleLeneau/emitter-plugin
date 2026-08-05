namespace Bortle.NINA.Emitter.Configuration {

    public class NatsSinkOptions {
        public bool Enabled { get; set; } = true;

        public string Url { get; set; } = "nats://localhost:4222";

        public string Username { get; set; } = "";

        public string Password { get; set; } = "";

        public string SubjectPrefix { get; set; } = "nina";

        public bool RequiresAuthentication {  
            get {
                return !string.IsNullOrEmpty(Username);
            } 
        }
    }
}