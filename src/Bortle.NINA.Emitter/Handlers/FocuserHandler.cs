using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using NINA.Equipment.Equipment.MyFocuser;
using NINA.Equipment.Interfaces.Mediator;
using System;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Handlers {
    public class FocuserHandler : IFocuserConsumer {
        private readonly IEventEmitter emitter;
        private readonly IFocuserMediator mediator;

        public FocuserHandler(IEventEmitter eventEmitter, IFocuserMediator focuserMediator) {
            emitter = eventEmitter;
            mediator = focuserMediator;
            mediator.RegisterConsumer(this);
            mediator.Connected += MediatorOnConnected;
            mediator.Disconnected += MediatorOnDisconnected;
        }

        public void UpdateDeviceInfo(FocuserInfo deviceInfo) {
            // throw new System.NotImplementedException();
        }

        public void UpdateEndAutoFocusRun(AutoFocusInfo info) {
            // throw new NotImplementedException();
        }

        public void UpdateUserFocused(FocuserInfo info) {
            // throw new NotImplementedException();
        }

        private Task MediatorOnConnected(object arg1, EventArgs arg2) {
            var data = new DeviceData { Connected = true, DeviceType = "Focuser" };
            emitter.Enqueue("device", "connection", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnDisconnected(object arg1, EventArgs arg2) {
            var data = new DeviceData { Connected = false, DeviceType = "Focuser" };
            emitter.Enqueue("device", "connection", data);
            return Task.CompletedTask;
        }

        public void Dispose() {
            mediator.Disconnected -= MediatorOnDisconnected;
            mediator.Connected -= MediatorOnConnected;
            mediator.RemoveConsumer(this);
        }
    }
}