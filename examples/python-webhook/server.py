# /// script
# requires-python = ">=3.11"
# dependencies = ["fastapi", "uvicorn"]
# ///
"""Minimal webhook receiver: logs every request and its body. Run with: uv run server.py"""

import logging

import uvicorn
from fastapi import FastAPI, Request

logging.basicConfig(level=logging.INFO, format="%(asctime)s %(message)s")
log = logging.getLogger("webhook")

app = FastAPI()


@app.post("/webhook")
async def webhook(request: Request):
    body = await request.body()
    log.info("Headers: %s", dict(request.headers))
    log.info("Body: %s", body.decode(errors="replace"))
    return {"status": "received"}


if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8000)
