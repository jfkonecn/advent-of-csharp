using AdventOfCSharp.Console.Year2024.Day02;

namespace AdventOfCSharp.Console.Tests.Year2024.Day02;

[TestFixture]
public class Solution202402Tests
{
    [TestCase("example1.txt", 2)]
    [TestCase("real.txt", 402)]
    public async Task Solution1Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 2, fileName);
        var actual = Solution202402.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 4)]
    [TestCase("real.txt", 455)]
    public async Task Solution2Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 2, fileName);
        var actual = Solution202402.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
