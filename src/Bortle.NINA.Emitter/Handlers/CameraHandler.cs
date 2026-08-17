using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using NINA.Core.Enum;
using NINA.Equipment.Equipment.MyCamera;
using NINA.Equipment.Interfaces.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using SensorType = Bortle.NINA.Emitter.Models.SensorType;

namespace Bortle.NINA.Emitter.Handlers {
    public class CameraHandler : ICameraConsumer {
        private readonly IEventEmitter emitter;
        private readonly ICameraMediator mediator;
        private CameraDeviceInfoData lastData;

        public CameraHandler(IEventEmitter eventEmitter, ICameraMediator cameraMediator) {
            emitter = eventEmitter;
            mediator = cameraMediator;
            mediator.RegisterConsumer(this);
            mediator.Connected += MediatorOnConnected;
            mediator.Disconnected += MediatorOnDisconnected;
            mediator.DownloadTimeout += MediatorOnDownloadTimeout;
        }

        public void UpdateDeviceInfo(CameraInfo deviceInfo) {
            var data = new CameraDeviceInfoData {
                Connected = deviceInfo.Connected,
                CanSetTemperature = deviceInfo.CanSetTemperature,
                HasShutter = deviceInfo.HasShutter,
                Temperature = deviceInfo.Temperature,
                Gain = deviceInfo.Gain == -1 ? null : deviceInfo.Gain,
                DefaultGain = deviceInfo.DefaultGain == -1 ? null : deviceInfo.DefaultGain,
                ElectronsPerAdu = deviceInfo.ElectronsPerADU == -1 ? null : deviceInfo.ElectronsPerADU,
                BinX = deviceInfo.BinX,
                BinY = deviceInfo.BinY,
                BitDepth = deviceInfo.BitDepth,
                CanSetOffset = deviceInfo.CanSetOffset,
                OffsetMin = deviceInfo.OffsetMin,
                OffsetMax = deviceInfo.OffsetMax,
                Offset = deviceInfo.Offset,
                DefaultOffset = deviceInfo.DefaultOffset == -1 ? null : deviceInfo.DefaultOffset,
                UsbLimit = deviceInfo.USBLimit,
                IsSubSampleEnabled = deviceInfo.IsSubSampleEnabled,
                CameraState = ToCameraState(deviceInfo.CameraState),
                XSize = deviceInfo.XSize,
                YSize = deviceInfo.YSize,
                PixelSize = deviceInfo.PixelSize,
                HasBattery = deviceInfo.HasBattery,
                Battery = deviceInfo.Battery,
                GainMin = deviceInfo.GainMin,
                GainMax = deviceInfo.GainMax,
                CanSetGain = deviceInfo.CanSetGain,
                CanGetGain = deviceInfo.CanGetGain,
                Gains = deviceInfo.Gains.Select(i => (long)i).ToList(),
                CoolerOn = deviceInfo.CoolerOn,
                CoolerPower = deviceInfo.CoolerPower,
                HasDewHeater = deviceInfo.HasDewHeater,
                DewHeaterOn = deviceInfo.DewHeaterOn,
                CanSubSample = deviceInfo.CanSubSample,
                SubSampleX = deviceInfo.SubSampleX,
                SubSampleY = deviceInfo.SubSampleY,
                SubSampleWidth = deviceInfo.SubSampleWidth,
                SubSampleHeight = deviceInfo.SubSampleHeight,
                TemeratureSetPoint = deviceInfo.TemperatureSetPoint,
                ReadoutModes = deviceInfo.ReadoutModes != null ? deviceInfo.ReadoutModes.ToList() : [],
                ReadoutMode = deviceInfo.ReadoutMode,
                SnapReadoutMode = deviceInfo.ReadoutModeForSnapImages,
                NormalReadoutMode = deviceInfo.ReadoutModeForNormalImages,
                IsExposing = deviceInfo.IsExposing,
                ExposureEndTime = deviceInfo.ExposureEndTime,
                LastDownloadTime = deviceInfo.LastDownloadTime == -1 ? null : deviceInfo.LastDownloadTime,
                SensorType = ToSensorType(deviceInfo.SensorType),
                BayerOffsetX = deviceInfo.BayerOffsetX,
                BayerOffsetY = deviceInfo.BayerOffsetY,
                BinningModes = deviceInfo.BinningModes.Select(b => new BinningMode { X = b.X, Y = b.Y }).ToList(),
                ExposureMax = deviceInfo.ExposureMax,
                ExposureMin = deviceInfo.ExposureMin,
                LiveViewEnabled = deviceInfo.LiveViewEnabled,
                CanShowLiveView = deviceInfo.CanShowLiveView,
                CanSetUsbLimit = deviceInfo.CanSetUSBLimit,
                UsbLimitMin = deviceInfo.USBLimitMin,
                UsbLimitMax = deviceInfo.USBLimitMax
            };

            // Skip duplicates from internal nina polling
            if (data.Equals(lastData)) return;

            emitter.Enqueue("camera", "device-info", data);
            lastData = data;
        }

