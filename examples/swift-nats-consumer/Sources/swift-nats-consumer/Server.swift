import Hummingbird
import Logging

/// The Hummingbird HTTP side of the consumer: `/health` for a plain liveness check, `/status`
/// for the metrics `NatsConsumer` and `EventHandler` have been recording.
enum Server {
    static func build(host: String, port: Int, logger: Logger) -> Application<RouterResponder<BasicRequestContext>> {
        let router = Router()

        router.get("/health") { _, _ in
            "ok"
        }

        router.get("/status") { _, _ in
            await Metrics.shared.snapshot()
        }

        return Application(
            router: router,
            configuration: .init(address: .hostname(host, port: port)),
            logger: logger
        )
    }
}
