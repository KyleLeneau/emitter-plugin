using Bortle.NINA.Emitter.Configuration;
using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.EventSinks;
using Bortle.NINA.Emitter.Handlers;
using NINA.Core.Utility;
using NINA.Equipment.Interfaces.Mediator;
using NINA.Plugin;
using NINA.Plugin.Interfaces;
using NINA.Profile;
using NINA.Profile.Interfaces;
using NINA.WPF.Base.Interfaces.Mediator;
using NINA.WPF.Base.Interfaces.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Settings = Bortle.NINA.Emitter.Properties.Settings;

namespace Bortle.NINA.Emitter {
    /// <summary>
    /// This class exports the IPluginManifest interface and will be used for the general plugin information and options
    /// The base class "PluginBase" will populate all the necessary Manifest metadata out of the AssemblyInfo attributes. Please fill these accoringly
    ///
    /// An instance of this class will be created and set as datacontext on the plugin options tab in N.I.N.A. to be able to configure global plugin settings
    /// The user interface for the settings will be defined by a DataTemplate with the key having the naming convention "Emitter_Options" where Emitter corresponds to the AssemblyTitle - In this template example it is found in the Options.xaml
    /// </summary>
    [Export(typeof(IPluginManifest))]
    public class EmitterPlugin : PluginBase, INotifyPropertyChanged {
        private readonly IPluginOptionsAccessor pluginSettings;
        private readonly IEventEmitter eventEmitter;
        private readonly WeatherHandler weatherHandler;
        private readonly SafetyHandler safetyHandler;

        [ImportingConstructor]
        public EmitterPlugin(
            IProfileService profileService,
            IOptionsVM options,
            IImageSaveMediator imageSaveMediator,
            IWeatherDataMediator weatherDataMediator,
            ISafetyMonitorMediator safetyMonitorMediator
        ) {
            if (Settings.Default.UpdateSettings) {
                Settings.Default.Upgrade();
                Settings.Default.UpdateSettings = false;
                CoreUtil.SaveSettings(Settings.Default);
            }

            // This helper class can be used to store plugin settings that are dependent on the current profile
            this.pluginSettings = new PluginOptionsAccessor(profileService, Guid.Parse(this.Identifier));

            var emitterOptions = EmitterOptions.Load(this.pluginSettings);
            var sinks = new List<IEventSink>();
            if (emitterOptions.Nats.Enabled) {
                sinks.Add(new NatsEventSink(emitterOptions.Nats));
            }
            if (emitterOptions.Webhook.Enabled) {
                sinks.Add(new WebhookEventSink(emitterOptions.Webhook));
            }

            this.eventEmitter = new EventEmitterService(sinks);
            _ = this.eventEmitter.StartAsync();

            this.weatherHandler = new WeatherHandler(this.eventEmitter, weatherDataMediator);
            this.safetyHandler = new SafetyHandler(this.eventEmitter, safetyMonitorMediator);
        }

        public override async Task Teardown() {
            // Make sure to unregister an event when the object is no longer in use. Otherwise, garbage collection will be prevented.
            this.weatherHandler.Dispose();
            this.safetyHandler.Dispose();
            await this.eventEmitter.DisposeAsync();

            await base.Teardown();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}