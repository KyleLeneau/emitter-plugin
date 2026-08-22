import Foundation
import Logging

/// Decodes an incoming CloudEvent, then dispatches on its `type` to decode `data` into the
/// matching generated model from `EmitterModels.swift`.
///
/// This example only wires up a handful of representative event types (one per domain-ish
/// area: device connection, camera, mount, imaging, sequencing). The `asyncapi.yaml` spec
/// defines ~38 event types in total — add a `case` per additional `io.nina.*` type you want
/// strongly typed; anything else falls through to the `default` branch, which still decodes
/// and logs the raw JSON payload instead of failing.
enum WebhookHandler {
    static func handle(_ body: Data, logger: Logger) {
        // newJSONDecoder() comes from EmitterModels.swift and configures ISO-8601 date
        // decoding, matching every generated model's `Date` fields.
        let decoder = newJSONDecoder()

        let header: CloudEventHeader
        do {
            header = try decoder.decode(CloudEventHeader.self, from: body)
        } catch {
            logger.error("Failed to decode CloudEvent envelope: \(error)")
            return
        }

        do {
            switch header.type {
            case "io.nina.device.connection":
                log(try decoder.decode(CloudEvent<DeviceConnectionData>.self, from: body), logger: logger)
            case "io.nina.camera.device-info":
                log(try decoder.decode(CloudEvent<CameraDeviceInfoData>.self, from: body), logger: logger)
            case "io.nina.mount.device-info":
                log(try decoder.decode(CloudEvent<MountDeviceInfoData>.self, from: body), logger: logger)
            case "io.nina.image.saved":
                log(try decoder.decode(CloudEvent<ImageSavedData>.self, from: body), logger: logger)
            case "io.nina.sequence.start":
                log(try decoder.decode(CloudEvent<SequenceStartData>.self, from: body), logger: logger)
            case "io.nina.sequence.end":
                log(try decoder.decode(CloudEvent<SequenceEndData>.self, from: body), logger: logger)
            default:
                logger.info("Unmapped event type \"\(header.type)\" — logging raw data")
                log(try decoder.decode(CloudEvent<JSONValue>.self, from: body), logger: logger)
            }
        } catch {
            logger.error("Failed to decode data for type \"\(header.type)\": \(error)")
        }
    }

    private static func log<Payload>(_ event: CloudEvent<Payload>, logger: Logger) {
        logger.info(
            """
            CloudEvent received
              id:     \(event.id)
              source: \(event.source)
              type:   \(event.type)
              time:   \(event.time ?? "n/a")
              data:   \(String(describing: event.data))
            """
        )
    }
}
