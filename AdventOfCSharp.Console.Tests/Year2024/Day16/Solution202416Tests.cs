using AdventOfCSharp.Console.Year2024.Day16;

namespace AdventOfCSharp.Console.Tests.Year2024.Day16;

[TestFixture]
public class Solution202416Tests
{
    [TestCase("example1.txt", 7036)]
    [TestCase("example2.txt", 11048)]
    [TestCase("real.txt", 103512)]
    public async Task Solution1Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 16, fileName);
        var actual = Solution202416.Solution1(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example1.txt", 45)]
    [TestCase("example2.txt", 64)]
    [TestCase("real.txt", 554)]
    public async Task Solution2Tests(string fileName, int expected)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 16, fileName);
        var actual = Solution202416.Solution2(fileContents);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
