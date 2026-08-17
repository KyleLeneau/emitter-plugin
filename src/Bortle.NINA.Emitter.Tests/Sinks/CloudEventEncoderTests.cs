using Bortle.NINA.Emitter.EventSinks;
using CloudNative.CloudEvents;

namespace Bortle.NINA.Emitter.Tests.EventSinks
{

    public class CloudEventEncoderTests
    {
        private class GoodData
        {
            public string Name { get; set; } = string.Empty;
        }

        // System.Text.Json has no built-in converter for a delegate, so serializing this
        // property throws - standing in for a real "field type serializer error" case.
        private class BadData
        {
            public string Name { get; set; } = "ok";
            public Action Callback { get; set; } = () => { };
        }

        [Fact]
        public void EncodeStructuredModeMessage_ValidData_ReturnsBytes()
        {
            var evt = new CloudEvent
            {
                Type = "io.nina.weather.device-info",
                Source = new Uri("//nina/test-host", UriKind.RelativeOrAbsolute),
                Id = Guid.NewGuid().ToString(),
                Time = DateTimeOffset.UtcNow,
                DataContentType = "application/json",
                Data = new GoodData { Name = "test" }
            };

            var bytes = CloudEventEncoder.EncodeStructuredModeMessage(evt, out var contentType);

            Assert.True(bytes.Length > 0);
            Assert.Equal("application/cloudevents+json", contentType.MediaType);
        }

        [Fact]
        public void EncodeStructuredModeMessage_UnserializableProperty_ThrowsWithPropertyName()
        {
            var evt = new CloudEvent
            {
                Type = "io.nina.weather.device-info",
                Source = new Uri("//nina/test-host", UriKind.RelativeOrAbsolute),
                Id = Guid.NewGuid().ToString(),
                Time = DateTimeOffset.UtcNow,
                DataContentType = "application/json",
                Data = new BadData()
            };

            var ex = Assert.Throws<CloudEventEncodingException>(
                () => CloudEventEncoder.EncodeStructuredModeMessage(evt, out _));

            Assert.Contains("io.nina.weather.device-info", ex.Message);
            Assert.Contains(nameof(BadData.Callback), ex.Message);
            Assert.NotNull(ex.InnerException);
        }
    }
}
