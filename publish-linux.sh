#!/usr/bin/env bash
set -euo pipefail

dotnet publish Encrypto/Encrypto.csproj -p:PublishProfile=Linux
