using AdventOfCSharp.Console.Year2024.Day14;
using AdventOfCSharp.Console.Year2024.Day17;

Console.WriteLine("Enter lines of text (Ctrl+D to stop):");
string? line;
var fileContents = new List<string>();
while ((line = Console.ReadLine()) != null)
{
    fileContents.Add(line);
}

//Solution202414.Solution2(fileContents.ToArray(), 101, 103, null);
//Solution202414.Solution2(fileContents.ToArray(), 11, 7, null);
Solution202417.Solution2(fileContents.ToArray());
