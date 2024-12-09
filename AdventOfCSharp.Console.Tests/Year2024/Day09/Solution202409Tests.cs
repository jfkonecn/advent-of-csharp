using AdventOfCSharp.Console.Year2024.Day09;

namespace AdventOfCSharp.Console.Tests.Year2024.Day09;

[TestFixture]
public class Solution202409Tests
{
    [TestCase("example1.txt", 1928)]
    [TestCase("real.txt", 6395800119709)]
    public async Task Solution1Tests(string fileName, long expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 09, fileName);
        var actual = Solution202409.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 0)]
    public async Task Solution2Tests(string fileName, long expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 09, fileName);
        var actual = Solution202409.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
