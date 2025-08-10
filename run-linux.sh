#!/usr/bin/env bash
set -euo pipefail

PUBLISH_DIR="Encrypto/bin/Release/net8.0/linux-x64/publish"
APP="$PUBLISH_DIR/Encrypto"

if [ ! -f "$APP" ]; then
  echo "Publishing application for linux-x64..."
  ./publish-linux.sh
fi

"$APP" "$@"
