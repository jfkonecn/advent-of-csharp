using AdventOfCSharp.Console.Year2024.Day19;

namespace AdventOfCSharp.Console.Tests.Year2024.Day19;

[TestFixture]
public class Solution202419Tests
{
    [TestCase("example1.txt", 6)]
    [TestCase("real.txt", 228)]
    public async Task Solution1Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 19, fileName);
        var actual = Solution202419.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 16)]
    [TestCase("real.txt", 584553405070389)]
    public async Task Solution2Tests(string fileName, long expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 19, fileName);
        var actual = Solution202419.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
