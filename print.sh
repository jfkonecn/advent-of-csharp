#!/bin/bash

if [ "$#" -ne 3 ]; then
  echo "Usage: $0 <year> <day> <fileName>"
  exit 1
fi

YEAR=$1
DAY=$(printf '%02d' $2)
FILENAME=$3

cat "AdventOfCSharp.Console.Tests/assets/$YEAR/$DAY/$FILENAME" \
    | dotnet run --project AdventOfCSharp.Console

