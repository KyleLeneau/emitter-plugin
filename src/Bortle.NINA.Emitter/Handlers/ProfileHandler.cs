using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using Bortle.NINA.Emitter.Utils;
using NINA.Profile.Interfaces;
using System;
using System.Collections.Specialized;
using Action = Bortle.NINA.Emitter.Models.Action;

namespace Bortle.NINA.Emitter.Handlers {
    public class ProfileHandler : IDisposable {
        private readonly IEventEmitter emitter;
        private readonly IProfileService service;

        public ProfileHandler(IEventEmitter eventEmitter, IProfileService profileService) {
            emitter = eventEmitter;
            service = profileService;
            service.Profiles.CollectionChanged += ProfilesOnCollectionChanged;
            service.BeforeProfileChanging += OnBeforeProfileChanging;
            service.ProfileChanged += OnProfileChanged;
            service.LocationChanged += OnLocationChanged;
            service.HorizonChanged += OnHorizonChanged;
            service.LocaleChanged += OnLocaleChanged;

            SendSelectedProfile();
        }

        private void ProfilesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            ProfileListData data = e.Action switch {
                NotifyCollectionChangedAction.Add => new ProfileListData { Action = Action.Add },
                NotifyCollectionChangedAction.Remove => new ProfileListData { Action = Action.Remove },
                NotifyCollectionChangedAction.Replace => new ProfileListData { Action = Action.Replace },
                NotifyCollectionChangedAction.Reset => new ProfileListData { Action = Action.Reset },
                _ => null
            };

            if (data != null) {
                emitter.Enqueue("profile", "list", data);
            }
        }

        private void OnBeforeProfileChanging(object sender, EventArgs e) {
            // Should we send an event?
        }

        private void OnProfileChanged(object sender, EventArgs e) {
            SendSelectedProfile();
        }

        private void OnLocationChanged(object sender, EventArgs e) {
            var data = new ProfileLocationData {
                Latitude = service.ActiveProfile.AstrometrySettings.Latitude.Optional(),
                Longitude = service.ActiveProfile.AstrometrySettings.Longitude.Optional(),
                Elevation = service.ActiveProfile.AstrometrySettings.Elevation.Optional()
            };
            emitter.Enqueue("profile", "location", data);
        }

        private void OnHorizonChanged(object sender, EventArgs e) {
            var data = new ProfileHorizonData {
                FilePath = service.ActiveProfile.AstrometrySettings.HorizonFilePath
            };
            emitter.Enqueue("profile", "horizon", data);
        }

        private void OnLocaleChanged(object sender, EventArgs e) {
            var data = new ProfileLocaleData {
                Name = service.ActiveProfile.ApplicationSettings.Language.Name,
            };
            emitter.Enqueue("profile", "locale", data);
        }

        public void Dispose() {
            service.LocaleChanged -= OnLocaleChanged;
            service.HorizonChanged -= OnHorizonChanged;
            service.LocationChanged -= OnLocationChanged;
            service.ProfileChanged -= OnProfileChanged;
            service.BeforeProfileChanging -= OnBeforeProfileChanging;
        }

        private void SendSelectedProfile() {
            var data = new ProfileSelectedData {
                Id = service.ActiveProfile.Id.ToString(),
                Name = service.ActiveProfile.Name,
                Description = service.ActiveProfile.Description
            };
            emitter.Enqueue("profile", "selected", data);
        }
    }
}