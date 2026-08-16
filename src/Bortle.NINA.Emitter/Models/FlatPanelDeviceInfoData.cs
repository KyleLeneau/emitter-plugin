using System;

namespace Bortle.NINA.Emitter.Models {

    // Value-equality extension for the quicktype-generated FlatPanelDeviceInfoData (Generated/EmitterModels.cs).
    // quicktype's C# target only emits plain classes, so this is a hand-written partial rather than a
    // generated record. Used by FlatPanelHandler to skip emitting when NINA's internal polling produces no
    // actual change.

    public partial class FlatPanelDeviceInfoData : IEquatable<FlatPanelDeviceInfoData> {
        public bool Equals(FlatPanelDeviceInfoData other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Brightness == other.Brightness
                && Connected == other.Connected
                && string.Equals(CoverState, other.CoverState, StringComparison.Ordinal)
                && LightOn == other.LightOn
                && MaxBrightness == other.MaxBrightness
                && MinBrightness == other.MinBrightness
                && SupportsOnOff == other.SupportsOnOff
                && SupportsOpenClose == other.SupportsOpenClose;
        }

        public override bool Equals(object obj) => Equals(obj as FlatPanelDeviceInfoData);

        public override int GetHashCode() =>
            HashCode.Combine(Brightness, Connected, CoverState, LightOn, MaxBrightness, MinBrightness, SupportsOnOff, SupportsOpenClose);
    }
}