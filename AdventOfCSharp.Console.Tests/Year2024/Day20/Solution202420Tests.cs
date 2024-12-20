using AdventOfCSharp.Console.Year2024.Day20;

namespace AdventOfCSharp.Console.Tests.Year2024.Day20;

[TestFixture]
public class Solution202420Tests
{
    [TestCase("example1.txt", 44, 0)]
    [TestCase("real.txt", 44, 100)]
    public async Task Solution1Tests(string fileName, int expected, int minSavings)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 20, fileName);
        var actual = Solution202420.Solution1(fileContents, minSavings);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 0)]
    public async Task Solution2Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 20, fileName);
        var actual = Solution202420.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
