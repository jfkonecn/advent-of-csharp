using AdventOfCSharp.Console.Year2024.Day13;

namespace AdventOfCSharp.Console.Tests.Year2024.Day13;

[TestFixture]
public class Solution202413Tests
{
    [TestCase("example1.txt", 480)]
    [TestCase("real.txt", 25751)]
    public async Task Solution1Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 13, fileName);
        var actual = Solution202413.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 0)]
    public async Task Solution2Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 13, fileName);
        var actual = Solution202413.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
