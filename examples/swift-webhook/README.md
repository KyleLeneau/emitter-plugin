# Swift Webhook

A CloudEvents-aware webhook receiver using [Hummingbird 2](https://hummingbird-project.github.io/hummingbird-docs/)
as the HTTP server. Unlike the other minimal examples, this one actually decodes the
CloudEvent envelope and its typed `data` payload using the generated Swift models, and logs
the result.

## Run it

```sh
swift run
```

Then configure the Emitter plugin to use `http://localhost:8000/webhook` as the URL (you
might need to change the host for your network). Requires Swift 5.10+ (Xcode 16+ on macOS,
or the Swift toolchain on Linux — this package doesn't use any macOS-only APIs).

## How it decodes events

Every NATS message Emitter publishes is a [CloudEvents 1.0](https://cloudevents.io/)
structured-mode JSON object — see `schema/asyncapi.yaml` for the field conventions. `data`'s
shape depends on the CloudEvent `type`, e.g. `io.nina.device.connection` → `DeviceConnectionData`.

- **`CloudEvent.swift`** — a generic `CloudEvent<Payload>` matching `schema/schemas/cloudevents.json`,
  plus `CloudEventHeader` (just enough fields to read `type`) and `JSONValue`, a loosely-typed
  fallback for payloads with no mapped model.
- **`EmitterModels.swift`** — vendored as-is from `schema/generated/swift/EmitterModels.swift`.
  If you change a schema, re-run `./schema/build.sh generate` from the repo root and copy the
  regenerated file over this one.
- **`WebhookHandler.swift`** — decodes `CloudEventHeader` first to read `type`, then switches
  on it to decode `data` into the matching generated model. Only a handful of representative
  types are wired up (device connection, camera, mount, image saved, sequence start/end); add
  a `case` per additional `io.nina.*` type you want strongly typed. Anything unmapped falls
  through to the `default` branch, which still decodes and logs the raw JSON instead of failing.

Every request logs the decoded envelope and its typed (or raw, for unmapped types) `data`:

```
CloudEvent received
  id:     abc-123
  source: //nina/my-host
  type:   io.nina.device.connection
  time:   2026-08-24T12:00:00Z
  data:   Optional(DeviceConnectionData(connected: true, ...))
```
