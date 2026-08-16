using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using NINA.Core.Interfaces;
using NINA.Equipment.Equipment.MyGuider;
using NINA.Equipment.Interfaces.Mediator;
using System;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Handlers {
    public class GuiderHandler : IGuiderConsumer {
        private readonly IEventEmitter emitter;
        private readonly IGuiderMediator mediator;
        private GuiderDeviceInfoData lastData;

        public GuiderHandler(IEventEmitter eventEmitter, IGuiderMediator guiderMediator) {
            emitter = eventEmitter;
            mediator = guiderMediator;
            mediator.RegisterConsumer(this);
            mediator.Connected += MediatorOnConnected;
            mediator.Disconnected += MediatorOnDisconnected;
            mediator.AfterDither += MediatorOnAfterDither;
            mediator.GuideEvent += MediatorOnGuideEvent;
            mediator.GuidingStarted += MediatorOnGuidingStarted;
            mediator.GuidingStopped += MediatorOnGuidingStopped;
        }

        public void UpdateDeviceInfo(GuiderInfo deviceInfo) {
            var data = new GuiderDeviceInfoData {
                Connected = deviceInfo.Connected,
                CanClearCalibration = deviceInfo.CanClearCalibration,
                CanSetShiftRate = deviceInfo.CanSetShiftRate,
                CanGetLockPostion = deviceInfo.CanGetLockPosition,
                RmsError = ToRmsError(deviceInfo.RMSError),
                PixelScale = deviceInfo.PixelScale,
            };

            // Skip duplicates from internal nina polling
            if (data.Equals(lastData)) return;

            emitter.Enqueue("guider", "device-info", data);
            lastData = data;
        }

        private Task MediatorOnConnected(object arg1, EventArgs arg2) {
            var deviceInfo = mediator.GetInfo();
            var data = new DeviceConnectionData {
                Connected = true,
                DeviceType = DeviceType.Guider,
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
            var data = new DeviceConnectionData { Connected = false, DeviceType = DeviceType.Guider };
            emitter.Enqueue("device", "connection", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnAfterDither(object arg1, EventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private void MediatorOnGuideEvent(object sender, IGuideStep e) {
            // TODO: Implement event
        }

        private Task MediatorOnGuidingStarted(object arg1, EventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        private Task MediatorOnGuidingStopped(object arg1, EventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        public void Dispose() {
            mediator.GuidingStopped -= MediatorOnGuidingStopped;
            mediator.GuidingStarted -= MediatorOnGuidingStarted;
            mediator.GuideEvent -= MediatorOnGuideEvent;
            mediator.AfterDither -= MediatorOnAfterDither;
            mediator.Disconnected -= MediatorOnDisconnected;
            mediator.Connected -= MediatorOnConnected;
            mediator.RemoveConsumer(this);
        }

        private RmsError ToRmsError(RMSError rmsError) {
            if (rmsError == null) return null;

            return new RmsError {
                Ra = ToRmsUnit(rmsError.RA),
                Dec = ToRmsUnit(rmsError.Dec),
                Total = ToRmsUnit(rmsError.Total),
                PeakRa = ToRmsUnit(rmsError.PeakRA),
                PeakDec = ToRmsUnit(rmsError.PeakDec),
            };
        }

        private RmsUnit ToRmsUnit(RMSUnit unit) {
            return new RmsUnit { Pixel = unit.Pixel, ArcSeconds = unit.Arcseconds };
        }
    }
}