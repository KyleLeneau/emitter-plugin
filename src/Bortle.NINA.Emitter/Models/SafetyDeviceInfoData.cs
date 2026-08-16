using System;

namespace Bortle.NINA.Emitter.Models {

    // Value-equality extension for the quicktype-generated SafetyDeviceInfoData (Generated/EmitterModels.cs).
    // quicktype's C# target only emits plain classes, so this is a hand-written partial rather than a
    // generated record. Used by SafetyHandler to skip emitting when NINA's internal polling produces no
    // actual change.

    public partial class SafetyDeviceInfoData : IEquatable<SafetyDeviceInfoData> {
        public bool Equals(SafetyDeviceInfoData other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Connected == other.Connected
                && IsSafe == other.IsSafe;
        }

        public override bool Equals(object obj) => Equals(obj as SafetyDeviceInfoData);

        public override int GetHashCode() => HashCode.Combine(Connected, IsSafe);
    }
}