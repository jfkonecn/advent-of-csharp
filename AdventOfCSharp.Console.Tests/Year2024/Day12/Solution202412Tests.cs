using AdventOfCSharp.Console.Year2024.Day12;

namespace AdventOfCSharp.Console.Tests.Year2024.Day12;

[TestFixture]
public class Solution202412Tests
{
    [TestCase("example1.txt", 140)]
    [TestCase("example2.txt", 1930)]
    public async Task Solution1Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 12, fileName);
        var actual = Solution202412.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 0)]
    public async Task Solution2Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 12, fileName);
        var actual = Solution202412.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
