#!/bin/bash

mvn gatling:test

if [ "$LT_COPY_RESULTS_TO_S3" = "true" ]
then
  aws s3 cp ./target/gatling/ "$LT_S3_RESULTS_BUCKET" --recursive
fi