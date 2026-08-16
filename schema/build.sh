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
DOCS_OUT="$SCRIPT_DIR/../build/docs"

DATA_SCHEMAS=(
  "$SCHEMAS/device_connect_data.yaml"
  "$SCHEMAS/camera_data.yaml"
  "$SCHEMAS/dome_data.yaml"
  "$SCHEMAS/dome_shutter_data.yaml"
  "$SCHEMAS/filter_wheel_data.yaml"
  "$SCHEMAS/flat_panel_data.yaml"
  "$SCHEMAS/flat_panel_led_data.yaml"
  "$SCHEMAS/flat_panel_state_data.yaml"
  "$SCHEMAS/flat_panel_value_data.yaml"
  "$SCHEMAS/focuser_data.yaml"
  "$SCHEMAS/guider_data.yaml"
  "$SCHEMAS/guider_dither_data.yaml"
  "$SCHEMAS/guider_start_data.yaml"
  "$SCHEMAS/guider_step_data.yaml"
  "$SCHEMAS/mount_data.yaml"
  "$SCHEMAS/mount_flip_data.yaml"
  "$SCHEMAS/mount_move_data.yaml"
  "$SCHEMAS/rotator_data.yaml"
  "$SCHEMAS/rotator_move_data.yaml"
  "$SCHEMAS/safety_data.yaml"
  "$SCHEMAS/safety_change_data.yaml"
  "$SCHEMAS/switch_data.yaml"
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

generate_docs() {
  if ! command -v asyncapi &>/dev/null; then
    echo "error: asyncapi not found — install with: npm install -g @asyncapi/cli"
    exit 1
  fi

  echo "Generating docs from: $SCRIPT_DIR/asyncapi.yaml"

  # Run in a subshell so the cd is scoped to this command and the caller's
  # working directory is left untouched.
  (
    cd "$SCRIPT_DIR"
    asyncapi \
      generate fromTemplate \
      asyncapi.yaml \
      @asyncapi/html-template@3.0.0 \
      -o "$DOCS_OUT" \
      -p sidebarOrganization=byTags \
      -p favicon="https://kyleleneau.github.io/emitter-plugin/emitter_icon.jpg" \
      -p config='{"show":{"sidebar":true},"expand":{"messageExamples": true}}' \
      --use-new-generator
  )

  echo "Done. Docs are in $DOCS_OUT/"
}

usage() {
  echo "Usage: $0 <generate|install|all>"
  echo "  generate  Generate model code for all target languages from schemas"
  echo "  install   Copy the generated C# models into the plugin project"
  echo "  docs      Build the html asyncapi docs"
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
  docs)
    generate_docs
    ;;
  all|a)
    generate
    install
    ;;
  *)
    usage
    ;;
esac
