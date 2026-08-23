# Swift NATS Consumer

A standalone consumer that subscribes directly to the NATS subject Emitter's `nats` sink
publishes to (rather than receiving pushed webhooks), decodes each CloudEvent using the
generated Swift models, and serves `/status` and `/health` over HTTP so the consumer itself
can be monitored. Built with [nats.swift](https://github.com/nats-io/nats.swift) for the NATS
connection, [Hummingbird 2](https://hummingbird-project.github.io/hummingbird-docs/) for the
HTTP server, and [swift-argument-parser](https://github.com/apple/swift-argument-parser) for
the CLI.

## Run it

Point the Emitter plugin's NATS sink at a NATS server (see `NatsSinkOptions` — default
`nats://localhost:4222`, subject prefix `nina`), then:

```sh
swift run swift-nats-consumer
```

By default this connects to `nats://localhost:4222`, subscribes to `nina.>` (every instance,
every event type), and serves `/status`/`/health` on `0.0.0.0:8080`. All of that is
configurable:

```sh
swift run swift-nats-consumer \
  --nats-url nats://nats.example.com:4222 \
  --subject nina.my-observatory.> \
  --nats-username observer --nats-password secret \
  --tls \
  --host 0.0.0.0 --port 8080 \
  --log-level info
```

Run `swift run swift-nats-consumer --help` for the full flag reference. Requires Swift 5.10+
(Xcode 16+ on macOS, or the Swift toolchain on Linux).

## Endpoints

- **`GET /health`** — plain liveness check. Returns `200 ok` as long as the process is up,
  independent of whether NATS is currently reachable — useful as a container/k8s liveness
  probe that shouldn't restart the pod just because NATS is temporarily down.
- **`GET /status`** — JSON snapshot of what the consumer has actually seen:

  ```json
  {
    "uptimeSeconds": 143.2,
    "subject": "nina.>",
    "connectionState": "connected",
    "totalReceived": 42,
    "decodeFailures": 0,
    "countsByType": { "io.nina.camera.device-info": 30, "io.nina.image.saved": 12 },
    "lastEventType": "io.nina.image.saved",
    "lastEventAt": "2026-08-24T19:06:56Z"
  }
  ```

  `connectionState` mirrors nats.swift's connection events (`connected`, `disconnected`,
  `suspended`, `closed`, `lameDuckMode`, `error`).

## How it decodes events

Same envelope and generated models as the [`swift-webhook`](../swift-webhook/README.md)
example — see that README for the full explanation of the CloudEvents structured-mode shape.
The difference here is the transport and the reconnect story:

- **`NatsConsumer.swift`** — builds a `NatsClientOptions` from the CLI flags, registers a
  listener for connection lifecycle events (feeding `Metrics`), then connects and subscribes.
  The *initial* connect is retried in a loop with a fixed 2s backoff (nats.swift's own
  `connect()` throws outright on the first failure instead of retrying) — this keeps the HTTP
  server up and `/health` reporting live even if NATS isn't reachable yet at startup. Once
  connected, nats.swift handles reconnects and keeps the subscription alive on its own.
- **`EventHandler.swift`** — the same header-then-dispatch decoding as `WebhookHandler` in
  `swift-webhook`, fed from a NATS message payload instead of an HTTP request body. Same
  handful of representative `io.nina.*` types are wired up; add a `case` per additional type,
  same as that example.
- **`Metrics.swift`** — an `actor` tracking uptime, connection state, and per-type event
  counts, read by the `/status` route. `EventHandler` records a decode failure separately from
  a successful decode so `/status` can tell the two apart.
- **`Server.swift`** — the Hummingbird `Router` for `/health` and `/status`.
- **`Entrypoint.swift`** — the `swift-argument-parser` CLI entry point; runs the HTTP server
  and the NATS consume loop concurrently in a task group, tearing one down if the other fails.
- **`CloudEvent.swift`** / **`EmitterModels.swift`** — vendored the same way as in
  `swift-webhook`: if you change a schema, re-run `./schema/build.sh generate` from the repo
  root and copy the regenerated `EmitterModels.swift` over this one.
