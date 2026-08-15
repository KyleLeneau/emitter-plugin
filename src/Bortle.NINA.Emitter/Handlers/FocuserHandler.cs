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
        private FocuserInfo lastInfo;

        public FocuserHandler(IEventEmitter eventEmitter, IFocuserMediator focuserMediator) {
            emitter = eventEmitter;
            mediator = focuserMediator;
            mediator.RegisterConsumer(this);
            mediator.Connected += MediatorOnConnected;
            mediator.Disconnected += MediatorOnDisconnected;
        }

        public void UpdateDeviceInfo(FocuserInfo deviceInfo) {
            // Skip duplicates from internal nina polling
            if (deviceInfo.Equals(lastInfo)) return;

            var data = new FocuserDeviceInfoData {
                Connected = deviceInfo.Connected,
                Position = deviceInfo.Position,
                StepSize = deviceInfo.StepSize,
                Temperature = deviceInfo.Temperature,
                IsMoving = deviceInfo.IsMoving,
                IsSettling = deviceInfo.IsSettling,
                TempComp = deviceInfo.TempComp,
                TempCompAvailable = deviceInfo.TempCompAvailable,
            };
            emitter.Enqueue("focuser", "device-info", data);
            lastInfo = deviceInfo;
        }

        public void UpdateEndAutoFocusRun(AutoFocusInfo info) {
            // TODO: Implement event
        }

        public void UpdateUserFocused(FocuserInfo info) {
            // TODO: Implement event
        }

        private Task MediatorOnConnected(object arg1, EventArgs arg2) {
            var deviceInfo = mediator.GetInfo();
            var data = new DeviceConnectionData {
                Connected = true,
                DeviceType = DeviceType.Focuser,
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
            var data = new DeviceConnectionData { Connected = false, DeviceType = DeviceType.Focuser };
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