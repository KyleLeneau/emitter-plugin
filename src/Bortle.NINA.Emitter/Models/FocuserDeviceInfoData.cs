using System;

namespace Bortle.NINA.Emitter.Models {

    // Value-equality extension for the quicktype-generated FocuserDeviceInfoData (Generated/EmitterModels.cs).
    // quicktype's C# target only emits plain classes, so this is a hand-written partial rather than a
    // generated record. Used by FocuserHandler to skip emitting when NINA's internal polling produces no
    // actual change.

    public partial class FocuserDeviceInfoData : IEquatable<FocuserDeviceInfoData> {
        public bool Equals(FocuserDeviceInfoData other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Connected == other.Connected
                && IsMoving == other.IsMoving
                && IsSettling == other.IsSettling
                && Position == other.Position
                && StepSize == other.StepSize
                && TempComp == other.TempComp
                && TempCompAvailable == other.TempCompAvailable
                && Temperature == other.Temperature;
        }

        public override bool Equals(object obj) => Equals(obj as FocuserDeviceInfoData);

        public override int GetHashCode() =>
            HashCode.Combine(Connected, IsMoving, IsSettling, Position, StepSize, TempComp, TempCompAvailable, Temperature);
    }
}