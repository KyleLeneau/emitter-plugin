using System;
using System.Collections.Generic;
using System.Linq;

namespace Bortle.NINA.Emitter.Models {

    // Value-equality extensions for the quicktype-generated camera models (Generated/EmitterModels.cs).
    // quicktype's C# target only emits plain classes, so these are hand-written partials rather than
    // generated records. Used by CameraHandler to skip emitting when NINA's internal polling produces no
    // actual change.

    public partial class CameraDeviceInfoData : IEquatable<CameraDeviceInfoData> {
        public bool Equals(CameraDeviceInfoData other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Battery == other.Battery
                && BayerOffsetX == other.BayerOffsetX
                && BayerOffsetY == other.BayerOffsetY
                && BinX == other.BinX
                && BinY == other.BinY
                && ListEquals(BinningModes, other.BinningModes)
                && BitDepth == other.BitDepth
                && CameraState == other.CameraState
                && CanGetGain == other.CanGetGain
                && CanSetGain == other.CanSetGain
                && CanSetOffset == other.CanSetOffset
                && CanSetTemperature == other.CanSetTemperature
                && CanSetUsbLimit == other.CanSetUsbLimit
                && CanShowLiveView == other.CanShowLiveView
                && CanSubSample == other.CanSubSample
                && Connected == other.Connected
                && CoolerOn == other.CoolerOn
                && CoolerPower == other.CoolerPower
                && DefaultGain == other.DefaultGain
                && DefaultOffset == other.DefaultOffset
                && DewHeaterOn == other.DewHeaterOn
                && ElectronsPerAdu == other.ElectronsPerAdu
                && ExposureEndTime == other.ExposureEndTime
                && ExposureMax == other.ExposureMax
                && ExposureMin == other.ExposureMin
                && Gain == other.Gain
                && GainMax == other.GainMax
                && GainMin == other.GainMin
                && ListEquals(Gains, other.Gains)
                && HasBattery == other.HasBattery
                && HasDewHeater == other.HasDewHeater
                && HasShutter == other.HasShutter
                && IsExposing == other.IsExposing
                && IsSubSampleEnabled == other.IsSubSampleEnabled
                && LastDownloadTime == other.LastDownloadTime
                && LiveViewEnabled == other.LiveViewEnabled
                && NormalReadoutMode == other.NormalReadoutMode
                && Offset == other.Offset
                && OffsetMax == other.OffsetMax
                && OffsetMin == other.OffsetMin
                && PixelSize == other.PixelSize
                && ReadoutMode == other.ReadoutMode
                && ListEquals(ReadoutModes, other.ReadoutModes)
                && SensorType == other.SensorType
                && SnapReadoutMode == other.SnapReadoutMode
                && SubSampleHeight == other.SubSampleHeight
                && SubSampleWidth == other.SubSampleWidth
                && SubSampleX == other.SubSampleX
                && SubSampleY == other.SubSampleY
                && TemeratureSetPoint == other.TemeratureSetPoint
                && Temperature == other.Temperature
                && UsbLimit == other.UsbLimit
                && UsbLimitMax == other.UsbLimitMax
                && UsbLimitMin == other.UsbLimitMin
                && XSize == other.XSize
                && YSize == other.YSize;
        }

        public override bool Equals(object obj) => Equals(obj as CameraDeviceInfoData);

        public override int GetHashCode() {
            var hash = new HashCode();
            hash.Add(Battery);
            hash.Add(BayerOffsetX);
            hash.Add(BayerOffsetY);
            hash.Add(BinX);
            hash.Add(BinY);
            hash.Add(BinningModes?.Count);
            hash.Add(BitDepth);
            hash.Add(CameraState);
            hash.Add(CanGetGain);
            hash.Add(CanSetGain);
            hash.Add(CanSetOffset);
            hash.Add(CanSetTemperature);
            hash.Add(CanSetUsbLimit);
            hash.Add(CanShowLiveView);
            hash.Add(CanSubSample);
            hash.Add(Connected);
            hash.Add(CoolerOn);
            hash.Add(CoolerPower);
            hash.Add(DefaultGain);
            hash.Add(DefaultOffset);
            hash.Add(DewHeaterOn);
            hash.Add(ElectronsPerAdu);
            hash.Add(ExposureEndTime);
            hash.Add(ExposureMax);
            hash.Add(ExposureMin);
            hash.Add(Gain);
            hash.Add(GainMax);
            hash.Add(GainMin);
            hash.Add(Gains?.Count);
            hash.Add(HasBattery);
            hash.Add(HasDewHeater);
            hash.Add(HasShutter);
            hash.Add(IsExposing);
            hash.Add(IsSubSampleEnabled);
            hash.Add(LastDownloadTime);
            hash.Add(LiveViewEnabled);
            hash.Add(NormalReadoutMode);
            hash.Add(Offset);
            hash.Add(OffsetMax);
            hash.Add(OffsetMin);
            hash.Add(PixelSize);
            hash.Add(ReadoutMode);
            hash.Add(ReadoutModes?.Count);
            hash.Add(SensorType);
            hash.Add(SnapReadoutMode);
            hash.Add(SubSampleHeight);
            hash.Add(SubSampleWidth);
            hash.Add(SubSampleX);
            hash.Add(SubSampleY);
            hash.Add(TemeratureSetPoint);
            hash.Add(Temperature);
            hash.Add(UsbLimit);
            hash.Add(UsbLimitMax);
            hash.Add(UsbLimitMin);
            hash.Add(XSize);
            hash.Add(YSize);
            return hash.ToHashCode();
        }

        private static bool ListEquals<T>(List<T> a, List<T> b) {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.SequenceEqual(b);
        }
    }

    public partial class BinningMode : IEquatable<BinningMode> {
        public bool Equals(BinningMode other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj) => Equals(obj as BinningMode);

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}