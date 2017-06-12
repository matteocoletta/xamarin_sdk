#!/usr/bin/env bash

set -e

SDKDIR=./sdk/Adjust
JARINDIR=./sdk/Adjust/testlibrary/build/outputs
JAROUTDIR=../AdjustTesting.Xamarin.Android/Jars

(cd $SDKDIR; ./gradlew clean :testlibrary:makeJar)

rm -v -f $JAROUTDIR/adjust-testing.jar
cp -v $JARINDIR/adjust*.jar $JAROUTDIR/adjust-testing.jar
