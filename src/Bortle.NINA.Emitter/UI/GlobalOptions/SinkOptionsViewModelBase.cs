using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.EventSinks;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Bortle.NINA.Emitter.UI.GlobalOptions {

    /// <summary>
    /// Shared editable state and live connection status for a single sink's options section.
    /// Subclasses expose the sink-specific fields (Url, credentials, ...).
    /// </summary>
    public abstract class SinkOptionsViewModelBase : ObservableObject, IDisposable {
        private IEventSink attachedSink;
        private ConnectionState connectionState = ConnectionState.Disconnected;
        private bool enabled;
        private bool isSaving;
        private string errorMessage;

        public bool Enabled {
            get => this.enabled;
            set => SetProperty(ref this.enabled, value);
        }

        /// <summary>
        /// Live connection state of the currently running sink instance, or Disconnected if the
        /// sink isn't enabled/running. Updated from <see cref="IEventSink.StateChanged"/>.
        /// </summary>
        public ConnectionState ConnectionState {
            get => this.connectionState;
            private set => SetProperty(ref this.connectionState, value);
        }

        public bool IsSaving {
            get => this.isSaving;
            set => SetProperty(ref this.isSaving, value);
        }

        public string ErrorMessage {
            get => this.errorMessage;
            set => SetProperty(ref this.errorMessage, value);
        }

        /// <summary>
        /// Starts (or stops) tracking <paramref name="sink"/>'s live connection state, detaching
        /// from whatever sink was previously attached. Pass null when the sink is disabled/removed.
        /// </summary>
        public void AttachSink(IEventSink sink) {
            if (this.attachedSink != null) {
                this.attachedSink.StateChanged -= OnSinkStateChanged;
            }

            this.attachedSink = sink;

            if (this.attachedSink != null) {
                this.attachedSink.StateChanged += OnSinkStateChanged;
                this.ConnectionState = this.attachedSink.State;
            } else {
                this.ConnectionState = ConnectionState.Disconnected;
            }
        }

        private void OnSinkStateChanged(object sender, ConnectionState state) {
            // Sinks raise this from their own connect/send paths, which may not be the UI thread.
            var dispatcher = Application.Current?.Dispatcher;
            if (dispatcher == null || dispatcher.CheckAccess()) {
                this.ConnectionState = state;
            } else {
                dispatcher.BeginInvoke(DispatcherPriority.DataBind, new Action(() => this.ConnectionState = state));
            }
        }

        public void Dispose() {
            AttachSink(null);
        }
    }
}