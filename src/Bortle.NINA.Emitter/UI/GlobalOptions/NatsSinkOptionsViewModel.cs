using Bortle.NINA.Emitter.Configuration;

namespace Bortle.NINA.Emitter.UI.GlobalOptions {

    public class NatsSinkOptionsViewModel : SinkOptionsViewModelBase {
        private string url;
        private string username;
        private string password;
        private string subjectPrefix;

        public NatsSinkOptionsViewModel(NatsSinkOptions options) {
            this.Enabled = options.Enabled;
            this.url = options.Url;
            this.username = options.Username;
            this.password = options.Password;
            this.subjectPrefix = options.SubjectPrefix;
        }

        public string Url {
            get => this.url;
            set => SetProperty(ref this.url, value);
        }

        public string Username {
            get => this.username;
            set => SetProperty(ref this.username, value);
        }

        public string Password {
            get => this.password;
            set => SetProperty(ref this.password, value);
        }

        public string SubjectPrefix {
            get => this.subjectPrefix;
            set => SetProperty(ref this.subjectPrefix, value);
        }

        public NatsSinkOptions ToOptions() {
            return new NatsSinkOptions {
                Enabled = this.Enabled,
                Url = this.Url,
                Username = this.Username,
                Password = this.Password,
                SubjectPrefix = this.SubjectPrefix
            };
        }
    }
}