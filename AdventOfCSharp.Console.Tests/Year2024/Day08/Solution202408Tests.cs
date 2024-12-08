using AdventOfCSharp.Console.Year2024.Day08;

namespace AdventOfCSharp.Console.Tests.Year2024.Day08;

[TestFixture]
public class Solution202408Tests
{
    [TestCase("example1.txt", 14)]
    [TestCase("real.txt", 214)]
    public async Task Solution1Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 08, fileName);
        var actual = Solution202408.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 0)]
    public async Task Solution2Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 08, fileName);
        var actual = Solution202408.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
