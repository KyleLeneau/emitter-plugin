using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
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
            var data = new SequenceStartData { Name = "Some Name for starting sequence" };
            service.SequenceStarting += ServiceOnSequenceStarting;
            emitter.Enqueue("sequence", "start", data);
            return Task.CompletedTask;
        }

        private Task ServiceOnSequenceFinished(object arg1, EventArgs arg2) {
            var data = new SequenceEndData { Name = "Some Name for ending sequence" };
            service.SequenceFinished += ServiceOnSequenceFinished;
            emitter.Enqueue("sequence", "end", data);
            return Task.CompletedTask;
        }

        public void Dispose() {
            service.SequenceFinished -= ServiceOnSequenceFinished;
            service.SequenceStarting -= ServiceOnSequenceStarting;
        }
    }
}