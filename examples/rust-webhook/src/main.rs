//! Minimal webhook receiver: logs every request and its body. Run with: cargo run

use axum::{body::Bytes, http::HeaderMap, routing::post, Router};

async fn webhook(headers: HeaderMap, body: Bytes) -> &'static str {
    println!("Headers: {:?}", headers);
    println!("Body: {}", String::from_utf8_lossy(&body));
    "received"
}

#[tokio::main]
async fn main() {
    let app = Router::new().route("/webhook", post(webhook));
    let listener = tokio::net::TcpListener::bind("0.0.0.0:8000").await.unwrap();
    println!("Listening on 0.0.0.0:8000");
    axum::serve(listener, app).await.unwrap();
}
