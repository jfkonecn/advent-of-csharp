using AdventOfCSharp.Console.Year2023.Day12;

namespace AdventOfCSharp.Console.Tests.Year2023.Day12;

[TestFixture]
public class Solution202312Tests
{
    [TestCase("example1.txt")]
    public async Task Solution1Tests(string fileName)
    {
        var fileContents = await Utils.GetAdventTestFile(2023, 12, fileName);
        Assert.AreEqual(0, Solution202312.Solution1(fileContents));
    }
}
