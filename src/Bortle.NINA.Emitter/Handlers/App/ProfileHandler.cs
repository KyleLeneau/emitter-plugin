using Bortle.NINA.Emitter.Events;
using NINA.Profile.Interfaces;
using System;

namespace Bortle.NINA.Emitter.Handlers.App {
    public class ProfileHandler : IDisposable {
        private readonly IEventEmitter emitter;
        private readonly IProfileService service;

        public ProfileHandler(IEventEmitter eventEmitter, IProfileService profileService) {
            emitter = eventEmitter;
            service = profileService;
            service.ProfileChanged += ServiceOnProfileChanged;
            service.LocationChanged += ServiceOnLocationChanged;
            service.HorizonChanged += ServiceOnHorizonChanged;
            service.LocaleChanged += ServiceOnLocaleChanged;
        }

        private void ServiceOnProfileChanged(object sender, EventArgs e) {
            // throw new NotImplementedException();
        }

        private void ServiceOnLocationChanged(object sender, EventArgs e) {
            // throw new NotImplementedException();
        }

        private void ServiceOnHorizonChanged(object sender, EventArgs e) {
            // throw new NotImplementedException();
        }

        private void ServiceOnLocaleChanged(object sender, EventArgs e) {
            // throw new NotImplementedException();
        }

        public void Dispose() {
            service.LocaleChanged -= ServiceOnLocaleChanged;
            service.HorizonChanged -= ServiceOnHorizonChanged;
            service.LocationChanged -= ServiceOnLocationChanged;
            service.ProfileChanged -= ServiceOnProfileChanged;
        }
    }
}