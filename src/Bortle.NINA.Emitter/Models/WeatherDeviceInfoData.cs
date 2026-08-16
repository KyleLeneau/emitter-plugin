using System;

namespace Bortle.NINA.Emitter.Models {

    // Value-equality extension for the quicktype-generated WeatherDeviceInfoData (Generated/EmitterModels.cs).
    // quicktype's C# target only emits plain classes, so this is a hand-written partial rather than a
    // generated record. Used by WeatherHandler to skip emitting when NINA's internal polling produces no
    // actual change.

    public partial class WeatherDeviceInfoData : IEquatable<WeatherDeviceInfoData> {
        public bool Equals(WeatherDeviceInfoData other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Connected == other.Connected
                && CloudCover == other.CloudCover
                && DewPoint == other.DewPoint
                && Humidity == other.Humidity
                && Pressure == other.Pressure
                && RainRate == other.RainRate
                && SkyQuality == other.SkyQuality
                && SkyTemperature == other.SkyTemperature
                && StarFwhm == other.StarFwhm
                && Temperature == other.Temperature
                && WindDirection == other.WindDirection
                && WindGust == other.WindGust
                && WindSpeed == other.WindSpeed;
        }

        public override bool Equals(object obj) => Equals(obj as WeatherDeviceInfoData);

        public override int GetHashCode() {
            var hash = new HashCode();
            hash.Add(Connected);
            hash.Add(CloudCover);
            hash.Add(DewPoint);
            hash.Add(Humidity);
            hash.Add(Pressure);
            hash.Add(RainRate);
            hash.Add(SkyQuality);
            hash.Add(SkyTemperature);
            hash.Add(StarFwhm);
            hash.Add(Temperature);
            hash.Add(WindDirection);
            hash.Add(WindGust);
            hash.Add(WindSpeed);
            return hash.ToHashCode();
        }
    }
}