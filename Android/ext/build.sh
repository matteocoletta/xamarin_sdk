#!/usr/bin/env bash

set -e

ROOT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
ROOT_DIR="$(dirname "$ROOT_DIR")"
ROOT_DIR="$(dirname "$ROOT_DIR")"
SDKDIR=Android/ext/sdk/Adjust
JARINDIR=Android/ext/sdk/Adjust/adjust/build/outputs
JAROUTDIR=Android/AdjustSdk.Xamarin.Android/Jars

cd $ROOT_DIR/$SDKDIR
./gradlew clean :adjust:makeJar

cd $ROOT_DIR
rm -v -f $JAROUTDIR/adjust-android.jar
cp -v $JARINDIR/adjust*.jar $JAROUTDIR/adjust-android.jar
