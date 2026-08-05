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

        private readonly CameraHandler cameraHandler;
        private readonly DomeHandler domeHandler;
        private readonly FilterWheelHandler filterWheelHandler;
        private readonly FlatPanelHandler flatPanelHandler;
        private readonly FocuserHandler focuserHandler;
        private readonly GuiderHandler guiderHandler;
        private readonly MountHandler mountHandler;
        private readonly RotatorHandler rotatorHandler;
        private readonly SafetyHandler safetyHandler;
        private readonly SwitchHandler switchHandler;
        private readonly WeatherHandler weatherHandler;

        /// <summary>
        /// DataContext for the "Emitter_Options" DataTemplate (Options.xaml). Kept separate from
        /// this class so plugin-options UI logic doesn't bloat the plugin entry point.
        /// </summary>
        public GlobalOptionsViewModel OptionsVM { get; }

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
            IWeatherDataMediator weatherDataMediator
        ) {
            if (Settings.Default.UpdateSettings) {
                Settings.Default.Upgrade();
                Settings.Default.UpdateSettings = false;
                CoreUtil.SaveSettings(Settings.Default);
            }

            // This helper class can be used to store plugin settings that are dependent on the current profile
            var pluginSettings = new PluginOptionsAccessor(profileService, Guid.Parse(Identifier));

            var emitterOptions = EmitterOptions.Load(pluginSettings);
            var sinks = new List<IEventSink>();
            if (emitterOptions.Nats.Enabled) {
                sinks.Add(new NatsEventSink(emitterOptions.Nats));
            }
            if (emitterOptions.Webhook.Enabled) {
                sinks.Add(new WebhookEventSink(emitterOptions.Webhook));
            }

            eventEmitter = new EventEmitterService(sinks);
            _ = eventEmitter.StartAsync();

            // Setup event handlers
            cameraHandler = new CameraHandler(eventEmitter, cameraDataMediator);
            domeHandler = new DomeHandler(eventEmitter, domeMediator);
            filterWheelHandler = new FilterWheelHandler(eventEmitter, filterWheelMediator);
            flatPanelHandler = new FlatPanelHandler(eventEmitter, flatMediator);
            focuserHandler = new FocuserHandler(eventEmitter, focuserMediator);
            guiderHandler = new GuiderHandler(eventEmitter, guiderMediator);
            mountHandler = new MountHandler(eventEmitter, telescopeMediator);
            rotatorHandler = new RotatorHandler(eventEmitter, rotatorMediator);
            safetyHandler = new SafetyHandler(eventEmitter, safetyMonitorMediator);
            switchHandler = new SwitchHandler(eventEmitter, switchMediator);
            weatherHandler = new WeatherHandler(eventEmitter, weatherDataMediator);

            OptionsVM = new GlobalOptionsViewModel(pluginSettings, eventEmitter);
        }

        public override async Task Teardown() {
            // Make sure to unregister an event when the object is no longer in use. Otherwise, garbage collection will be prevented.
            OptionsVM.Dispose();
            cameraHandler.Dispose();
            domeHandler.Dispose();
            filterWheelHandler.Dispose();
            flatPanelHandler.Dispose();
            focuserHandler.Dispose();
            guiderHandler.Dispose();
            mountHandler.Dispose();
            rotatorHandler.Dispose();
            safetyHandler.Dispose();
            switchHandler.Dispose();
            weatherHandler.Dispose();
            await eventEmitter.DisposeAsync();

            await base.Teardown();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}