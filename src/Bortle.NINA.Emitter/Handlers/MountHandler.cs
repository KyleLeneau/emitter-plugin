using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using NINA.Equipment.Equipment.MyTelescope;
using NINA.Equipment.Interfaces.Mediator;
using System;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Handlers {
    public class MountHandler : ITelescopeConsumer {
        private readonly IEventEmitter emitter;
        private readonly ITelescopeMediator mediator;

        public MountHandler(IEventEmitter eventEmitter, ITelescopeMediator telescopeMediator) {
            emitter = eventEmitter;
            mediator = telescopeMediator;
            mediator.RegisterConsumer(this);
            mediator.Connected += MediatorOnConnected;
            mediator.Disconnected += MediatorOnDisconnected;
            mediator.AfterMeridianFlip += MediatorOnAfterMeridianFlip;
            mediator.BeforeMeridianFlip += MediatorOnBeforeMeridianFlip;
            mediator.Homed += MediatorOnHomed;
            mediator.Parked += MediatorOnParked;
            mediator.Slewed += MediatorOnSlewed;
            mediator.Unparked += MediatorOnUnparked;
        }

        public void UpdateDeviceInfo(TelescopeInfo deviceInfo) {
            // TODO: Implement event
        }

        private Task MediatorOnConnected(object arg1, EventArgs arg2) {
            var deviceInfo = mediator.GetInfo();
            var data = new DeviceConnectionData {
                Connected = true,
                DeviceType = "Mount",
                Name = deviceInfo.Name,
                Description = deviceInfo.Description,
                DriverInfo = deviceInfo.DriverInfo,
                DriverVersion = deviceInfo.DriverVersion,
                DeviceId = deviceInfo.DeviceId
            };
            emitter.Enqueue("device", "connection", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnDisconnected(object arg1, EventArgs arg2) {
            var data = new DeviceConnectionData { Connected = false, DeviceType = "Mount" };
            emitter.Enqueue("device", "connection", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnAfterMeridianFlip(object arg1, AfterMeridianFlipEventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private Task MediatorOnBeforeMeridianFlip(object arg1, BeforeMeridianFlipEventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private Task MediatorOnHomed(object arg1, EventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private Task MediatorOnParked(object arg1, EventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private Task MediatorOnSlewed(object arg1, MountSlewedEventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private Task MediatorOnUnparked(object arg1, EventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        public void Dispose() {
            mediator.Unparked -= MediatorOnUnparked;
            mediator.Slewed -= MediatorOnSlewed;
            mediator.Parked -= MediatorOnParked;
            mediator.Homed -= MediatorOnHomed;
            mediator.BeforeMeridianFlip -= MediatorOnBeforeMeridianFlip;
            mediator.AfterMeridianFlip -= MediatorOnAfterMeridianFlip;
            mediator.Disconnected -= MediatorOnDisconnected;
            mediator.Connected -= MediatorOnConnected;
            mediator.RemoveConsumer(this);
        }
    }
}