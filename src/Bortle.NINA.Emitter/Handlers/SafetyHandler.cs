using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using NINA.Equipment.Equipment.MySafetyMonitor;
using NINA.Equipment.Interfaces.Mediator;
using NINA.Equipment.Interfaces.ViewModel;
using System;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Handlers {
    public class SafetyHandler : ISafetyMonitorConsumer {
        private readonly IEventEmitter emitter;
        private readonly ISafetyMonitorMediator mediator;
        private SafetyMonitorInfo lastInfo;

        public SafetyHandler(IEventEmitter eventEmitter, ISafetyMonitorMediator safetyMonitorMediator) {
            emitter = eventEmitter;
            mediator = safetyMonitorMediator;
            mediator.RegisterConsumer(this);
            mediator.IsSafeChanged += MediatorOnIsSafeChanged;
            mediator.Connected += MediatorOnConnected;
            mediator.Disconnected += MediatorOnDisconnected;
        }

        public void UpdateDeviceInfo(SafetyMonitorInfo deviceInfo) {
            // Skip duplicates from internal nina polling
            if (deviceInfo.Equals(lastInfo)) return;

            var data = new SafetyDeviceInfoData {
                Connected = deviceInfo.Connected,
                IsSafe = deviceInfo.IsSafe
            };
            emitter.Enqueue("safety-monitor", "device-info", data);
            lastInfo = deviceInfo;
        }

        private void MediatorOnIsSafeChanged(object sender, IsSafeEventArgs e) {
            var data = new SafetyChangeData { IsSafe = e.IsSafe };
            emitter.Enqueue("safety-monitor", "is-safe", data);
        }

        private Task MediatorOnConnected(object arg1, EventArgs arg2) {
            var deviceInfo = mediator.GetInfo();
            var data = new DeviceConnectionData {
                Connected = true,
                DeviceType = DeviceType.SafetyMonitor,
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
            var data = new DeviceConnectionData { Connected = false, DeviceType = DeviceType.SafetyMonitor };
            emitter.Enqueue("device", "connection", data);
            return Task.CompletedTask;
        }

        public void Dispose() {
            mediator.Disconnected -= MediatorOnDisconnected;
            mediator.Connected -= MediatorOnConnected;
            mediator.IsSafeChanged -= MediatorOnIsSafeChanged;
            mediator.RemoveConsumer(this);
        }
    }
}