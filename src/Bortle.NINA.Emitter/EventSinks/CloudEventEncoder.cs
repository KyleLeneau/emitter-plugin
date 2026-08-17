using CloudNative.CloudEvents;
using CloudNative.CloudEvents.SystemTextJson;
using System;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json;

namespace Bortle.NINA.Emitter.EventSinks {

    /// <summary>
    /// Encodes a <see cref="CloudEvent"/> to CloudEvents structured-mode JSON bytes, shared by
    /// every sink so encoding and its failure handling live in one place instead of being
    /// duplicated per sink. On a serialization failure, the event's Data payload is re-serialized
    /// property-by-property so the thrown exception names the specific property that failed
    /// instead of surfacing a bare, hard-to-place System.Text.Json exception.
    /// </summary>
    internal static class CloudEventEncoder {
        private static readonly JsonEventFormatter Formatter = new JsonEventFormatter(CloudEventJsonOptions.Settings, default);

        public static ReadOnlyMemory<byte> EncodeStructuredModeMessage(CloudEvent evt, out ContentType contentType) {
            try {
                return Formatter.EncodeStructuredModeMessage(evt, out contentType);
            } catch (Exception ex) {
                throw new CloudEventEncodingException(evt, DescribeDataSerializationFailure(evt.Data), ex);
            }
        }

        /// <summary>
        /// Re-serializes each public property of the event's Data payload individually to find
        /// which one(s) actually fail, since the exception from the combined serialize above
        /// doesn't say which field is at fault.
        /// </summary>
        private static string DescribeDataSerializationFailure(object data) {
            if (data == null) {
                return "Data is null";
            }

            var failures = data.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(prop => {
                    try {
                        var value = prop.GetValue(data);
                        JsonSerializer.Serialize(value, prop.PropertyType, CloudEventJsonOptions.Settings);
                        return null;
                    } catch (Exception ex) {
                        return $"{prop.Name} ({prop.PropertyType.Name}): {ex.Message}";
                    }
                })
                .Where(failure => failure != null)
                .ToList();

            return failures.Count > 0
                ? $"property serialization failed for: {string.Join("; ", failures)}"
                : $"could not isolate failing property on {data.GetType().Name}";
        }
    }

    /// <summary>
    /// Thrown by <see cref="CloudEventEncoder"/> when a <see cref="CloudEvent"/> fails to encode,
    /// with a message that names the CloudEvent and, where it could be isolated, the specific
    /// Data property responsible.
    /// </summary>
    public class CloudEventEncodingException : Exception {
        public CloudEventEncodingException(CloudEvent evt, string detail, Exception inner)
            : base($"Failed to encode CloudEvent '{evt.Type}' (id={evt.Id}): {detail}", inner) {
        }
    }
}