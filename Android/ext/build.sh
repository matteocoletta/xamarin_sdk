#!/usr/bin/env bash

set -e

SDKDIR=./sdk/Adjust
JARINDIR=./sdk/Adjust/adjust/build/outputs
JAROUTDIR=../AdjustSdk.Xamarin.Android/Jars

(cd $SDKDIR; ./gradlew clean :adjust:makeJar)

rm -v -f $JAROUTDIR/adjust-android.jar
cp -v $JARINDIR/adjust*.jar $JAROUTDIR/adjust-android.jar
