#!/usr/bin/env bash
# Generates model code for all target languages from the individual data-type schemas,
# and installs the C# output into the plugin project.
#
# Usage: build.sh <generate|install|all>
#
# Each schema file (camera_data.yaml, etc.) maps to one generated class.
# Passing all files to a single quicktype invocation produces one output file per language
# with no root wrapper class.
# Requires quicktype: npm install -g quicktype
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
SCHEMAS="$SCRIPT_DIR/schemas"
OUT="$SCRIPT_DIR/generated"
PLUGIN_DIR="$SCRIPT_DIR/../src/Bortle.NINA.Emitter"
DEST_DIR="$PLUGIN_DIR/Generated"

DATA_SCHEMAS=(
  "$SCHEMAS/camera_data.yaml"
  "$SCHEMAS/telescope_data.yaml"
  "$SCHEMAS/imaging_data.yaml"
  "$SCHEMAS/weather_data.yaml"
)

generate() {
  if ! command -v quicktype &>/dev/null; then
    echo "error: quicktype not found — install with: npm install -g quicktype"
    exit 1
  fi

  mkdir -p "$OUT/csharp" "$OUT/rust" "$OUT/typescript" "$OUT/swift"

  echo "Generating from: ${DATA_SCHEMAS[*]}"

  # ── C# ───────────────────────────────────────────────────────────────────
  echo "  → C#..."
  quicktype \
    --src-lang schema \
    --lang csharp \
    --namespace "Bortle.NINA.Emitter.Models" \
    --framework SystemTextJson \
    --array-type list \
    --out "$OUT/csharp/EmitterModels.cs" \
    "${DATA_SCHEMAS[@]}"

  # ── Rust ─────────────────────────────────────────────────────────────────
  echo "  → Rust..."
  quicktype \
    --src-lang schema \
    --lang rust \
    --visibility public \
    --out "$OUT/rust/emitter_models.rs" \
    "${DATA_SCHEMAS[@]}"

  # ── TypeScript ───────────────────────────────────────────────────────────
  echo "  → TypeScript..."
  quicktype \
    --src-lang schema \
    --lang typescript \
    --out "$OUT/typescript/emitterModels.ts" \
    "${DATA_SCHEMAS[@]}"

  # ── Swift ────────────────────────────────────────────────────────────────
  echo "  → Swift..."
  quicktype \
    --src-lang schema \
    --lang swift \
    --swift-5-support \
    --out "$OUT/swift/EmitterModels.swift" \
    "${DATA_SCHEMAS[@]}"

  echo "Done. Generated files are in $OUT/"
}

install() {
  local src="$OUT/csharp/EmitterModels.cs"

  if [ ! -f "$src" ]; then
    echo "error: $src not found — run '$0 generate' first"
    exit 1
  fi

  mkdir -p "$DEST_DIR"
  cp "$src" "$DEST_DIR/EmitterModels.cs"
  echo "Installed: $DEST_DIR/EmitterModels.cs"
}

usage() {
  echo "Usage: $0 <generate|install|all>"
  echo "  generate  Generate model code for all target languages from schemas"
  echo "  install   Copy the generated C# models into the plugin project"
  echo "  all       Run generate then install"
  exit 1
}

case "${1:-}" in
  generate|g)
    generate
    ;;
  install|i)
    install
    ;;
  all|a)
    generate
    install
    ;;
  *)
    usage
    ;;
esac
