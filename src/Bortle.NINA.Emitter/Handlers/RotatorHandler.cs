using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using NINA.Equipment.Equipment.MyRotator;
using NINA.Equipment.Interfaces.Mediator;
using System;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Handlers {
    public class RotatorHandler : IRotatorConsumer {
        private readonly IEventEmitter emitter;
        private readonly IRotatorMediator mediator;
        private RotatorDeviceInfoData lastData;

        public RotatorHandler(IEventEmitter eventEmitter, IRotatorMediator rotatorMediator) {
            emitter = eventEmitter;
            mediator = rotatorMediator;
            mediator.RegisterConsumer(this);
            mediator.Connected += MediatorOnConnected;
            mediator.Disconnected += MediatorOnDisconnected;
            mediator.Moved += MediatorOnMoved;
            mediator.MovedMechanical += MediatorOnMovedMechanical;
            mediator.Synced += MediatorOnSynced;
        }

        public void UpdateDeviceInfo(RotatorInfo deviceInfo) {
            var data = new RotatorDeviceInfoData {
                Connected = deviceInfo.Connected,
                CanReverse = deviceInfo.CanReverse,
                Reverse = deviceInfo.Reverse,
                MechanicalPosition = deviceInfo.MechanicalPosition,
                Position = deviceInfo.Position,
                StepSize = deviceInfo.StepSize,
                IsMoving = deviceInfo.IsMoving,
                Synced = deviceInfo.Synced,
            };

            // Skip duplicates from internal nina polling
            if (data.Equals(lastData)) return;

            emitter.Enqueue("rotator", "device-info", data);
            lastData = data;
        }

        private Task MediatorOnConnected(object arg1, EventArgs arg2) {
            var deviceInfo = mediator.GetInfo();
            var data = new DeviceConnectionData {
                Connected = true,
                DeviceType = DeviceType.Rotator,
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
            var data = new DeviceConnectionData { Connected = false, DeviceType = DeviceType.Rotator };
            emitter.Enqueue("device", "connection", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnMoved(object arg1, RotatorEventArgs arg2) {
            var data = new RotatorMovedData { Event = Event.Moved, From = arg2.From, To = arg2.To };
            emitter.Enqueue("rotator", "move", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnMovedMechanical(object arg1, RotatorEventArgs arg2) {
            var data = new RotatorMovedData { Event = Event.MovedMechanical, From = arg2.From, To = arg2.To };
            emitter.Enqueue("rotator", "move", data);
            return Task.CompletedTask;
        }

        private void MediatorOnSynced(object sender, RotatorEventArgs e) {
            var data = new RotatorMovedData { Event = Event.Synced, From = e.From, To = e.To };
            emitter.Enqueue("rotator", "move", data);
        }

        public void Dispose() {
            mediator.Synced -= MediatorOnSynced;
            mediator.MovedMechanical -= MediatorOnMovedMechanical;
            mediator.Moved -= MediatorOnMoved;
            mediator.Disconnected -= MediatorOnDisconnected;
            mediator.Connected -= MediatorOnConnected;
            mediator.RemoveConsumer(this);
        }
    }
}