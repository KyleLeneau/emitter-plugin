using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bortle.NINA.Emitter.EventSinks {

    /// <summary>
    /// JsonSerializerOptions shared by every sink's <c>JsonEventFormatter</c>. Built from the
    /// generated model converters so date/time payload fields keep their expected formatting,
    /// plus DefaultIgnoreCondition so schema-optional fields with no value (e.g. unsupported
    /// weather sensors) are omitted from the payload instead of serialized as "field": null.
    /// </summary>
    internal static class CloudEventJsonOptions {
        public static readonly JsonSerializerOptions Settings = new(Models.Converter.Settings) {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }
}