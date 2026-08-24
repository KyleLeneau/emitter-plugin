# Adapted from Hummingbird's own official example template
# (https://github.com/hummingbird-project/hummingbird-examples/blob/main/hello/Dockerfile),
# swapped to this package's product name and CLI flags.

# ================================
# Build image
# ================================
FROM swift:6.3-noble AS build

# Install OS updates
RUN export DEBIAN_FRONTEND=noninteractive DEBCONF_NONINTERACTIVE_SEEN=true \
    && apt-get -q update \
    && apt-get -q dist-upgrade -y \
    && apt-get install -y libjemalloc-dev build-essential curl \
    && rm -rf /var/lib/apt/lists/*

# nkeys.swift (a nats.swift dependency, for NATS nkey/JWT auth) pulls in swift-sodium, which
# on Linux links against a system libsodium — but it needs symbols (post-quantum crypto_kem_*,
# sodium_ip2bin/bin2ip) that only exist in libsodium 1.0.20+, newer than the 1.0.18 every
# current Ubuntu release ships via apt. Build it from source instead; the runtime stage copies
# the same build rather than installing the (too old) libsodium23 apt package.
ARG LIBSODIUM_VERSION=1.0.22
RUN curl -fsSL "https://download.libsodium.org/libsodium/releases/libsodium-${LIBSODIUM_VERSION}.tar.gz" \
      -o /tmp/libsodium.tar.gz \
    && tar xzf /tmp/libsodium.tar.gz -C /tmp \
    && cd "/tmp/libsodium-${LIBSODIUM_VERSION}" \
    && ./configure \
    && make -j"$(nproc)" \
    && make install \
    && ldconfig \
    && rm -rf /tmp/libsodium*

# Set up a build area
WORKDIR /build

# First just resolve dependencies. This creates a cached layer that can be reused as long as
# Package.swift/Package.resolved don't change, so `docker build` skips re-fetching nats.swift,
# Hummingbird, etc. on every source change.
COPY ./Package.* ./
RUN swift package resolve

# nats.swift v0.4.0 (still true on its `main` branch as of 2026-08-24 — no fix released yet)
# calls URLSession/CharacterSet APIs in NatsConnection.swift under a plain `import Foundation`,
# with no `#if canImport(FoundationNetworking)` guard. Foundation re-exports those on Darwin
# but not on Linux, where they live in the separate FoundationNetworking module — so the
# package fails to compile on Linux at all, independent of anything in this Dockerfile. Patch
# the vendored checkout to add the missing import; the grep guards make this fail loudly
# (instead of silently no-op'ing) if a future nats.swift release moves or fixes this, so a
# version bump doesn't quietly ship a stale patch.
RUN NATS_CONNECTION_FILE=".build/checkouts/nats.swift/Sources/Nats/NatsConnection.swift" \
    && grep -q '^import Foundation$' "$NATS_CONNECTION_FILE" \
    && awk '1; /^import Foundation$/ && !patched { print "#if canImport(FoundationNetworking)"; print "import FoundationNetworking"; print "#endif"; patched=1 }' \
      "$NATS_CONNECTION_FILE" > "$NATS_CONNECTION_FILE.patched" \
    && mv "$NATS_CONNECTION_FILE.patched" "$NATS_CONNECTION_FILE" \
    && grep -q 'canImport(FoundationNetworking)' "$NATS_CONNECTION_FILE"

# Copy the rest of the package
COPY . .

# Build the application, with optimizations, with static linking, and using jemalloc
RUN swift build -c release \
    --product "swift-nats-consumer" \
    --static-swift-stdlib \
    -Xlinker -ljemalloc

# Switch to the staging area
WORKDIR /staging

# Copy main executable to staging area
RUN cp "$(swift build --package-path /build -c release --show-bin-path)/swift-nats-consumer" ./

# Copy static swift backtracer binary to staging area
RUN cp "/usr/libexec/swift/linux/swift-backtrace-static" ./

# ================================
# Run image
# ================================
FROM ubuntu:noble

# Make sure all system packages are up to date, and install only essential packages.
# libcurl4 is needed because nats.swift imports FoundationNetworking (see the
# canImport(FoundationNetworking) patch above) — swift-corelibs-foundation's networking layer
# is backed by libcurl on Linux, unlike Darwin where it's a thin wrapper over the OS's own
# URLSession.
RUN export DEBIAN_FRONTEND=noninteractive DEBCONF_NONINTERACTIVE_SEEN=true \
    && apt-get -q update \
    && apt-get -q dist-upgrade -y \
    && apt-get -q install -y \
      libjemalloc2 \
      libcurl4 \
      ca-certificates \
      tzdata \
    && rm -r /var/lib/apt/lists/*

# The libsodium built from source in the build stage — apt's libsodium23 is too old (see the
# comment above the source build). Copying just the shared libs, not the headers/static archive
# the build stage also installed under /usr/local.
COPY --from=build /usr/local/lib/libsodium.so* /usr/local/lib/
RUN ldconfig

# Create a low-privilege user and group to run as
RUN useradd --user-group --create-home --system --skel /dev/null --home-dir /app consumer

# Switch to the new home directory
WORKDIR /app

# Copy built executable and the static backtracer from the builder stage
COPY --from=build --chown=consumer:consumer /staging /app

# Provide configuration needed by the built-in crash reporter
ENV SWIFT_BACKTRACE=enable=yes,sanitize=yes,threads=all,images=all,interactive=no,swift-backtrace=./swift-backtrace-static

# Ensure all further commands run as the unprivileged user
USER consumer:consumer

# The HTTP side (/status, /health) listens here by default
EXPOSE 8080

# --nats-url almost always needs overriding at `docker run`/compose time — "localhost" inside
# the container is the container itself, not a NATS server on the host or elsewhere.
ENTRYPOINT ["./swift-nats-consumer"]
CMD ["--nats-url", "nats://nats:4222", "--host", "0.0.0.0", "--port", "8080"]
