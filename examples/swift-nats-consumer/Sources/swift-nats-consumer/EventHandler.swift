import Foundation
import Logging

/// Decodes an incoming CloudEvent published by Emitter's NATS sink, then dispatches on its
/// `type` to decode `data` into the matching generated model from `EmitterModels.swift`.
///
/// This mirrors `WebhookHandler` in the `swift-webhook` example — same envelope, same
/// generated models, just fed from a NATS message payload instead of an HTTP request body.
/// Only a handful of representative event types are wired up; add a `case` per additional
/// `io.nina.*` type you want strongly typed. Anything else falls through to the `default`
/// branch, which still decodes and logs the raw JSON payload instead of failing.
enum EventHandler {
    static func handle(_ payload: Data, subject: String, logger: Logger) async {
        // newJSONDecoder() comes from EmitterModels.swift and configures ISO-8601 date
        // decoding, matching every generated model's `Date` fields.
        let decoder = newJSONDecoder()

        let header: CloudEventHeader
        do {
            header = try decoder.decode(CloudEventHeader.self, from: payload)
        } catch {
            logger.error("Failed to decode CloudEvent envelope on subject \"\(subject)\": \(error)")
            await Metrics.shared.recordDecodeFailure()
            return
        }

        do {
            switch header.type {
            case "io.nina.device.connection":
                log(try decoder.decode(CloudEvent<DeviceConnectionData>.self, from: payload), subject: subject, logger: logger)
            case "io.nina.camera.device-info":
                log(try decoder.decode(CloudEvent<CameraDeviceInfoData>.self, from: payload), subject: subject, logger: logger)
            case "io.nina.mount.device-info":
                log(try decoder.decode(CloudEvent<MountDeviceInfoData>.self, from: payload), subject: subject, logger: logger)
            case "io.nina.image.saved":
                log(try decoder.decode(CloudEvent<ImageSavedData>.self, from: payload), subject: subject, logger: logger)
            case "io.nina.sequence.start":
                log(try decoder.decode(CloudEvent<SequenceStartData>.self, from: payload), subject: subject, logger: logger)
            case "io.nina.sequence.end":
                log(try decoder.decode(CloudEvent<SequenceEndData>.self, from: payload), subject: subject, logger: logger)
            default:
                logger.info("Unmapped event type \"\(header.type)\" — logging raw data")
                log(try decoder.decode(CloudEvent<JSONValue>.self, from: payload), subject: subject, logger: logger)
            }
            await Metrics.shared.recordEvent(type: header.type)
        } catch {
            logger.error("Failed to decode data for type \"\(header.type)\": \(error)")
            await Metrics.shared.recordDecodeFailure()
        }
    }

    private static func log<Payload>(_ event: CloudEvent<Payload>, subject: String, logger: Logger) {
        logger.info(
            """
            CloudEvent received
              subject: \(subject)
              id:      \(event.id)
              source:  \(event.source)
              type:    \(event.type)
              time:    \(event.time ?? "n/a")
              data:    \(String(describing: event.data))
            """
        )
    }
}
