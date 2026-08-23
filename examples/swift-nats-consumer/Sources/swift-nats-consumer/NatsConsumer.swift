import Foundation
import Logging
import Nats

/// Connects to NATS, subscribes to `subject`, and feeds every incoming message to
/// `EventHandler`. Also mirrors connection lifecycle events into `Metrics` so `/status` can
/// report whether the consumer is actually receiving anything.
struct NatsConsumer {
    let url: URL
    let subject: String
    let username: String?
    let password: String?
    let requireTls: Bool
    let logger: Logger

    /// Runs forever (until cancelled). Once connected, nats.swift handles reconnects on its
    /// own (`NatsClientOptions.reconnectWait`/`maxReconnects`) and keeps the subscription
    /// alive across them — but the *initial* `connect()` throws outright on failure instead
    /// of retrying, so that part is retried here to keep the HTTP server up (and `/health`
    /// reporting live) while NATS is unreachable at startup.
    func run() async throws {
        var options = NatsClientOptions()
            .url(url)

        if let username, let password {
            options = options.usernameAndPassword(username, password)
        }
        if requireTls {
            options = options.requireTls()
        }

        let client = options.build()

        client.on([.connected, .disconnected, .suspended, .closed, .lameDuckMode, .error]) { event in
            Task {
                await Metrics.shared.recordConnectionState(event.kind())
            }
            switch event {
            case .connected:
                logger.info("NATS connected")
            case .disconnected:
                logger.warning("NATS disconnected — will retry")
            case .suspended:
                logger.warning("NATS connection suspended")
            case .closed:
                logger.warning("NATS connection closed")
            case .lameDuckMode:
                logger.warning("NATS server entering lame duck mode")
            case .error(let error):
                logger.error("NATS error: \(error)")
            }
        }

        await Metrics.shared.configure(subject: subject)

        while true {
            do {
                try await client.connect()
                break
            } catch {
                logger.warning("NATS connect to \"\(url)\" failed: \(error) — retrying in 2s")
                try await Task.sleep(for: .seconds(2))
            }
        }

        logger.info("subscribing to \"\(subject)\"")
        let subscription = try await client.subscribe(subject: subject)

        for try await message in subscription {
            guard let payload = message.payload else { continue }
            await EventHandler.handle(payload, subject: message.subject, logger: logger)
        }
    }
}
