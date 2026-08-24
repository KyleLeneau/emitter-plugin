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

## Running it in Docker

The `Dockerfile` here is adapted from
[Hummingbird's own example template](https://github.com/hummingbird-project/hummingbird-examples/blob/main/hello/Dockerfile):
a `swift:6.3-noble` build stage produces a statically-linked release binary, staged into a
slim `ubuntu:noble` runtime image that runs as an unprivileged user. This is what "hosted on a
Linux server" looks like for this example — build once, ship the image, run it next to (or
pointed at) your NATS server.

```sh
docker build -t swift-nats-consumer .
```

Built and verified against real containers (`nats:latest` + the built image on a shared Docker
network, decoding a published event end to end) while writing this — the ~350MB build took two
real fixes along the way, both to nats.swift's own Linux support rather than anything specific
to this example, and both are patched (with comments explaining why) in the `Dockerfile`
itself rather than left for you to hit:

- `nkeys.swift` (a nats.swift dependency) needs libsodium 1.0.22+ for symbols Ubuntu's `apt`
  package (stuck at 1.0.18) doesn't have — the Dockerfile builds libsodium from source instead.
- nats.swift v0.4.0 references `URLSession`/`CharacterSet` APIs on Linux without importing
  `FoundationNetworking` (fine on Darwin, where `Foundation` re-exports them; not fine on
  Linux) — the Dockerfile patches the vendored source in the build stage and installs
  `libcurl4` at runtime for it. Safe to drop both once nats.swift ships a fix upstream.

`--nats-url` needs to resolve from *inside* the container, so `nats://localhost:4222` (the
CLI's own default) is almost never right there — that's the container talking to itself.
Point it at wherever NATS actually runs instead:

```sh
# NATS running as another container on a shared user-defined network
docker network create nina
docker run --rm -d --name nats --network nina nats:latest
docker run --rm -p 8080:8080 --network nina \
  swift-nats-consumer --nats-url nats://nats:4222

# NATS running on the Docker host itself (e.g. alongside NINA on the same machine)
docker run --rm -p 8080:8080 \
  swift-nats-consumer --nats-url nats://host.docker.internal:4222
```

The image's default `CMD` already assumes the first case (`--nats-url nats://nats:4222`) —
override it with your own flags as shown above for anything else. `docker-compose.yml` is the
more usual way to wire this up long-term:

```yaml
services:
  nats:
    image: nats:latest
    ports: ["4222:4222"]
  consumer:
    build: .
    ports: ["8080:8080"]
    command: ["--nats-url", "nats://nats:4222", "--subject", "nina.>"]
    depends_on: [nats]
```

Then `curl http://localhost:8080/health` and `curl http://localhost:8080/status` from the
host to check on it.

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
