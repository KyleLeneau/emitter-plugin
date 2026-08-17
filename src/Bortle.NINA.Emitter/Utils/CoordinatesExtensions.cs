using Bortle.NINA.Emitter.Models;

namespace Bortle.NINA.Emitter.Utils {

    public class AstrometryCoordinates {

        public static Epoch? ToEpoch(global::NINA.Astrometry.Epoch epoch) {
            return epoch switch {
                global::NINA.Astrometry.Epoch.JNOW => Epoch.Jnow,
                global::NINA.Astrometry.Epoch.B1950 => Epoch.B1950,
                global::NINA.Astrometry.Epoch.J2000 => Epoch.J2000,
                global::NINA.Astrometry.Epoch.J2050 => Epoch.J2050,
                _ => null
            };
        }

        public static Coordinates ToCoordinates(global::NINA.Astrometry.Coordinates coordinates) {
            if (coordinates == null) return null;

            return new Coordinates {
                RaDegrees = coordinates.RADegrees,
                DecDegrees = coordinates.Dec,
                Epoch = ToEpoch(coordinates.Epoch),
            };
        }
    }

}