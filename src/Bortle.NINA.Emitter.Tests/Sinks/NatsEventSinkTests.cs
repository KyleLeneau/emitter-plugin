using Bortle.NINA.Emitter.EventSinks;
using CloudNative.CloudEvents;

namespace Bortle.NINA.Emitter.Tests.EventSinks
{

    public class NatsEventSinkTests
    {
        [Theory]
        [InlineData("io.nina.weather.device-info", "//nina/my-host", "nina", "nina.my-host.weather.device-info")]
        [InlineData("io.nina.camera.connected", "//nina/my-host", "nina", "nina.my-host.camera.connected")]
        [InlineData("io.nina.telescope.slew-complete", "//nina/other-host", "custom-prefix", "custom-prefix.other-host.telescope.slew-complete")]
        public void BuildSubject_UsesConfiguredPrefixInstanceIdAndDomainEventType(
            string type, string source, string subjectPrefix, string expectedSubject)
        {
            var evt = new CloudEvent
            {
                Type = type,
                Source = new Uri(source, UriKind.RelativeOrAbsolute),
                Id = Guid.NewGuid().ToString(),
                Time = DateTimeOffset.UtcNow,
                DataContentType = "application/json"
            };

            var subject = NatsEventSink.BuildSubject(subjectPrefix, evt);

            Assert.Equal(expectedSubject, subject);
        }
    }
}