        private Task MediatorOnConnected(object arg1, EventArgs arg2) {
            var deviceInfo = mediator.GetInfo();
            var data = new DeviceConnectionData {
                Connected = true,
                DeviceType = DeviceType.Camera,
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
            var data = new DeviceConnectionData { Connected = false, DeviceType = DeviceType.Camera };
            emitter.Enqueue("device", "connection", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnDownloadTimeout(object arg1, EventArgs arg2) {
            var data = new CameraErrorData { Error = Error.DownloadTimeout };
            emitter.Enqueue("camera", "error", data);
            return Task.CompletedTask;
        }

        public void Dispose() {
            mediator.DownloadTimeout -= MediatorOnDownloadTimeout;
            mediator.Disconnected -= MediatorOnDisconnected;
            mediator.Connected -= MediatorOnConnected;
            mediator.RemoveConsumer(this);
        }

        private CameraState? ToCameraState(CameraStates state) {
            return state switch {
                CameraStates.NoState => CameraState.None,
                CameraStates.Idle => CameraState.Idle,
                CameraStates.Waiting => CameraState.Waiting,
                CameraStates.Exposing => CameraState.Exposing,
                CameraStates.Reading => CameraState.Reading,
                CameraStates.Download => CameraState.Download,
                CameraStates.LoadingFile => CameraState.LoadingFile,
                CameraStates.Error => CameraState.Error,
                _ => CameraState.Error
            };
        }

        private SensorType? ToSensorType(global::NINA.Core.Enum.SensorType type) {
            return type switch {
                global::NINA.Core.Enum.SensorType.Monochrome => SensorType.Monochrome,
                global::NINA.Core.Enum.SensorType.Color => SensorType.Color,
                global::NINA.Core.Enum.SensorType.RGGB => SensorType.Rggb,
                global::NINA.Core.Enum.SensorType.CMYG => SensorType.Cmyg,
                global::NINA.Core.Enum.SensorType.CMYG2 => SensorType.Cmyg2,
                global::NINA.Core.Enum.SensorType.LRGB => SensorType.Lrgb,
                global::NINA.Core.Enum.SensorType.BGGR => SensorType.Bggr,
                global::NINA.Core.Enum.SensorType.GBRG => SensorType.Gbrg,
                global::NINA.Core.Enum.SensorType.GRBG => SensorType.Grbg,
                global::NINA.Core.Enum.SensorType.GRGB => SensorType.Grgb,
                global::NINA.Core.Enum.SensorType.GBGR => SensorType.Gbgr,
                global::NINA.Core.Enum.SensorType.RGBG => SensorType.Rgbg,
                global::NINA.Core.Enum.SensorType.BGRG => SensorType.Bgrg,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}