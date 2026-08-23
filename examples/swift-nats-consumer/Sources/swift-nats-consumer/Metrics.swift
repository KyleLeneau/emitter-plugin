import Foundation
import Hummingbird
import Nats

/// In-memory counters and connection state, updated by `NatsConsumer` and `EventHandler` as
/// events arrive, and read by the `/status` route. An `actor` because both the NATS event
/// loop and the HTTP server read/write it concurrently.
actor Metrics {
    static let shared = Metrics()

    private let startedAt = Date()
    private(set) var connectionState: NatsEventKind = .disconnected
    private(set) var subject: String = ""
    private(set) var totalReceived: Int = 0
    private(set) var decodeFailures: Int = 0
    private(set) var countsByType: [String: Int] = [:]
    private(set) var lastEventType: String?
    private(set) var lastEventAt: Date?

    private init() {}

    func configure(subject: String) {
        self.subject = subject
    }

    func recordConnectionState(_ state: NatsEventKind) {
        self.connectionState = state
    }

    func recordEvent(type: String) {
        self.totalReceived += 1
        self.countsByType[type, default: 0] += 1
        self.lastEventType = type
        self.lastEventAt = Date()
    }

    func recordDecodeFailure() {
        self.decodeFailures += 1
    }

    /// A snapshot suitable for JSON encoding on the `/status` route.
    func snapshot() -> StatusSnapshot {
        StatusSnapshot(
            uptimeSeconds: Date().timeIntervalSince(startedAt),
            subject: subject,
            connectionState: connectionState.rawValue,
            totalReceived: totalReceived,
            decodeFailures: decodeFailures,
            countsByType: countsByType,
            lastEventType: lastEventType,
            lastEventAt: lastEventAt
        )
    }
}

/// JSON body returned by `GET /status`. `ResponseCodable` (Hummingbird's Encodable +
/// ResponseGenerator) lets a route handler return this directly.
struct StatusSnapshot: ResponseCodable {
    let uptimeSeconds: TimeInterval
    let subject: String
    let connectionState: String
    let totalReceived: Int
    let decodeFailures: Int
    let countsByType: [String: Int]
    let lastEventType: String?
    let lastEventAt: Date?
}
