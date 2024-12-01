#!/bin/bash

# Check for required arguments
if [ "$#" -ne 2 ]; then
  echo "Usage: $0 <year> <day>"
  exit 1
fi

# Get year and zero-padded day
YEAR=$1
DAY=$(printf '%02d' $2)

# Define base directory relative to the script location
BASE_DIR=$(dirname "$0")

# Define paths for the files
SOLUTION_FILE="$BASE_DIR/AdventOfCSharp.Console/Year$YEAR/Day$DAY/Solution${YEAR}${DAY}.cs"
TEST_FILE="$BASE_DIR/AdventOfCSharp.Console.Tests/Year$YEAR/Day$DAY/Solution${YEAR}${DAY}Tests.cs"
ASSETS_DIR="$BASE_DIR/AdventOfCSharp.Console.Tests/assets/$YEAR/$DAY"
EXAMPLE_FILE="$ASSETS_DIR/example1.txt"
REAL_FILE="$ASSETS_DIR/real.txt"

# Create directories if they don't exist
mkdir -p "$(dirname "$SOLUTION_FILE")"
mkdir -p "$(dirname "$TEST_FILE")"
mkdir -p "$ASSETS_DIR"

# Generate the solution file
cat > "$SOLUTION_FILE" <<EOL
namespace AdventOfCSharp.Console.Year$YEAR.Day$DAY;

public static class Solution${YEAR}${DAY}
{
    public static int Solution1(string[] fileContents)
    {
        throw new NotImplementedException();
    }

    public static int Solution2(string[] fileContents)
    {
        throw new NotImplementedException();
    }
}
EOL

# Generate the test file
cat > "$TEST_FILE" <<EOL
using AdventOfCSharp.Console.Year$YEAR.Day$DAY;

namespace AdventOfCSharp.Console.Tests.Year$YEAR.Day$DAY;

[TestFixture]
public class Solution${YEAR}${DAY}Tests
{
    [TestCase("example1.txt", 0)]
    public async Task Solution1Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile($YEAR, $DAY, fileName);
        var actual = Solution${YEAR}${DAY}.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 0)]
    public async Task Solution2Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile($YEAR, $DAY, fileName);
        var actual = Solution${YEAR}${DAY}.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
EOL

# Generate the example file
echo "example 1" > "$EXAMPLE_FILE"

# Generate the real file
echo "real" > "$REAL_FILE"

# Success message
echo "Files for Year $YEAR, Day $DAY have been generated:"
echo "  - $SOLUTION_FILE"
echo "  - $TEST_FILE"
echo "  - $EXAMPLE_FILE"
echo "  - $REAL_FILE"

