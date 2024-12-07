using AdventOfCSharp.Console.Year2024.Day07;

namespace AdventOfCSharp.Console.Tests.Year2024.Day07;

[TestFixture]
public class Solution202407Tests
{
    [TestCase("example1.txt", 0)]
    public async Task Solution1Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 07, fileName);
        var actual = Solution202407.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 0)]
    public async Task Solution2Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 07, fileName);
        var actual = Solution202407.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
