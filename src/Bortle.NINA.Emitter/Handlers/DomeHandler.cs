using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using NINA.Equipment.Equipment.MyDome;
using NINA.Equipment.Interfaces.Mediator;
using System;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Handlers {
    public class DomeHandler : IDomeConsumer {
        private readonly IEventEmitter emitter;
        private readonly IDomeMediator mediator;

        public DomeHandler(IEventEmitter eventEmitter, IDomeMediator domeMediator) {
            emitter = eventEmitter;
            mediator = domeMediator;
            mediator.RegisterConsumer(this);
            mediator.Connected += MediatorOnConnected;
            mediator.Disconnected += MediatorOnDisconnected;
            mediator.Closed += MediatorOnClosed;
            mediator.Homed += MediatorOnHomed;
            mediator.Opened += MediatorOnOpened;
            mediator.Parked += MediatorOnParked;
            mediator.Slewed += MediatorOnSlewed;
            mediator.Synced += MediatorOnSynced;
        }

        public void UpdateDeviceInfo(DomeInfo deviceInfo) {
            // TODO: Implement event
        }

        private Task MediatorOnConnected(object arg1, EventArgs arg2) {
            var deviceInfo = mediator.GetInfo();
            var data = new DeviceConnectionData {
                Connected = true,
                DeviceType = "Dome",
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
            var data = new DeviceConnectionData { Connected = false, DeviceType = "Dome" };
            emitter.Enqueue("device", "connection", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnClosed(object arg1, EventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private Task MediatorOnHomed(object arg1, EventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private Task MediatorOnOpened(object arg1, EventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private Task MediatorOnParked(object arg1, EventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private Task MediatorOnSlewed(object arg1, DomeEventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private void MediatorOnSynced(object sender, EventArgs e) {
            // TODO: Implement event
        }

        public void Dispose() {
            mediator.Synced -= MediatorOnSynced;
            mediator.Slewed -= MediatorOnSlewed;
            mediator.Parked -= MediatorOnParked;
            mediator.Opened -= MediatorOnOpened;
            mediator.Homed -= MediatorOnHomed;
            mediator.Closed -= MediatorOnClosed;
            mediator.Disconnected -= MediatorOnDisconnected;
            mediator.Connected -= MediatorOnConnected;
            mediator.RemoveConsumer(this);
        }
    }
}