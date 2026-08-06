using Bortle.NINA.Emitter.Events;
using NINA.Profile.Interfaces;
using System;
using System.Collections.Specialized;

namespace Bortle.NINA.Emitter.Handlers {
    public class ProfileHandler : IDisposable {
        private readonly IEventEmitter emitter;
        private readonly IProfileService service;

        public ProfileHandler(IEventEmitter eventEmitter, IProfileService profileService) {
            emitter = eventEmitter;
            service = profileService;
            service.Profiles.CollectionChanged += ProfilesOnCollectionChanged;
            service.ProfileChanged += OnProfileChanged;
            service.LocationChanged += OnLocationChanged;
            service.HorizonChanged += OnHorizonChanged;
            service.LocaleChanged += OnLocaleChanged;
        }

        private void ProfilesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            // throw new NotImplementedException();
            // switch (e.Action) {
            //     case NotifyCollectionChangedAction.Add: 
            //     case NotifyCollectionChangedAction.Remove:
            //     case NotifyCollectionChangedAction.Replace:
            // }
        }

        private void OnProfileChanged(object sender, EventArgs e) {
            // throw new NotImplementedException();
        }

        private void OnLocationChanged(object sender, EventArgs e) {
            // throw new NotImplementedException();
        }

        private void OnHorizonChanged(object sender, EventArgs e) {
            // throw new NotImplementedException();
        }

        private void OnLocaleChanged(object sender, EventArgs e) {
            // throw new NotImplementedException();
        }

        public void Dispose() {
            service.LocaleChanged -= OnLocaleChanged;
            service.HorizonChanged -= OnHorizonChanged;
            service.LocationChanged -= OnLocationChanged;
            service.ProfileChanged -= OnProfileChanged;
        }
    }
}