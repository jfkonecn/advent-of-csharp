using AdventOfCSharp.Console.Year2024.Day06;

namespace AdventOfCSharp.Console.Tests.Year2024.Day06;

[TestFixture]
public class Solution202406Tests
{
    [TestCase("example1.txt", 41)]
    [TestCase("real.txt", 5030)]
    public async Task Solution1Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 06, fileName);
        var actual = Solution202406.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 6)]
    [TestCase("real.txt", 2124)]
    public async Task Solution2Tests(string fileName, int expected)
    {
        // 2124 is too high
        var fileContents = await Utils.GetAdventTestFile(2024, 06, fileName);
        var actual = Solution202406.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
