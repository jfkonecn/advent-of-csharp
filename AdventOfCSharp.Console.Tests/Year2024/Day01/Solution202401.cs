using AdventOfCSharp.Console.Year2024.Day01;

namespace AdventOfCSharp.Console.Tests.Year2024.Day01;

[TestFixture]
public class Solution202401Tests
{
    [TestCase("")]
    public void Solution1(string fileName)
    {
        var fileContents = fileName;
        Assert.AreEqual(0, Solution202401.Solution1(fileContents));
    }
}
