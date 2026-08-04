using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.EventSinks;
using Bortle.NINA.Emitter.Tests.Fakes;

namespace Bortle.NINA.Emitter.Tests.Events {

    public class EventEmitterServiceTests {
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(5);

        private record SamplePayload(string Value);

        [Fact]
        public async Task Enqueue_BuildsCloudEventWithExpectedEnvelope() {
            var received = new TaskCompletionSource<CloudNative.CloudEvents.CloudEvent>();
            var sink = new FakeEventSink(onSend: (evt, ct) => {
                received.TrySetResult(evt);
                return Task.CompletedTask;
            });

            await using var emitter = new EventEmitterService(new[] { sink }, instanceId: "test-host");
            var before = DateTimeOffset.UtcNow;

            emitter.Enqueue("weather", "device-info", new SamplePayload("hello"));

            var evt = await WaitAsync(received.Task);

            Assert.Equal("io.nina.weather.device-info", evt.Type);
            Assert.Equal("//nina/test-host", evt.Source!.OriginalString);
            Assert.False(string.IsNullOrWhiteSpace(evt.Id));
            Assert.True(Guid.TryParse(evt.Id, out _));
            Assert.NotNull(evt.Time);
            Assert.InRange(evt.Time!.Value, before.AddSeconds(-1), DateTimeOffset.UtcNow.AddSeconds(1));
            Assert.Equal("application/json", evt.DataContentType);
            Assert.Equal(new SamplePayload("hello"), evt.Data);
        }

        [Fact]
        public async Task Enqueue_FansOutToAllRegisteredSinks() {
            var sinkA = new FakeEventSink("a");
            var sinkB = new FakeEventSink("b");

            await using var emitter = new EventEmitterService(new IEventSink[] { sinkA, sinkB }, instanceId: "test-host");

            emitter.Enqueue("weather", "device-info", new SamplePayload("hello"));

            await WaitUntil(() => sinkA.Received.Count == 1 && sinkB.Received.Count == 1);

            Assert.Single(sinkA.Received);
            Assert.Single(sinkB.Received);
        }

        [Fact]
        public async Task Enqueue_WhenOneSinkThrows_OtherSinkStillReceivesEvent() {
            var faultySink = new FakeEventSink("faulty", onSend: (_, _) => throw new InvalidOperationException("boom"));
            var healthySink = new FakeEventSink("healthy");

            await using var emitter = new EventEmitterService(new IEventSink[] { faultySink, healthySink }, instanceId: "test-host");

            emitter.Enqueue("weather", "device-info", new SamplePayload("hello"));

            await WaitUntil(() => healthySink.Received.Count == 1);

            Assert.Single(healthySink.Received);
        }

        [Fact]
        public async Task Enqueue_WhenQueueIsFull_DropsOldestPendingEvent() {
            var gate = new TaskCompletionSource();
            var firstItemProcessing = new TaskCompletionSource();

            var sink = new FakeEventSink(onSend: async (evt, ct) => {
                if (((SamplePayload)evt.Data!).Value == "item-0") {
                    firstItemProcessing.TrySetResult();
                    await gate.Task;
                }
            });

            // Capacity 1: while "item-0" is being processed (blocked on the gate), the channel
            // can only hold one more pending item, so writing item-1/2/3 must drop the older
            // ones and keep only the newest ("item-3").
            await using var emitter = new EventEmitterService(new[] { sink }, instanceId: "test-host", queueCapacity: 1);

            emitter.Enqueue("weather", "device-info", new SamplePayload("item-0"));
            await WaitAsync(firstItemProcessing.Task);

            emitter.Enqueue("weather", "device-info", new SamplePayload("item-1"));
            emitter.Enqueue("weather", "device-info", new SamplePayload("item-2"));
            emitter.Enqueue("weather", "device-info", new SamplePayload("item-3"));

            gate.TrySetResult();

            await WaitUntil(() => sink.Received.Count == 2);

            var values = sink.Received.Select(e => ((SamplePayload)e.Data!).Value).ToList();
            Assert.Equal(new[] { "item-0", "item-3" }, values);
        }

        private static async Task<T> WaitAsync<T>(Task<T> task) {
            var completed = await Task.WhenAny(task, Task.Delay(Timeout));
            Assert.Same(task, completed);
            return await task;
        }

        private static async Task WaitAsync(Task task) {
            var completed = await Task.WhenAny(task, Task.Delay(Timeout));
            Assert.Same(task, completed);
            await task;
        }

        private static async Task WaitUntil(Func<bool> predicate) {
            var deadline = DateTime.UtcNow + Timeout;
            while (!predicate()) {
                if (DateTime.UtcNow > deadline) {
                    Assert.Fail("Condition was not met within the timeout.");
                }
                await Task.Delay(10);
            }
        }
    }
}
