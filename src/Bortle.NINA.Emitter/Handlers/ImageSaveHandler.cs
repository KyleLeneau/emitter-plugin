using Bortle.NINA.Emitter.Events;
using NINA.WPF.Base.Interfaces.Mediator;
using System;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Handlers {
    public class ImageSaveHandler : IDisposable {
        private readonly IEventEmitter emitter;
        private readonly IImageSaveMediator service;

        public ImageSaveHandler(IEventEmitter eventEmitter, IImageSaveMediator imageSaveMediator) {
            emitter = eventEmitter;
            service = imageSaveMediator;
            service.BeforeFinalizeImageSaved += ServiceOnBeforeFinalizeImageSaved;
            service.BeforeImageSaved += ServiceOnBeforeImageSaved;
            service.ImageSaved += ServiceOnImageSaved;
        }

        private Task ServiceOnBeforeFinalizeImageSaved(object arg1, BeforeFinalizeImageSavedEventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private Task ServiceOnBeforeImageSaved(object arg1, BeforeImageSavedEventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private void ServiceOnImageSaved(object sender, ImageSavedEventArgs e) {
            // TODO: Implement event
        }

        public void Dispose() {
            service.ImageSaved -= ServiceOnImageSaved;
            service.BeforeImageSaved -= ServiceOnBeforeImageSaved;
            service.BeforeFinalizeImageSaved -= ServiceOnBeforeFinalizeImageSaved;
        }
    }
}