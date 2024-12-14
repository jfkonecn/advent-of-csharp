using AdventOfCSharp.Console.Year2024.Day12;

namespace AdventOfCSharp.Console.Tests.Year2024.Day12;

[TestFixture]
public class Solution202412Tests
{
    [TestCase("example1.txt", 140)]
    [TestCase("example2.txt", 1930)]
    [TestCase("real.txt", 1467094)]
    public async Task Solution1Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 12, fileName);
        var actual = Solution202412.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 80)]
    [TestCase("example2.txt", 1206)]
    [TestCase("example3.txt", 236)]
    [TestCase("example4.txt", 368)]
    [TestCase("real.txt", 881182)]
    public async Task Solution2Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 12, fileName);
        var actual = Solution202412.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
