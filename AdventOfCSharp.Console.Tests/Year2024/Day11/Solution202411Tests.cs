using AdventOfCSharp.Console.Year2024.Day11;

namespace AdventOfCSharp.Console.Tests.Year2024.Day11;

[TestFixture]
public class Solution202411Tests
{
    [TestCase("example1.txt", 7, 1)]
    [TestCase("example2.txt", 22, 6)]
    [TestCase("example2.txt", 55312, 25)]
    [TestCase("real.txt", 186424, 25)]
    public async Task Solution1Tests(string fileName, int expected, int blinks)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 11, fileName);
        var actual = Solution202411.Solution1(fileContents, blinks);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("real.txt", 219838428124832L, 75)]
    public async Task Solution2Tests(string fileName, long expected, int blinks)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 11, fileName);
        var actual = Solution202411.Solution2(fileContents, blinks);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
