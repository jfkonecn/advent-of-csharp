#!/bin/bash

if [ "$#" -ne 2 ]; then
  echo "Usage: $0 <year> <day>"
  exit 1
fi

YEAR=$1
DAY=$(printf '%02d' $2)

NAMESPACE="AdventOfCSharp.Console.Tests.Year$YEAR.Day$DAY"
CLASS_NAME="Solution${YEAR}${DAY}Tests"

echo "Running tests for namespace: $NAMESPACE, class: $CLASS_NAME"
dotnet test --filter FullyQualifiedName~$NAMESPACE.$CLASS_NAME

