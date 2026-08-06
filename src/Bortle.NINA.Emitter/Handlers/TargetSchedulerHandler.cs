using Bortle.NINA.Emitter.Events;
using NINA.Plugin.Interfaces;
using System;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Handlers {
    public class TargetSchedulerHandler : IDisposable, ISubscriber {
        private const string TopicWaitStart = "TargetScheduler-WaitStart";
        private const string TopicNewTargetStart = "TargetScheduler-NewTargetStart";
        private const string TopicTargetStart = "TargetScheduler-TargetStart";

        private readonly IEventEmitter emitter;
        private readonly IMessageBroker broker;

        public TargetSchedulerHandler(IEventEmitter eventEmitter, IMessageBroker messageBroker) {
            emitter = eventEmitter;
            broker = messageBroker;

            // WARN: this message broker might go away in the future update
            broker.Subscribe(TopicWaitStart, this);
            broker.Subscribe(TopicNewTargetStart, this);
            broker.Subscribe(TopicTargetStart, this);
        }

        public Task OnMessageReceived(IMessage message) {
            // TODO: Implement event

            // switch (message.Topic) {
            //     case TopicWaitStart:
            //     case TopicNewTargetStart:
            //     case TopicTargetStart:
            // }

            return Task.CompletedTask;
        }

        public void Dispose() {
            broker.Unsubscribe(TopicWaitStart, this);
            broker.Unsubscribe(TopicNewTargetStart, this);
            broker.Unsubscribe(TopicTargetStart, this);
        }
    }
}