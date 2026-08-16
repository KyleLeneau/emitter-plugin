using System;
using System.Collections.Generic;
using System.Linq;

namespace Bortle.NINA.Emitter.Models {

    // Value-equality extensions for the quicktype-generated mount models (Generated/EmitterModels.cs).
    // quicktype's C# target only emits plain classes, so these are hand-written partials rather than
    // generated records. Used by MountHandler to skip emitting when NINA's internal polling produces no
    // actual change.

    public partial class MountDeviceInfoData : IEquatable<MountDeviceInfoData> {
        public bool Equals(MountDeviceInfoData other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return AlignmentMode == other.AlignmentMode
                && Altitude == other.Altitude
                && AtHome == other.AtHome
                && AtPark == other.AtPark
                && Azimuth == other.Azimuth
                && CanFindHome == other.CanFindHome
                && CanMovePrimaryAxis == other.CanMovePrimaryAxis
                && CanMoveSecondaryAxis == other.CanMoveSecondaryAxis
                && CanPark == other.CanPark
                && CanPulseGuide == other.CanPulseGuide
                && CanSetDeclinationRate == other.CanSetDeclinationRate
                && CanSetPark == other.CanSetPark
                && CanSetPierSide == other.CanSetPierSide
                && CanSetRightAscensionRate == other.CanSetRightAscensionRate
                && CanSetTracking == other.CanSetTracking
                && CanSlew == other.CanSlew
                && CanSlewAltAz == other.CanSlewAltAz
                && Connected == other.Connected
                && Declination == other.Declination
                && EquatorialSystem == other.EquatorialSystem
                && GuideRateDecArcSecPerSec == other.GuideRateDecArcSecPerSec
                && GuideRateRaArcSecPerSec == other.GuideRateRaArcSecPerSec
                && HasUnknownEpoch == other.HasUnknownEpoch
                && IsPulseGuiding == other.IsPulseGuiding
                && NestedListEquals(PrimaryAxisRates, other.PrimaryAxisRates)
                && RightAscension == other.RightAscension
                && NestedListEquals(SecondaryAxisRates, other.SecondaryAxisRates)
                && SideOfPier == other.SideOfPier
                && SiderealTime == other.SiderealTime
                && SiteElevation == other.SiteElevation
                && SiteLatitude == other.SiteLatitude
                && SiteLongitude == other.SiteLongitude
                && Slewing == other.Slewing
                && object.Equals(TargetCoordinates, other.TargetCoordinates)
                && TargetSideOfPier == other.TargetSideOfPier
                && TimeToMeridianFlip == other.TimeToMeridianFlip
                && TrackingEnabled == other.TrackingEnabled
                && ListEquals(TrackingModes, other.TrackingModes)
                && object.Equals(TrackingRate, other.TrackingRate)
                && UtcDate == other.UtcDate;
        }

        public override bool Equals(object obj) => Equals(obj as MountDeviceInfoData);

        public override int GetHashCode() {
            var hash = new HashCode();
            hash.Add(AlignmentMode);
            hash.Add(Altitude);
            hash.Add(AtHome);
            hash.Add(AtPark);
            hash.Add(Azimuth);
            hash.Add(CanFindHome);
            hash.Add(CanMovePrimaryAxis);
            hash.Add(CanMoveSecondaryAxis);
            hash.Add(CanPark);
            hash.Add(CanPulseGuide);
            hash.Add(CanSetDeclinationRate);
            hash.Add(CanSetPark);
            hash.Add(CanSetPierSide);
            hash.Add(CanSetRightAscensionRate);
            hash.Add(CanSetTracking);
            hash.Add(CanSlew);
            hash.Add(CanSlewAltAz);
            hash.Add(Connected);
            hash.Add(Declination);
            hash.Add(EquatorialSystem);
            hash.Add(GuideRateDecArcSecPerSec);
            hash.Add(GuideRateRaArcSecPerSec);
            hash.Add(HasUnknownEpoch);
            hash.Add(IsPulseGuiding);
            hash.Add(PrimaryAxisRates?.Count);
            hash.Add(RightAscension);
            hash.Add(SecondaryAxisRates?.Count);
            hash.Add(SideOfPier);
            hash.Add(SiderealTime);
            hash.Add(SiteElevation);
            hash.Add(SiteLatitude);
            hash.Add(SiteLongitude);
            hash.Add(Slewing);
            hash.Add(TargetCoordinates);
            hash.Add(TargetSideOfPier);
            hash.Add(TimeToMeridianFlip);
            hash.Add(TrackingEnabled);
            hash.Add(TrackingModes?.Count);
            hash.Add(TrackingRate);
            hash.Add(UtcDate);
            return hash.ToHashCode();
        }

        private static bool ListEquals<T>(List<T> a, List<T> b) {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.SequenceEqual(b);
        }

        private static bool NestedListEquals(List<List<double>> a, List<List<double>> b) {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            if (a.Count != b.Count) return false;

            for (var i = 0; i < a.Count; i++) {
                if (!ListEquals(a[i], b[i])) return false;
            }
            return true;
        }
    }

    public partial class Coordinates : IEquatable<Coordinates> {
        public bool Equals(Coordinates other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return DecDegrees == other.DecDegrees && Epoch == other.Epoch && RaDegrees == other.RaDegrees;
        }

        public override bool Equals(object obj) => Equals(obj as Coordinates);

        public override int GetHashCode() => HashCode.Combine(DecDegrees, Epoch, RaDegrees);
    }

    public partial class TrackingRate : IEquatable<TrackingRate> {
        public bool Equals(TrackingRate other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return CustomDecRate == other.CustomDecRate
                && CustomRaRate == other.CustomRaRate
                && TrackingMode == other.TrackingMode;
        }

        public override bool Equals(object obj) => Equals(obj as TrackingRate);

        public override int GetHashCode() => HashCode.Combine(CustomDecRate, CustomRaRate, TrackingMode);
    }
}