namespace AdventOfCSharp.Console.Year2024.Day08;

public static class Solution202408
{
    private record ParseResult
    {
        public Dictionary<char, List<(int x, int y)>> CharLookup { get; } = new();
        public Dictionary<(int x, int y), char> PointLookup { get; } = new();
    }

    private static ParseResult Parse(string[] fileContents)
    {
        var result = new ParseResult();
        var charLookup = result.CharLookup;
        var pointLookup = result.PointLookup;

        for (int x = 0; x < fileContents.Length; x++)
        {
            for (int y = 0; y < fileContents.Length; y++)
            {
                var c = fileContents[x][y];
                if (c != '.')
                {
                    charLookup.TryAdd(c, new List<(int x, int y)>());
                    charLookup[c].Add((x, y));
                    pointLookup.Add((x, y), c);
                }
            }
        }
        return result;
    }

    public static int Solution1(string[] fileContents)
    {
        var charLookup = Parse(fileContents).CharLookup;
        var pairs = new HashSet<(int x, int y)>();
        foreach (var (c, list) in charLookup)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var (curX, curY) = list[i];
                for (int j = 0; j < list.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    var (refX, refY) = list[j];
                    var newX = (curX - refX) + curX;
                    var newY = (curY - refY) + curY;
                    if (
                        newX >= 0
                        && newY >= 0
                        && newX < fileContents.Length
                        && newY < fileContents[i].Length
                    )
                    {
                        pairs.Add((newX, newY));
                    }
                }
            }
        }
        return pairs.Count;
    }

    public static int Solution2(string[] fileContents)
    {
        var charLookup = Parse(fileContents).CharLookup;
        var pairs = new HashSet<(int x, int y)>();
        foreach (var (c, list) in charLookup)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var (curX, curY) = list[i];
                for (int j = 0; j < list.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    var (refX, refY) = list[j];
                    var diffX = (curX - refX);
                    var diffY = (curY - refY);
                    var startX = curX;
                    var startY = curY;
                    while (
                        startX >= 0
                        && startY >= 0
                        && startX < fileContents.Length
                        && startY < fileContents[i].Length
                    )
                    {
                        startX -= diffX;
                        startY -= diffY;
                    }
                    startX += diffX;
                    startY += diffY;

                    while (
                        startX >= 0
                        && startY >= 0
                        && startX < fileContents.Length
                        && startY < fileContents[i].Length
                    )
                    {
                        pairs.Add((startX, startY));
                        startX += diffX;
                        startY += diffY;
                    }
                }
            }
        }
        return pairs.Count;
    }
}
