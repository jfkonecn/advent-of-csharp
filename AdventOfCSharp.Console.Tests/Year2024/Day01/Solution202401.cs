using AdventOfCSharp.Console.Year2024.Day01;

namespace AdventOfCSharp.Console.Tests.Year2024.Day01;

[TestFixture]
public class Solution202401Tests
{
    [TestCase("example1.txt")]
    public async Task Solution1Tests(string fileName)
    {
        var fileContents = await Utils.GetAdventTestFile(2024, 1, fileName);
        Assert.AreEqual(0, Solution202401.Solution1(fileContents));
    }
}
