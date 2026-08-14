using Bortle.NINA.Emitter.EventSinks;
using Bortle.NINA.Emitter.Models;
using CloudNative.CloudEvents;
using CloudNative.CloudEvents.SystemTextJson;
using System.Text;

namespace Bortle.NINA.Emitter.Tests.EventSinks
{

    public class CloudEventJsonOptionsTests
    {
        [Fact]
        public void EncodeStructuredModeMessage_OmitsNullOptionalFields()
        {
            var formatter = new JsonEventFormatter(CloudEventJsonOptions.Settings, default);
            var data = new WeatherDeviceInfoData
            {
                Connected = false,
                // Every other field is left null, mirroring a device that reports no sensors.
            };
            var evt = new CloudEvent
            {
                Type = "io.nina.weather.device-info",
                Source = new Uri("//nina/test-host", UriKind.RelativeOrAbsolute),
                Id = Guid.NewGuid().ToString(),
                Time = DateTimeOffset.UtcNow,
                DataContentType = "application/json",
                Data = data
            };

            var bytes = formatter.EncodeStructuredModeMessage(evt, out _);
            var json = Encoding.UTF8.GetString(bytes.ToArray());

            Assert.DoesNotContain("null", json);
            Assert.Contains("\"connected\":false", json);
        }

        [Fact]
        public void EncodeStructuredModeMessage_IncludesNonNullOptionalFields()
        {
            var formatter = new JsonEventFormatter(CloudEventJsonOptions.Settings, default);
            var data = new WeatherDeviceInfoData
            {
                Connected = true,
                Temperature = 12.5,
            };
            var evt = new CloudEvent
            {
                Type = "io.nina.weather.device-info",
                Source = new Uri("//nina/test-host", UriKind.RelativeOrAbsolute),
                Id = Guid.NewGuid().ToString(),
                Time = DateTimeOffset.UtcNow,
                DataContentType = "application/json",
                Data = data
            };

            var bytes = formatter.EncodeStructuredModeMessage(evt, out _);
            var json = Encoding.UTF8.GetString(bytes.ToArray());

            Assert.Contains("\"temperature\":12.5", json);
            Assert.DoesNotContain("\"humidity\"", json);
        }
    }
}
