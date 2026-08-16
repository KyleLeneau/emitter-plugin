using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using NINA.Equipment.Equipment.MySwitch;
using NINA.Equipment.Interfaces;
using NINA.Equipment.Interfaces.Mediator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Handlers {
    public class SwitchHandler : ISwitchConsumer {
        private readonly IEventEmitter emitter;
        private readonly ISwitchMediator mediator;
        private SwitchInfo lastInfo;

        public SwitchHandler(IEventEmitter eventEmitter, ISwitchMediator switchMediator) {
            emitter = eventEmitter;
            mediator = switchMediator;
            mediator.RegisterConsumer(this);
            mediator.Connected += MediatorOnConnected;
            mediator.Disconnected += MediatorOnDisconnected;
        }

        public void UpdateDeviceInfo(SwitchInfo deviceInfo) {
            // Skip duplicates from internal nina polling
            if (deviceInfo.Equals(lastInfo)) return;

            var data = new SwitchDeviceInfoData {
                Connected = deviceInfo.Connected,
                WriteableSwitches = deviceInfo.WritableSwitches != null ? deviceInfo.WritableSwitches.Select(w => new WriteableSwitch {
                    Id = w.Id,
                    Name = w.Name,
                    Description = w.Description,
                    Value = w.Value,
                    Maximum = w.Maximum,
                    Minimum = w.Minimum,
                    StepSize = w.StepSize,
                    TargetValue = w.TargetValue
                }).ToList() : [],
                ReadableSwitches = deviceInfo.ReadonlySwitches != null ? deviceInfo.ReadonlySwitches.Select(r => new ReadableSwitch {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Value = r.Value
                }).ToList() : []
            };
            emitter.Enqueue("switch", "device-info", data);
            lastInfo = deviceInfo;
        }

        private Task MediatorOnConnected(object arg1, EventArgs arg2) {
            var deviceInfo = mediator.GetInfo();
            var data = new DeviceConnectionData {
                Connected = true,
                DeviceType = DeviceType.Switch,
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
            var data = new DeviceConnectionData { Connected = false, DeviceType = DeviceType.Switch };
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