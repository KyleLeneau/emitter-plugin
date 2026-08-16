using System;

namespace Bortle.NINA.Emitter.Models {

    // Value-equality extensions for the quicktype-generated filter wheel models (Generated/EmitterModels.cs).
    // quicktype's C# target only emits plain classes, so these are hand-written partials rather than
    // generated records. Used by FilterWheelHandler to skip emitting when NINA's internal polling produces
    // no actual change.

    public partial class FilterWheelDeviceInfoData : IEquatable<FilterWheelDeviceInfoData> {
        public bool Equals(FilterWheelDeviceInfoData other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Connected == other.Connected
                && IsMoving == other.IsMoving
                && object.Equals(SelectedFilter, other.SelectedFilter);
        }

        public override bool Equals(object obj) => Equals(obj as FilterWheelDeviceInfoData);

        public override int GetHashCode() => HashCode.Combine(Connected, IsMoving, SelectedFilter);
    }

    public partial class FilterInfo : IEquatable<FilterInfo> {
        public bool Equals(FilterInfo other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(AutoFocusBinning, other.AutoFocusBinning, StringComparison.Ordinal)
                && AutoFocusGain == other.AutoFocusGain
                && AutoFocusOffset == other.AutoFocusOffset
                && AutoFocusTime == other.AutoFocusTime
                && IsAfFilter == other.IsAfFilter
                && string.Equals(Name, other.Name, StringComparison.Ordinal)
                && Offset == other.Offset
                && Postion == other.Postion;
        }

        public override bool Equals(object obj) => Equals(obj as FilterInfo);

        public override int GetHashCode() =>
            HashCode.Combine(AutoFocusBinning, AutoFocusGain, AutoFocusOffset, AutoFocusTime, IsAfFilter, Name, Offset, Postion);
    }
}