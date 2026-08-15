using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using NINA.Equipment.Equipment.MyFilterWheel;
using NINA.Equipment.Interfaces.Mediator;
using System;
using System.Threading.Tasks;
using NINAFilterInfo = NINA.Core.Model.Equipment.FilterInfo;

namespace Bortle.NINA.Emitter.Handlers {
    public class FilterWheelHandler : IFilterWheelConsumer {
        private readonly IEventEmitter emitter;
        private readonly IFilterWheelMediator mediator;
        private FilterWheelInfo lastInfo;

        public FilterWheelHandler(IEventEmitter eventEmitter, IFilterWheelMediator filterWheelMediator) {
            emitter = eventEmitter;
            mediator = filterWheelMediator;
            mediator.RegisterConsumer(this);
            mediator.Connected += MediatorOnConnected;
            mediator.Disconnected += MediatorOnDisconnected;
            mediator.FilterChanged += MediatorOnFilterChanged;
        }

        public void UpdateDeviceInfo(FilterWheelInfo deviceInfo) {
            // Skip duplicates from internal nina polling
            if (deviceInfo.Equals(lastInfo)) return;

            var data = new FilterWheelDeviceInfoData {
                Connected = deviceInfo.Connected,
                IsMoving = deviceInfo.IsMoving,
                SelectedFilter = ToFilterInfo(deviceInfo.SelectedFilter)
            };
            emitter.Enqueue("filter-wheel", "device-info", data);
            lastInfo = deviceInfo;
        }

        private Task MediatorOnConnected(object arg1, EventArgs arg2) {
            var deviceInfo = mediator.GetInfo();
            var data = new DeviceConnectionData {
                Connected = true,
                DeviceType = DeviceType.FilterWheel,
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
            var data = new DeviceConnectionData { Connected = false, DeviceType = DeviceType.FilterWheel };
            emitter.Enqueue("device", "connection", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnFilterChanged(object arg1, FilterChangedEventArgs arg2) {
            // TODO: Implement event
            return Task.CompletedTask;
        }

        public void Dispose() {
            mediator.FilterChanged -= MediatorOnFilterChanged;
            mediator.Disconnected -= MediatorOnDisconnected;
            mediator.Connected -= MediatorOnConnected;
            mediator.RemoveConsumer(this);
        }

        private FilterInfo ToFilterInfo(NINAFilterInfo filterInfo) {
            if (filterInfo == null) return null;

            return new FilterInfo {
                Name = filterInfo.Name,
                Offset = filterInfo.FocusOffset,
                Postion = filterInfo.Position,
                AutoFocusTime = filterInfo.AutoFocusExposureTime,
                IsAfFilter = filterInfo.AutoFocusFilter,
                AutoFocusBinning = filterInfo.AutoFocusBinning.ToString(),
                AutoFocusGain = filterInfo.AutoFocusGain,
                AutoFocusOffset = filterInfo.AutoFocusOffset
            };
        }
    }
}