namespace Bortle.NINA.Emitter.Utils {

    public static class DoubleExtensions {
        public static double? Optional(this double value) => double.IsNaN(value) ? null : value;
    }
}