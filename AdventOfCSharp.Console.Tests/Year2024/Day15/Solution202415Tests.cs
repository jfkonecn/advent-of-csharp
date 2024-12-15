using AdventOfCSharp.Console.Year2024.Day15;

namespace AdventOfCSharp.Console.Tests.Year2024.Day15;

[TestFixture]
public class Solution202415Tests
{
    [TestCase("example1.txt", 10092)]
    [TestCase("example2.txt", 2028)]
    [TestCase("real.txt", 1349898)]
    public async Task Solution1Tests(string fileName, long expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 15, fileName);
        var actual = Solution202415.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    //[TestCase("example2.txt", 9021)]
    [TestCase("example3.txt", 0)]
    public async Task Solution2Tests(string fileName, long expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 15, fileName);
        var actual = Solution202415.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
