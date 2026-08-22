// swift-tools-version:5.10
import PackageDescription

let package = Package(
    name: "swift-webhook",
    platforms: [
        .macOS(.v14)
    ],
    dependencies: [
        .package(url: "https://github.com/hummingbird-project/hummingbird.git", from: "2.0.0"),
        .package(url: "https://github.com/apple/swift-nio.git", from: "2.63.0")
    ],
    targets: [
        .executableTarget(
            name: "swift-webhook",
            dependencies: [
                .product(name: "Hummingbird", package: "hummingbird"),
                // Only needed for `ByteBuffer.readableBytesView` in App.swift, to turn the
                // collected request body into `Foundation.Data` for decoding.
                .product(name: "NIOCore", package: "swift-nio")
            ]
        )
    ]
)
