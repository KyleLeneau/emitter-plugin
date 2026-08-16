using System;

namespace Bortle.NINA.Emitter.Models {

    // Value-equality extensions for the quicktype-generated guider models (Generated/EmitterModels.cs).
    // quicktype's C# target only emits plain classes, so these are hand-written partials rather than
    // generated records. Used by GuiderHandler to skip emitting when NINA's internal polling produces no
    // actual change.

    public partial class GuiderDeviceInfoData : IEquatable<GuiderDeviceInfoData> {
        public bool Equals(GuiderDeviceInfoData other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return CanClearCalibration == other.CanClearCalibration
                && CanGetLockPostion == other.CanGetLockPostion
                && CanSetShiftRate == other.CanSetShiftRate
                && Connected == other.Connected
                && PixelScale == other.PixelScale
                && object.Equals(RmsError, other.RmsError);
        }

        public override bool Equals(object obj) => Equals(obj as GuiderDeviceInfoData);

        public override int GetHashCode() =>
            HashCode.Combine(CanClearCalibration, CanGetLockPostion, CanSetShiftRate, Connected, PixelScale, RmsError);
    }

    public partial class RmsError : IEquatable<RmsError> {
        public bool Equals(RmsError other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return object.Equals(Dec, other.Dec)
                && object.Equals(PeakDec, other.PeakDec)
                && object.Equals(PeakRa, other.PeakRa)
                && object.Equals(Ra, other.Ra)
                && object.Equals(Total, other.Total);
        }

        public override bool Equals(object obj) => Equals(obj as RmsError);

        public override int GetHashCode() => HashCode.Combine(Dec, PeakDec, PeakRa, Ra, Total);
    }

    public partial class RmsUnit : IEquatable<RmsUnit> {
        public bool Equals(RmsUnit other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return ArcSeconds == other.ArcSeconds && Pixel == other.Pixel;
        }

        public override bool Equals(object obj) => Equals(obj as RmsUnit);

        public override int GetHashCode() => HashCode.Combine(ArcSeconds, Pixel);
    }
}