using AdventOfCSharp.Console.Year2024.Day05;

namespace AdventOfCSharp.Console.Tests.Year2024.Day05;

[TestFixture]
public class Solution202405Tests
{
    [TestCase("example1.txt", 143)]
    [TestCase("real.txt", 5129)]
    public async Task Solution1Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 05, fileName);
        var actual = Solution202405.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 123)]
    [TestCase("real.txt", 4077)]
    public async Task Solution2Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 05, fileName);
        var actual = Solution202405.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
