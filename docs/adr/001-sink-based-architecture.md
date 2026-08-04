# 000 — ADR Name with auto incrementing index counter

## Context

In order to support seperation of concerns and allow for the introduction of new backends the core of this plugin and architecture should be a sink based pattern where each backend (nats, & webhook initially) should be implemented as a Sink that manages the implementation of pushing an event to its backend and maintaining the connection to the backend with an SDK or similar.

Various Handlers for NINA event domains and types should be implemented to subscribe to the changes in NINA and create the Data types for the cloud event. Each of these handlers should not directly interact with the Sink but instead should `Enqueue<T>(string domain, string eventType, T data)` or similar onto a Manager or Service that is holding 1 or more Sinks (1 to start with).

The Manager/Service should be responsible for creating the CloudEvent wrapper and queueing the event for sending to the Sink in an async/background way.

Sinks will need to have Options to configure them and will come from a UI and Settings so if a Sink needs to change then only the Manager/Service should have to know about that change. The connection state should be available from the Sink to know if it's active/ready or not and will bubble up to a UI.

---

## Plan

### Scope

This pass delivers the full stack: core plumbing (Handler → Manager → Sink), both initial
sinks (NATS, Webhook), and one Handler (Weather) wired end-to-end as the reference
implementation. Camera/Telescope/Imaging handlers follow the identical pattern in later
work. Sink configuration is code-first (POCO options persisted via the existing
`IPluginOptionsAccessor`); the WPF Options UI (`Options.xaml`) stays a placeholder for now.

Conventions below (subject pattern, CloudEvent `type`/`source`, per-domain event names)
are already fixed by `schema/asyncapi.yaml` and the generated models in
`Generated/EmitterModels.cs` — this plan implements against those, not invents new ones.

### Components

1. **`Events/IEventEmitter.cs` + `EventEmitterService.cs`** (the Manager/Service)
   - `Enqueue<T>(string domain, string eventType, T data)` builds a `CloudEvent`:
     - `type`: `io.nina.{domain}.{eventType}`
     - `source`: `//nina/{instanceId}` (instanceId = lowercased machine hostname, resolved once at startup)
     - `id`: new GUID, `time`: UTC now, `data`: payload, `datacontenttype: application/json`
   - Backed by a bounded `System.Threading.Channels.Channel<CloudEvent>` with
     `BoundedChannelFullMode.DropOldest` (agreed backpressure behavior).
   - A single background consumer loop dequeues and fans out to every registered, enabled
     `IEventSink` via `Task.WhenAll`, with each `SendAsync` wrapped in try/catch so one sink's
     failure doesn't block or drop the event for the others.
   - Holds `IReadOnlyList<IEventSink>` and exposes per-sink `ConnectionState` for a future UI
     (per ROADMAP: "dockable with current status of queue or connections").

2. **`Sinks/IEventSink.cs`** (the Sink contract)
   - `string Name`, `ConnectionState State`, `event EventHandler<ConnectionState> StateChanged`
   - `Task ConnectAsync(CancellationToken)`, `Task DisconnectAsync(CancellationToken)`
   - `Task SendAsync(CloudEvent evt, CancellationToken)`
   - `ConnectionState`: `Disconnected`, `Connecting`, `Connected`, `Faulted`

