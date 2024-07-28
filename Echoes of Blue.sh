#!/bin/sh
echo -ne '\033c\033]0;Echoes of Blue\a'
base_path="$(dirname "$(realpath "$0")")"
"$base_path/Echoes of Blue.x86_64" "$@"
