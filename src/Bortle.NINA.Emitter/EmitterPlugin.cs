using Bortle.NINA.Emitter.Configuration;
using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.EventSinks;
using Bortle.NINA.Emitter.Handlers;
using Bortle.NINA.Emitter.UI.GlobalOptions;
using NINA.Core.Utility;
using NINA.Equipment.Interfaces.Mediator;
using NINA.Plugin;
using NINA.Plugin.Interfaces;
using NINA.Profile;
using NINA.Profile.Interfaces;
using NINA.Sequencer.Interfaces.Mediator;
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

        private readonly IEventEmitter eventEmitter;
        private readonly HandlerRegistry registry = new();

        /// <summary>
        /// DataContext for the "Emitter_Options" DataTemplate (Options.xaml). Kept separate from
        /// this class so plugin-options UI logic doesn't bloat the plugin entry point.
        /// </summary>
        public GlobalOptionsViewModel OptionsVM { get; }

        /// <summary>
        /// Injectable list: https://github.com/isbeorn/nina.plugin.template#constructor-injection
        /// </summary>
        [ImportingConstructor]
        public EmitterPlugin(
            IProfileService profileService,
            IOptionsVM options,
            IImageSaveMediator imageSaveMediator,
            ICameraMediator cameraDataMediator,
            IDomeMediator domeMediator,
            IFilterWheelMediator filterWheelMediator,
            IFlatDeviceMediator flatMediator,
            IFocuserMediator focuserMediator,
            IGuiderMediator guiderMediator,
            ITelescopeMediator telescopeMediator,
            IRotatorMediator rotatorMediator,
            ISafetyMonitorMediator safetyMonitorMediator,
            ISwitchMediator switchMediator,
            IWeatherDataMediator weatherDataMediator,
            ISequenceMediator sequenceMediator,
            IMessageBroker messageBroker
        ) {
            if (Settings.Default.UpdateSettings) {
                Settings.Default.Upgrade();
                Settings.Default.UpdateSettings = false;
                CoreUtil.SaveSettings(Settings.Default);
            }

            // This helper class can be used to store plugin settings that are dependent on the current profile
            var pluginSettings = new PluginOptionsAccessor(profileService, Guid.Parse(Identifier));

            // Load up configured Sinks from settings
            var emitterOptions = EmitterOptions.Load(pluginSettings);
            var sinks = new List<IEventSink>();
            if (emitterOptions.Nats.Enabled) {
                sinks.Add(new NatsEventSink(emitterOptions.Nats));
            }
            if (emitterOptions.Webhook.Enabled) {
                sinks.Add(new WebhookEventSink(emitterOptions.Webhook));
            }

            // Create the Emitter with configured Sinks
            eventEmitter = new EventEmitterService(sinks);
            _ = eventEmitter.StartAsync();

            // Setup event handlers
            registry.Add(new CameraHandler(eventEmitter, cameraDataMediator)); // ISSUE
            registry.Add(new DomeHandler(eventEmitter, domeMediator));
            registry.Add(new FilterWheelHandler(eventEmitter, filterWheelMediator));
            registry.Add(new FlatPanelHandler(eventEmitter, flatMediator));
            registry.Add(new FocuserHandler(eventEmitter, focuserMediator));
            registry.Add(new GuiderHandler(eventEmitter, guiderMediator));
            registry.Add(new MountHandler(eventEmitter, telescopeMediator));
            registry.Add(new RotatorHandler(eventEmitter, rotatorMediator));
            registry.Add(new SafetyHandler(eventEmitter, safetyMonitorMediator));
            registry.Add(new SwitchHandler(eventEmitter, switchMediator));
            registry.Add(new WeatherHandler(eventEmitter, weatherDataMediator));
            registry.Add(new ImageSaveHandler(eventEmitter, imageSaveMediator));
            registry.Add(new ProfileHandler(eventEmitter, profileService));
            registry.Add(new SequenceHandler(eventEmitter, sequenceMediator));
            registry.Add(new TargetSchedulerHandler(eventEmitter, messageBroker));

            // Setup GlobalOptions View Model
            OptionsVM = new GlobalOptionsViewModel(pluginSettings, eventEmitter);
        }

        public override async Task Teardown() {
            OptionsVM.Dispose();
            registry.Dispose();
            await eventEmitter.DisposeAsync();
            await base.Teardown();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}