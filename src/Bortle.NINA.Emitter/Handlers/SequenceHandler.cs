using Bortle.NINA.Emitter.Events;
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
            service.SequenceStarting += ServiceOnSequenceStarting;
            service.SequenceFinished += ServiceOnSequenceFinished;
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