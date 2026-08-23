import ArgumentParser
import Foundation
import Logging

@main
struct Run: AsyncParsableCommand {
    static let configuration = CommandConfiguration(
        commandName: "swift-nats-consumer",
        abstract: "Subscribes to Emitter's NATS events and serves /status and /health over HTTP."
    )

    @Option(name: .customLong("nats-url"), help: "NATS server URL.")
    var natsUrl: String = "nats://localhost:4222"

    @Option(name: .customLong("subject"), help: "NATS subject to subscribe to.")
    var subject: String = "nina.>"

    @Option(name: .customLong("nats-username"), help: "Username, if the NATS server requires auth.")
    var natsUsername: String?

    @Option(name: .customLong("nats-password"), help: "Password, if the NATS server requires auth.")
    var natsPassword: String?

    @Flag(name: .customLong("tls"), help: "Require a TLS connection to the NATS server.")
    var tls: Bool = false

    @Option(name: .customLong("host"), help: "Host the HTTP server binds to.")
    var host: String = "0.0.0.0"

    @Option(name: .customLong("port"), help: "Port the HTTP server binds to.")
    var port: Int = 8080

    @Option(name: .customLong("log-level"), help: "Log level (trace, debug, info, notice, warning, error, critical).")
    var logLevel: String = "info"

    func run() async throws {
        var logger = Logger(label: "swift-nats-consumer")
        logger.logLevel = Logger.Level(rawValue: logLevel) ?? .info

        guard let url = URL(string: natsUrl) else {
            throw ValidationError("--nats-url \"\(natsUrl)\" is not a valid URL.")
        }

        if (natsUsername == nil) != (natsPassword == nil) {
            throw ValidationError("--nats-username and --nats-password must be provided together.")
        }

        let consumer = NatsConsumer(
            url: url,
            subject: subject,
            username: natsUsername,
            password: natsPassword,
            requireTls: tls,
            logger: logger
        )

        let server = Server.build(host: host, port: port, logger: logger)

        // Run the HTTP server and the NATS consume loop side by side; if either one throws
        // (e.g. the port is already in use), tear the other down instead of leaking a task.
        try await withThrowingTaskGroup(of: Void.self) { group in
            group.addTask { try await server.runService() }
            group.addTask { try await consumer.run() }
            try await group.next()
            group.cancelAll()
        }
    }
}
