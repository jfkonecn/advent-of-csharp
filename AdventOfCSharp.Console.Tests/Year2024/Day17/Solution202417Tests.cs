using AdventOfCSharp.Console.Year2024.Day17;

namespace AdventOfCSharp.Console.Tests.Year2024.Day17;

[TestFixture]
public class Solution202417Tests
{
    [TestCase("example1.txt", "4,6,3,5,6,3,5,2,1,0")]
    [TestCase("real.txt", "3,5,0,1,5,1,5,1,0")]
    public async Task Solution1Tests(string fileName, string expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 17, fileName);
        var actual = Solution202417.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("real.txt", 107413700225434)]
    public async Task Solution2Tests(string fileName, long expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 17, fileName);
        var actual = Solution202417.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