3. **`Sinks/NatsSink.cs` + `NatsSinkOptions.cs`**
   - Wraps NATS.Net's `NatsClient`. Subject = `nina.{instanceId}.{domain}.{eventType}`,
     derived from the CloudEvent `type` (strip `io.nina.` prefix) plus the configurable
     `SubjectPrefix` (default `nina`, per ROADMAP: "allow nats subject prefix to be
     specified/changed").
   - Publishes structured-mode CloudEvents JSON via `CloudNative.CloudEvents.SystemTextJson.JsonEventFormatter`.
   - Options: `Url`, `SubjectPrefix`, `Enabled`. Connection state tracked from NATS.Net's
     connection state callbacks.

4. **`Sinks/WebhookSink.cs` + `WebhookSinkOptions.cs`**
   - Wraps `HttpClient`, POSTs the CloudEvent in structured mode
     (`Content-Type: application/cloudevents+json`) to a configured URL.
   - Options: `Url`, `Enabled`, optional bearer token header, timeout.
   - No per-event retry in this pass — a failed POST logs and marks the sink `Faulted`;
     the bounded queue's drop-oldest policy is the only backpressure/loss mechanism for now.

5. **`Handlers/WeatherHandler.cs`** (reference Handler)
   - Implements NINA's `IWeatherDataConsumer`, registers itself via
     `IWeatherDataMediator.RegisterConsumer(this)`.
   - `UpdateWeatherDataValues(WeatherDataInfo info, ...)` maps the NINA type to the
     generated `WeatherData` model and calls `emitter.Enqueue("weather", "device-info", data)`
     — the single event this domain has, per `schema/asyncapi.yaml`.
   - Camera/Telescope/Imaging handlers will follow the same consumer-registration shape
     against `ICameraMediator`/`ITelescopeMediator`/`IImageSaveMediator` in follow-up work.

6. **`Configuration/EmitterOptions.cs`** (named `Configuration/`, not `Options/`, to avoid colliding
   with the existing `Bortle.NINA.Emitter.Options` WPF resource-dictionary class)
   - Root object holding `NatsSinkOptions` and `WebhookSinkOptions`, persisted through the
     existing `pluginSettings` (`IPluginOptionsAccessor`) profile-dependent settings — no
     new settings UI this pass.

7. **`EmitterPlugin.cs` wiring**
   - Constructor: add `IWeatherDataMediator` to the `[ImportingConstructor]`, load
     `EmitterOptions`, construct `EventEmitterService` with the configured sinks, start it,
     construct `WeatherHandler` and register it as a consumer.
   - `Teardown()`: unregister the consumer, stop/dispose `EventEmitterService`
     (cancel the background loop), disconnect sinks.

8. **Tests** — no test project exists yet even though CI already runs `dotnet test`;
   add `src/Bortle.NINA.Emitter.Tests/` (xUnit) and reference it from `EmitterPlugin.slnx`.
   - `EventEmitterService`: CloudEvent envelope correctness (type/source/id/time), bounded
     queue drop-oldest behavior, one sink throwing doesn't affect others — tested against a
     fake `IEventSink`, no real network.
   - `NatsSink`/`WebhookSink`: subject-string formatting and serialized JSON shape, not
     live connections.
   - `WeatherHandler`: `WeatherDataInfo` → `WeatherData` mapping.

### Suggested file layout

```
src/Bortle.NINA.Emitter/
  Events/
    IEventEmitter.cs
    EventEmitterService.cs
    ConnectionState.cs
  Sinks/
    IEventSink.cs
    NatsSink.cs
    NatsSinkOptions.cs
    WebhookSink.cs
    WebhookSinkOptions.cs
  Handlers/
    WeatherHandler.cs
  Configuration/
    EmitterOptions.cs
src/Bortle.NINA.Emitter.Tests/
  Events/EventEmitterServiceTests.cs
  Sinks/NatsSinkTests.cs
  Sinks/WebhookSinkTests.cs
  Handlers/WeatherHandlerTests.cs
```

### Suggested sequencing

1. `IEventSink`, `ConnectionState`, `IEventEmitter`/`EventEmitterService` + tests (fake sink only).
2. `NatsSink` + options + tests.
3. `WebhookSink` + options + tests.
4. `WeatherHandler` wired into `EmitterPlugin.cs` end-to-end.
5. Update `ROADMAP.md` checkboxes for the items this covers.

### Explicitly deferred

- WPF Options UI for per-sink settings (code-first options only this pass).
- Camera/Telescope/Imaging handlers (same pattern as Weather, follow-up work).
- Kafka sink (no client package added yet).
- Per-event retry/dead-lettering (relying on the drop-oldest bounded queue instead).
