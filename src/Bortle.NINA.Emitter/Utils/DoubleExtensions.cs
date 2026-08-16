namespace Bortle.NINA.Emitter.Utils {

    public static class DoubleExtensions {
        public static double? Optional(this double value) => double.IsNaN(value) ? null : value;
    }

    // public static class LongExtensions {
    //     public static long? Optional(this long value) => value == -1 ? null : value;
    // }
}