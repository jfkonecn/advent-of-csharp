using AdventOfCSharp.Console.Year2024.Day14;

namespace AdventOfCSharp.Console.Tests.Year2024.Day14;

[TestFixture]
public class Solution202414Tests
{
    [TestCase("example1.txt", 12, 11, 7)]
    [TestCase("real.txt", 236628054, 101, 103)]
    public async Task Solution1Tests(string fileName, int expected, int width, int height)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 14, fileName);
        var actual = Solution202414.Solution1(fileContents, width, height);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 0, 11, 7)]
    public async Task Solution2Tests(string fileName, int expected, int width, int height)
    {
        // Answer was 7584
        var fileContents = await Utils.GetAdventTestFile(2024, 14, fileName);
        var actual = Solution202414.Solution2(fileContents, width, height, 100);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
