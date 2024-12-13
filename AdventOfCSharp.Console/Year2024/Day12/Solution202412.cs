namespace AdventOfCSharp.Console.Year2024.Day12;

public static class Solution202412
{
    private record Plot
    {
        public required char Type { get; init; }
        public long Area { get; set; } = 0;
        public long Perimeter { get; set; } = 0;
        public List<(int x, int y)> Boundary { get; set; } = new();
    }

    private static List<Plot> Parse(string[] fileContents)
    {
        var visited = new HashSet<(int x, int y)>();
        var result = new List<Plot>();
        for (int x = 0; x < fileContents.Length; x++)
        {
            for (int y = 0; y < fileContents[x].Length; y++)
            {
                if (visited.Contains((x, y)))
                {
                    continue;
                }
                var plot = new Plot() { Type = fileContents[x][y] };
                GetPlotInfo(x, y, plot);
                plot.Boundary = plot.Boundary.OrderBy(x => x.x).ThenByDescending(x => x.y).ToList();
                result.Add(plot);
            }
        }
        return result;
        void GetPlotInfo(int x, int y, Plot plot)
        {
            char c = plot.Type;
            if (
                x < 0
                || y < 0
                || x >= fileContents.Length
                || y >= fileContents[x].Length
                || fileContents[x][y] != c
                || visited.Contains((x, y))
            )
            {
                return;
            }

            visited.Add((x, y));
            plot.Area++;

            bool leftBorder = x == 0 || fileContents[x - 1][y] != c;
            bool topBorder = y == 0 || fileContents[x][y - 1] != c;
            bool rightBorder = x == fileContents.Length - 1 || fileContents[x + 1][y] != c;
            bool bottomBorder = y == fileContents[x].Length - 1 || fileContents[x][y + 1] != c;

            var startingPerimeter = plot.Perimeter;
            if (leftBorder)
            {
                plot.Perimeter++;
            }
            if (topBorder)
            {
                plot.Perimeter++;
            }
            if (rightBorder)
            {
                plot.Perimeter++;
            }
            if (bottomBorder)
            {
                plot.Perimeter++;
            }

            if (startingPerimeter != plot.Perimeter)
            {
                plot.Boundary.Add((x, y));
            }

            GetPlotInfo(x + 1, y, plot);
            GetPlotInfo(x - 1, y, plot);
            GetPlotInfo(x, y + 1, plot);
            GetPlotInfo(x, y - 1, plot);
        }
    }

    public static long Solution1(string[] fileContents)
    {
        var plots = Parse(fileContents);
        return plots.Select(x => x.Area * x.Perimeter).Sum();
    }

    public static long Solution2(string[] fileContents)
    {
        var plots = Parse(fileContents);
        foreach (var plot in plots)
        {
            System.Console.WriteLine(plot);
        }
        return plots
            .Select(x =>
            {
                return x.Area * x.Area;
            })
            .Sum();
    }
}
