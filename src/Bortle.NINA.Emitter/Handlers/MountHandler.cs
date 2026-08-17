using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using Bortle.NINA.Emitter.Utils;
using NINA.Equipment.Equipment.MyTelescope;
using NINA.Equipment.Interfaces.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Handlers {
    public class MountHandler : ITelescopeConsumer {
        private readonly IEventEmitter emitter;
        private readonly ITelescopeMediator mediator;
        private MountDeviceInfoData lastData;

        public MountHandler(IEventEmitter eventEmitter, ITelescopeMediator telescopeMediator) {
            emitter = eventEmitter;
            mediator = telescopeMediator;
            mediator.RegisterConsumer(this);
            mediator.Connected += MediatorOnConnected;
            mediator.Disconnected += MediatorOnDisconnected;
            mediator.AfterMeridianFlip += MediatorOnAfterMeridianFlip;
            mediator.BeforeMeridianFlip += MediatorOnBeforeMeridianFlip;
            mediator.Homed += MediatorOnHomed;
            mediator.Parked += MediatorOnParked;
            mediator.Slewed += MediatorOnSlewed;
            mediator.Unparked += MediatorOnUnparked;
        }

        public void UpdateDeviceInfo(TelescopeInfo deviceInfo) {
            var data = new MountDeviceInfoData {
                Connected = deviceInfo.Connected,
                SiderealTime = deviceInfo.SiderealTime.Optional(),
                RightAscension = deviceInfo.RightAscension.Optional(),
                Declination = deviceInfo.Declination.Optional(),
                SiteLatitude = deviceInfo.SiteLatitude,
                SiteLongitude = deviceInfo.SiteLongitude,
                SiteElevation = deviceInfo.SiteElevation,
                TimeToMeridianFlip = deviceInfo.TimeToMeridianFlip.Optional(),
                SideOfPier = ToPierSide(deviceInfo.SideOfPier),
                Altitude = deviceInfo.Altitude.Optional(),
                Azimuth = deviceInfo.Azimuth.Optional(),
                AtPark = deviceInfo.AtPark,
                TrackingRate = ToTrackingRate(deviceInfo.TrackingRate),
                TrackingEnabled = deviceInfo.TrackingEnabled,
                AtHome = deviceInfo.AtHome,
                CanFindHome = deviceInfo.CanFindHome,
                CanPark = deviceInfo.CanPark,
                CanSetPark = deviceInfo.CanSetPark,
                CanSetTracking = deviceInfo.CanSetTrackingEnabled,
                CanSetDeclinationRate = deviceInfo.CanSetDeclinationRate,
                CanSetRightAscensionRate = deviceInfo.CanSetRightAscensionRate,
                EquatorialSystem = AstrometryCoordinates.ToEpoch(deviceInfo.EquatorialSystem),
                HasUnknownEpoch = deviceInfo.HasUnknownEpoch,
                TargetCoordinates = AstrometryCoordinates.ToCoordinates(deviceInfo.TargetCoordinates),
                TargetSideOfPier = ToPierSide(deviceInfo.TargetSideOfPier),
                Slewing = deviceInfo.Slewing,
                GuideRateRaArcSecPerSec = deviceInfo.GuideRateRightAscensionArcsecPerSec.Optional(),
                GuideRateDecArcSecPerSec = deviceInfo.GuideRateDeclinationArcsecPerSec.Optional(),
                CanMovePrimaryAxis = deviceInfo.CanMovePrimaryAxis,
                CanMoveSecondaryAxis = deviceInfo.CanMoveSecondaryAxis,
                PrimaryAxisRates = deviceInfo.PrimaryAxisRates != null ? deviceInfo.PrimaryAxisRates.Select(v => (List<double>)[v.Item1, v.Item2]).ToList() : [],
                SecondaryAxisRates = deviceInfo.SecondaryAxisRates != null ? deviceInfo.SecondaryAxisRates.Select(v => (List<double>)[v.Item1, v.Item2]).ToList() : [],
                AlignmentMode = ToAlignmentMode(deviceInfo.AlignmentMode),
                CanPulseGuide = deviceInfo.CanPulseGuide,
                IsPulseGuiding = deviceInfo.IsPulseGuiding,
                CanSetPierSide = deviceInfo.CanSetPierSide,
                CanSlew = deviceInfo.CanSlew,
                CanSlewAltAz = deviceInfo.CanSlewAltAz,
                UtcDate = deviceInfo.UTCDate
            };

            // Skip duplicates from internal nina polling
            if (data.Equals(lastData)) return;

            emitter.Enqueue("mount", "device-info", data);
            lastData = data;
        }

        private Task MediatorOnConnected(object arg1, EventArgs arg2) {
            var deviceInfo = mediator.GetInfo();
            var data = new DeviceConnectionData {
                Connected = true,
                DeviceType = DeviceType.Mount,
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
            var data = new DeviceConnectionData { Connected = false, DeviceType = DeviceType.Mount };
            emitter.Enqueue("device", "connection", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnAfterMeridianFlip(object arg1, AfterMeridianFlipEventArgs arg2) {
            var data = new MountFlipData {
                FlipEvent = FlipEvent.After,
                Success = arg2.Success,
                TargetCoordinates = AstrometryCoordinates.ToCoordinates(arg2.Target)
            };
            emitter.Enqueue("mount", "flip", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnBeforeMeridianFlip(object arg1, BeforeMeridianFlipEventArgs arg2) {
            var data = new MountFlipData {
                FlipEvent = FlipEvent.Before,
                TargetCoordinates = AstrometryCoordinates.ToCoordinates(arg2.Target)
            };
            emitter.Enqueue("mount", "flip", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnHomed(object arg1, EventArgs arg2) {
            var data = new MountMovedData { MoveType = MoveType.Homed };
            emitter.Enqueue("mount", "move", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnParked(object arg1, EventArgs arg2) {
            var data = new MountMovedData { MoveType = MoveType.Parked };
            emitter.Enqueue("mount", "move", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnSlewed(object arg1, MountSlewedEventArgs arg2) {
            var data = new MountMovedData {
                MoveType = MoveType.Slewed,
                FromCoordinates = AstrometryCoordinates.ToCoordinates(arg2.From),
                ToCoordinates = AstrometryCoordinates.ToCoordinates(arg2.To)
            };
            emitter.Enqueue("mount", "move", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnUnparked(object arg1, EventArgs arg2) {
            var data = new MountMovedData { MoveType = MoveType.Unparked };
            emitter.Enqueue("mount", "move", data);
            return Task.CompletedTask;
        }

        public void Dispose() {
            mediator.Unparked -= MediatorOnUnparked;
            mediator.Slewed -= MediatorOnSlewed;
            mediator.Parked -= MediatorOnParked;
            mediator.Homed -= MediatorOnHomed;
            mediator.BeforeMeridianFlip -= MediatorOnBeforeMeridianFlip;
            mediator.AfterMeridianFlip -= MediatorOnAfterMeridianFlip;
            mediator.Disconnected -= MediatorOnDisconnected;
            mediator.Connected -= MediatorOnConnected;
            mediator.RemoveConsumer(this);
        }

        private PierSide? ToPierSide(global::NINA.Core.Enum.PierSide? side) {
            return side switch {
                global::NINA.Core.Enum.PierSide.pierEast => PierSide.East,
                global::NINA.Core.Enum.PierSide.pierWest => PierSide.West,
                global::NINA.Core.Enum.PierSide.pierUnknown => PierSide.Unknown,
                _ => PierSide.Unknown
            };
        }

        private TrackingRate ToTrackingRate(global::NINA.Equipment.Interfaces.TrackingRate rate) {
            return new TrackingRate() {
                TrackingMode = ToTrackingMode(rate.TrackingMode),
                CustomRaRate = rate.CustomRightAscensionRate,
                CustomDecRate = rate.CustomDeclinationRate
            };
        }

        private TrackingMode? ToTrackingMode(global::NINA.Equipment.Interfaces.TrackingMode mode) {
            return mode switch {
                global::NINA.Equipment.Interfaces.TrackingMode.Sidereal => TrackingMode.Sidereal,
                global::NINA.Equipment.Interfaces.TrackingMode.Lunar => TrackingMode.Lunar,
                global::NINA.Equipment.Interfaces.TrackingMode.Solar => TrackingMode.Solar,
                global::NINA.Equipment.Interfaces.TrackingMode.King => TrackingMode.King,
                global::NINA.Equipment.Interfaces.TrackingMode.Custom => TrackingMode.Custom,
                global::NINA.Equipment.Interfaces.TrackingMode.Stopped => TrackingMode.Stopped,
                _ => null
            };
        }



        private AlignmentMode? ToAlignmentMode(global::NINA.Core.Enum.AlignmentMode mode) {
            return mode switch {
                global::NINA.Core.Enum.AlignmentMode.AltAz => AlignmentMode.AltAz,
                global::NINA.Core.Enum.AlignmentMode.Polar => AlignmentMode.Polar,
                global::NINA.Core.Enum.AlignmentMode.GermanPolar => AlignmentMode.GermanPolar,
                _ => null
            };
        }
    }
}