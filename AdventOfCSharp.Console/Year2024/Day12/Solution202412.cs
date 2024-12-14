namespace AdventOfCSharp.Console.Year2024.Day12;

public static class Solution202412
{
    private record Plot
    {
        public required char Type { get; init; }
        public long Area { get; set; } = 0;
        public long Perimeter { get; set; } = 0;
        public HashSet<(int x, int y)> Points { get; set; } = new();
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
                plot.Points = plot.Points.OrderBy(x => x.x).ThenByDescending(x => x.y).ToHashSet();
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

            plot.Points.Add((x, y));

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
        return plots
            .Select(plot =>
            {
                var points = plot.Points;
                int sides = 0;

                var directions = new List<(int dx, int dy)> { (-1, 0), (1, 0), (0, -1), (0, 1) };

                foreach (var (dx, dy) in directions.Where(x => x.dy == 0))
                {
                    for (int x = 0; x < fileContents.Length; x++)
                    {
                        bool onASide = false;
                        for (int y = 0; y < fileContents[0].Length; y++)
                        {
                            int nx = x + dx;
                            int ny = y + dy;
                            if (!onASide && points.Contains((x, y)) && !points.Contains((nx, ny)))
                            {
                                sides++;
                                onASide = true;
                            }
                            else if (!points.Contains((x, y)) || points.Contains((nx, ny)))
                            {
                                onASide = false;
                            }
                        }
                    }
                }
                foreach (var (dx, dy) in directions.Where(x => x.dx == 0))
                {
                    for (int y = 0; y < fileContents[0].Length; y++)
                    {
                        bool onASide = false;
                        for (int x = 0; x < fileContents.Length; x++)
                        {
                            int nx = x + dx;
                            int ny = y + dy;
                            if (!onASide && points.Contains((x, y)) && !points.Contains((nx, ny)))
                            {
                                sides++;
                                onASide = true;
                            }
                            else if (!points.Contains((x, y)) || points.Contains((nx, ny)))
                            {
                                onASide = false;
                            }
                        }
                    }
                }
                return sides * plot.Area;
            })
            .Sum();
    }
}
