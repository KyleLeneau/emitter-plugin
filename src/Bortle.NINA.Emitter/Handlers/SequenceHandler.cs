using Bortle.NINA.Emitter.Events;
using NINA.Core.Utility;
using NINA.Sequencer.Interfaces.Mediator;
using System;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Handlers {
    public class SequenceHandler : IDisposable {
        private readonly IEventEmitter emitter;
        private readonly ISequenceMediator service;

        public SequenceHandler(IEventEmitter eventEmitter, ISequenceMediator sequenceMediator) {
            emitter = eventEmitter;
            service = sequenceMediator;
            
            // All plugins need to load before this handler can start
            Task.Run(async () => {
                while (!service.Initialized) {
                    await Task.Delay(50);
                }

                Logger.Debug("Finished initializing sequence, subscribing to events");
                service.SequenceStarting += ServiceOnSequenceStarting;
                service.SequenceFinished += ServiceOnSequenceFinished;
            });
        }

        private Task ServiceOnSequenceStarting(object arg1, EventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private Task ServiceOnSequenceFinished(object arg1, EventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        public void Dispose() {
            service.SequenceFinished -= ServiceOnSequenceFinished;
            service.SequenceStarting -= ServiceOnSequenceStarting;
        }
    }
}