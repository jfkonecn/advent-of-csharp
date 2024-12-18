using AdventOfCSharp.Console.Year2024.Day18;

namespace AdventOfCSharp.Console.Tests.Year2024.Day18;

[TestFixture]
public class Solution202418Tests
{
    [TestCase("example1.txt", 22, 12, 7)]
    [TestCase("real.txt", 308, 1024, 71)]
    public async Task Solution1Tests(
        string fileName,
        int expected,
        int? simulateBytes,
        int gridSize
    )
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 18, fileName);
        var actual = Solution202418.Solution1(fileContents, simulateBytes, gridSize);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", "6,1", null, 7)]
    [TestCase("real.txt", "46,28", null, 71)]
    public async Task Solution2Tests(
        string fileName,
        string expected,
        int? simulateBytes,
        int gridSize
    )
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 18, fileName);
        var actual = Solution202418.Solution2(fileContents, simulateBytes, gridSize);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
