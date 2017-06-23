#!/usr/bin/env bash

set -e

ROOT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
ROOT_DIR="$(dirname "$ROOT_DIR")"
ROOT_DIR="$(dirname "$ROOT_DIR")"
SDKDIR=Android/ext/sdk/Adjust
JARINDIR=Android/ext/sdk/Adjust/testlibrary/build/outputs
JAROUTDIR=Android/AdjustTesting.Xamarin.Android/Jars

cd $ROOT_DIR/$SDKDIR
./gradlew clean :testlibrary:makeJar

cd $ROOT_DIR
rm -v -f $JAROUTDIR/adjust-testing.jar
cp -v $JARINDIR/adjust*.jar $JAROUTDIR/adjust-testing.jar
