# Rust Webhook

Extremely minimal Http webhook for testing. Just run `cargo run` to start the server, then configure Emitter plugin to use `http://localhost:8000/webhook` as the URL (you might need to change the host for your network).

This example using `axum` and `tokio` crates for minimal async setup.
