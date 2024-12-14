using AdventOfCSharp.Console.Year2024.Day14;

namespace AdventOfCSharp.Console.Tests.Year2024.Day14;

[TestFixture]
public class Solution202414Tests
{
    [TestCase("example1.txt", 0)]
    public async Task Solution1Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 14, fileName);
        var actual = Solution202414.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 0)]
    public async Task Solution2Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 14, fileName);
        var actual = Solution202414.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
