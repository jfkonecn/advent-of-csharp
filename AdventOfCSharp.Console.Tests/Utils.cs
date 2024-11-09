namespace AdventOfCSharp.Console.Tests;

static class Utils
{
    public static Task<string[]> GetAdventTestFile(int year, int day, string fileName)
    {
        var path = Path.Join("assets", year.ToString(), day.ToString("D2"), fileName);
        return File.ReadAllLinesAsync(path);
    }
}
