#!/usr/bin/env bash

# Builds the JAR files and puts them in the correct /lib paths.
# Does NOT play or run the app. This has to be done from the IDE (Visual Studio)

set -e

ROOT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
ROOT_DIR="$(dirname "$ROOT_DIR")"
ANDROID_EXT_DIR=Android/ext

RED='\033[0;31m' # Red color
GREEN='\033[0;32m' # Green color
NC='\033[0m' # No Color

echo -e "${GREEN}>>> START ${NC}"

cd $ROOT_DIR/$ANDROID_EXT_DIR
echo -e "${GREEN}>>> Building the Android JAR file ${NC}"
./build.sh

echo -e "${GREEN}>>> Building the Android TESTING JAR file ${NC}"
./build_testing.sh

echo -e "${GREEN}>>> DONE ${NC}"
