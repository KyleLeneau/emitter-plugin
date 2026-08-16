using System;
using System.Collections.Generic;
using System.Linq;

namespace Bortle.NINA.Emitter.Models {

    // Value-equality extensions for the quicktype-generated switch models (Generated/EmitterModels.cs).
    // quicktype's C# target only emits plain classes, so these are hand-written partials rather than
    // generated records. Used by SwitchHandler to skip emitting when NINA's internal polling produces
    // no actual change.

    public partial class SwitchDeviceInfoData : IEquatable<SwitchDeviceInfoData> {
        public bool Equals(SwitchDeviceInfoData other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Connected == other.Connected
                && ListEquals(ReadableSwitches, other.ReadableSwitches)
                && ListEquals(WriteableSwitches, other.WriteableSwitches);
        }

        public override bool Equals(object obj) => Equals(obj as SwitchDeviceInfoData);

        public override int GetHashCode() {
            var hash = new HashCode();
            hash.Add(Connected);
            foreach (var s in ReadableSwitches ?? Enumerable.Empty<ReadableSwitch>()) hash.Add(s);
            foreach (var s in WriteableSwitches ?? Enumerable.Empty<WriteableSwitch>()) hash.Add(s);
            return hash.ToHashCode();
        }

        private static bool ListEquals<T>(List<T> a, List<T> b) where T : IEquatable<T> {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.SequenceEqual(b);
        }
    }

    public partial class ReadableSwitch : IEquatable<ReadableSwitch> {
        public bool Equals(ReadableSwitch other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Id == other.Id
                && Value == other.Value
                && string.Equals(Name, other.Name, StringComparison.Ordinal)
                && string.Equals(Description, other.Description, StringComparison.Ordinal);
        }

        public override bool Equals(object obj) => Equals(obj as ReadableSwitch);

        public override int GetHashCode() => HashCode.Combine(Id, Value, Name, Description);
    }

    public partial class WriteableSwitch : IEquatable<WriteableSwitch> {
        public bool Equals(WriteableSwitch other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Id == other.Id
                && Maximum == other.Maximum
                && Minimum == other.Minimum
                && StepSize == other.StepSize
                && TargetValue == other.TargetValue
                && Value == other.Value
                && string.Equals(Name, other.Name, StringComparison.Ordinal)
                && string.Equals(Description, other.Description, StringComparison.Ordinal);
        }

        public override bool Equals(object obj) => Equals(obj as WriteableSwitch);

        public override int GetHashCode() =>
            HashCode.Combine(Id, Maximum, Minimum, StepSize, TargetValue, Value, Name, Description);
    }
}