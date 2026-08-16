using System;

namespace Bortle.NINA.Emitter.Models {

    // Value-equality extension for the quicktype-generated RotatorDeviceInfoData (Generated/EmitterModels.cs).
    // quicktype's C# target only emits plain classes, so this is a hand-written partial rather than a
    // generated record. Used by RotatorHandler to skip emitting when NINA's internal polling produces no
    // actual change.

    public partial class RotatorDeviceInfoData : IEquatable<RotatorDeviceInfoData> {
        public bool Equals(RotatorDeviceInfoData other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return CanReverse == other.CanReverse
                && Connected == other.Connected
                && IsMoving == other.IsMoving
                && MechanicalPosition == other.MechanicalPosition
                && Position == other.Position
                && Reverse == other.Reverse
                && StepSize == other.StepSize
                && Synced == other.Synced;
        }

        public override bool Equals(object obj) => Equals(obj as RotatorDeviceInfoData);

        public override int GetHashCode() =>
            HashCode.Combine(CanReverse, Connected, IsMoving, MechanicalPosition, Position, Reverse, StepSize, Synced);
    }
}