// swift-tools-version:5.10
import PackageDescription

let package = Package(
    name: "swift-nats-consumer",
    platforms: [
        .macOS(.v14)
    ],
    dependencies: [
        .package(url: "https://github.com/hummingbird-project/hummingbird.git", from: "2.0.0"),
        .package(url: "https://github.com/nats-io/nats.swift.git", from: "0.4.0"),
        .package(url: "https://github.com/apple/swift-argument-parser.git", from: "1.3.0")
    ],
    targets: [
        .executableTarget(
            name: "swift-nats-consumer",
            dependencies: [
                .product(name: "Hummingbird", package: "hummingbird"),
                .product(name: "Nats", package: "nats.swift"),
                .product(name: "ArgumentParser", package: "swift-argument-parser")
            ]
        )
    ]
)
