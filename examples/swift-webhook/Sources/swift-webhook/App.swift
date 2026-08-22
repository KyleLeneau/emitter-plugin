import Foundation
import Hummingbird
import Logging
import NIOCore

@main
struct App {
    static func main() async throws {
        let logger: Logger = {
            var logger = Logger(label: "swift-webhook")
            logger.logLevel = .info
            return logger
        }()

        let router = Router()
        router.post("/webhook") { request, context -> HTTPResponse.Status in
            let buffer = try await request.body.collect(upTo: context.maxUploadSize)
            WebhookHandler.handle(Data(buffer.readableBytesView), logger: logger)
            return .ok
        }

        let app = Application(
            router: router,
            configuration: .init(address: .hostname("0.0.0.0", port: 8000)),
            logger: logger
        )
        try await app.runService()
    }
}
