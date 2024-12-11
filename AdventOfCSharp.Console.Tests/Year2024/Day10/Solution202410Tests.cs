using AdventOfCSharp.Console.Year2024.Day10;

namespace AdventOfCSharp.Console.Tests.Year2024.Day10;

[TestFixture]
public class Solution202410Tests
{
    [TestCase("example1.txt", 1)]
    [TestCase("example2.txt", 36)]
    [TestCase("real.txt", 557)]
    public async Task Solution1Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 10, fileName);
        var actual = Solution202410.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example2.txt", 81)]
    [TestCase("real.txt", 1062)]
    public async Task Solution2Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 10, fileName);
        var actual = Solution202410.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
