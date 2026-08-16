using System;

namespace Bortle.NINA.Emitter.Models {

    // Value-equality extension for the quicktype-generated DomeDeviceInfoData (Generated/EmitterModels.cs).
    // quicktype's C# target only emits plain classes, so this is a hand-written partial rather than a
    // generated record. Used by DomeHandler to skip emitting when NINA's internal polling produces no
    // actual change.

    public partial class DomeDeviceInfoData : IEquatable<DomeDeviceInfoData> {
        public bool Equals(DomeDeviceInfoData other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return AltitudeDegrees == other.AltitudeDegrees
                && ApplicationFollowing == other.ApplicationFollowing
                && AtHome == other.AtHome
                && AtPark == other.AtPark
                && AzimuthDegrees == other.AzimuthDegrees
                && CanFindHome == other.CanFindHome
                && CanPark == other.CanPark
                && CanSetAzimuth == other.CanSetAzimuth
                && CanSetPark == other.CanSetPark
                && CanSetShutter == other.CanSetShutter
                && CanSyncAzimuth == other.CanSyncAzimuth
                && Connected == other.Connected
                && DriverCanFollow == other.DriverCanFollow
                && DriverFollowing == other.DriverFollowing
                && string.Equals(FollowingType, other.FollowingType, StringComparison.Ordinal)
                && ShutterState == other.ShutterState
                && Slewing == other.Slewing;
        }

        public override bool Equals(object obj) => Equals(obj as DomeDeviceInfoData);

        public override int GetHashCode() {
            var hash = new HashCode();
            hash.Add(AltitudeDegrees);
            hash.Add(ApplicationFollowing);
            hash.Add(AtHome);
            hash.Add(AtPark);
            hash.Add(AzimuthDegrees);
            hash.Add(CanFindHome);
            hash.Add(CanPark);
            hash.Add(CanSetAzimuth);
            hash.Add(CanSetPark);
            hash.Add(CanSetShutter);
            hash.Add(CanSyncAzimuth);
            hash.Add(Connected);
            hash.Add(DriverCanFollow);
            hash.Add(DriverFollowing);
            hash.Add(FollowingType);
            hash.Add(ShutterState);
            hash.Add(Slewing);
            return hash.ToHashCode();
        }
    }
}