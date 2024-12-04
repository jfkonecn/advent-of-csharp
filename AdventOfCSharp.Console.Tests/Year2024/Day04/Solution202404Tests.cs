using AdventOfCSharp.Console.Year2024.Day04;

namespace AdventOfCSharp.Console.Tests.Year2024.Day04;

[TestFixture]
public class Solution202404Tests
{
    [TestCase("example1.txt", 18)]
    [TestCase("real.txt", 2493)]
    public async Task Solution1Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 04, fileName);
        var actual = Solution202404.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 9)]
    [TestCase("real.txt", 1890)]
    public async Task Solution2Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 04, fileName);
        var actual = Solution202404.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
