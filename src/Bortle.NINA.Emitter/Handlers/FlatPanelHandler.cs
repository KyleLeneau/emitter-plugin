using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using NINA.Equipment.Equipment.MyFlatDevice;
using NINA.Equipment.Interfaces.Mediator;
using System;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Handlers {
    public class FlatPanelHandler : IFlatDeviceConsumer {
        private readonly IEventEmitter emitter;
        private readonly IFlatDeviceMediator mediator;
        private FlatDeviceInfo lastInfo;

        public FlatPanelHandler(IEventEmitter eventEmitter, IFlatDeviceMediator flatDeviceMediator) {
            emitter = eventEmitter;
            mediator = flatDeviceMediator;
            mediator.RegisterConsumer(this);
            mediator.Connected += MediatorOnConnected;
            mediator.Disconnected += MediatorOnDisconnected;
            mediator.BrightnessChanged += MediatorOnBrightnessChanged;
            mediator.Closed += MediatorOnClosed;
            mediator.Opened += MediatorOnOpened;
            mediator.LightToggled += MediatorOnLightToggled;
        }

        public void UpdateDeviceInfo(FlatDeviceInfo deviceInfo) {
            // Skip duplicates from internal nina polling
            if (deviceInfo.Equals(lastInfo)) return;

            var data = new FlatPanelData {
                Connected = deviceInfo.Connected,
                Brightness = deviceInfo.Brightness,
                CoverState = deviceInfo.CoverState.ToString(),
                LightOn = deviceInfo.LightOn,
                MaxBrightness = deviceInfo.MaxBrightness,
                MinBrightness = deviceInfo.MinBrightness,
                SupportsOnOff = deviceInfo.SupportsOnOff,
                SupportsOpenClose = deviceInfo.SupportsOpenClose,
            };

            emitter.Enqueue("flat_panel", "device-info", data);
            lastInfo = deviceInfo;
        }

        private Task MediatorOnConnected(object arg1, EventArgs arg2) {
            var deviceInfo = mediator.GetInfo();
            var data = new DeviceConnectionData {
                Connected = true,
                DeviceType = "FlatPanel",
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
            var data = new DeviceConnectionData { Connected = false, DeviceType = "FlatPanel" };
            emitter.Enqueue("device", "connection", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnBrightnessChanged(object arg1, FlatDeviceBrightnessChangedEventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private Task MediatorOnClosed(object arg1, EventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private Task MediatorOnOpened(object arg1, EventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private Task MediatorOnLightToggled(object arg1, EventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        public void Dispose() {
            mediator.LightToggled -= MediatorOnLightToggled;
            mediator.Opened -= MediatorOnOpened;
            mediator.Closed -= MediatorOnClosed;
            mediator.BrightnessChanged -= MediatorOnBrightnessChanged;
            mediator.Disconnected -= MediatorOnDisconnected;
            mediator.Connected -= MediatorOnConnected;
            mediator.RemoveConsumer(this);
        }
    }
}