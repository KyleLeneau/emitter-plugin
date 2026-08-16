using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using NINA.Equipment.Equipment.MyDome;
using NINA.Equipment.Interfaces.Mediator;
using System;
using System.Threading.Tasks;
using NINAShutterState = NINA.Equipment.Interfaces.ShutterState;

namespace Bortle.NINA.Emitter.Handlers {
    public class DomeHandler : IDomeConsumer {
        private readonly IEventEmitter emitter;
        private readonly IDomeMediator mediator;
        private DomeDeviceInfoData lastData;

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
            var data = new DomeDeviceInfoData {
                Connected = deviceInfo.Connected,
                ShutterState = ToShutterState(deviceInfo.ShutterStatus),
                DriverCanFollow = deviceInfo.DriverCanFollow,
                CanSetShutter = deviceInfo.CanSetShutter,
                CanSetPark = deviceInfo.CanSetPark,
                CanSetAzimuth = deviceInfo.CanSetAzimuth,
                CanSyncAzimuth = deviceInfo.CanSyncAzimuth,
                CanPark = deviceInfo.CanPark,
                CanFindHome = deviceInfo.CanFindHome,
                AtPark = deviceInfo.AtPark,
                AtHome = deviceInfo.AtHome,
                DriverFollowing = deviceInfo.DriverFollowing,
                ApplicationFollowing = deviceInfo.ApplicationFollowing,
                FollowingType = deviceInfo.FollowingType,
                Slewing = deviceInfo.Slewing,
                AzimuthDegrees = deviceInfo.Azimuth,
                AltitudeDegrees = deviceInfo.Altitude,
            };

            // Skip duplicates from internal nina polling
            if (data.Equals(lastData)) return;

            emitter.Enqueue("dome", "device-info", data);
            lastData = data;
        }

        private Task MediatorOnConnected(object arg1, EventArgs arg2) {
            var deviceInfo = mediator.GetInfo();
            var data = new DeviceConnectionData {
                Connected = true,
                DeviceType = DeviceType.Dome,
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
            var data = new DeviceConnectionData { Connected = false, DeviceType = DeviceType.Dome };
            emitter.Enqueue("device", "connection", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnClosed(object arg1, EventArgs arg2) {
            var data = new DomeShutterData { Position = Position.Closed };
            emitter.Enqueue("dome", "shutter", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnHomed(object arg1, EventArgs arg2) {
            var data = new DomeShutterData { Position = Position.Homed };
            emitter.Enqueue("dome", "shutter", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnOpened(object arg1, EventArgs arg2) {
            var data = new DomeShutterData { Position = Position.Opened };
            emitter.Enqueue("dome", "shutter", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnParked(object arg1, EventArgs arg2) {
            var data = new DomeShutterData { Position = Position.Parked };
            emitter.Enqueue("dome", "shutter", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnSlewed(object arg1, DomeEventArgs arg2) {
            var data = new DomeShutterData { Position = Position.Slewed };
            emitter.Enqueue("dome", "shutter", data);
            return Task.CompletedTask;
        }

        private void MediatorOnSynced(object sender, EventArgs e) {
            var data = new DomeShutterData { Position = Position.Synced };
            emitter.Enqueue("dome", "shutter", data);
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

        private ShutterState ToShutterState(NINAShutterState state) {
            switch (state) {
                case NINAShutterState.ShutterNone:
                    return ShutterState.None;
                case NINAShutterState.ShutterOpen:
                    return ShutterState.Open;
                case NINAShutterState.ShutterClosed:
                    return ShutterState.Closed;
                case NINAShutterState.ShutterOpening:
                    return ShutterState.Opening;
                case NINAShutterState.ShutterClosing:
                    return ShutterState.Closing;
                case NINAShutterState.ShutterError:
                default:
                    return ShutterState.Error;
            }
        }
    }
}