using AdventOfCSharp.Console.Year2024.Day01;

namespace AdventOfCSharp.Console.Tests.Year2024.Day01;

[TestFixture]
public class Solution202401Tests
{
    [TestCase("example1.txt", 11)]
    [TestCase("real.txt", 2196996)]
    public async Task Solution1Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 1, fileName);
        var actual = Solution202401.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 31)]
    [TestCase("real.txt", 23655822)]
    public async Task Solution2Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 1, fileName);
        var actual = Solution202401.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
