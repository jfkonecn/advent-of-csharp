namespace AdventOfCSharp.Console.Year2024.Day20;

public static class Solution202420
{
    private enum Space
    {
        Unknown,
        Wall,
        Empty,
    }

    private record Point
    {
        public int X { get; init; }
        public int Y { get; init; }
    }

    private record ParseResult
    {
        public required Space[,] Maze { get; init; }
        public required Point Start { get; init; }
        public required Point End { get; init; }
    }

    private static ParseResult Parse(string[] fileContents)
    {
        var maze = new Space[fileContents.Length, fileContents[0].Length];
        Point? start = null;
        Point? end = null;
        for (int y = 0; y < maze.GetLength(0); y++)
        {
            for (int x = 0; x < maze.GetLength(1); x++)
            {
                var c = fileContents[y][x];

                if (c == 'S')
                {
                    start = new() { X = x, Y = y };
                }
                else if (c == 'E')
                {
                    end = new() { X = x, Y = y };
                }
                maze[y, x] = c switch
                {
                    '.' or 'S' or 'E' => Space.Empty,
                    '#' => Space.Wall,
                    _ => throw new Exception($"Unknown char '{c}'"),
                };
            }
        }

        if (start == null)
        {
            throw new Exception("Start not found");
        }
        else if (end == null)
        {
            throw new Exception("End not found");
        }

        return new()
        {
            Maze = maze,
            Start = start,
            End = end,
        };
    }

    private static int Distance(Point p1, Point p2)
    {
        return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
    }

    private static List<(List<Point> History, int Score, Point? Cheat)> FindCost(ParseResult result)
    {
        var directions = new (int dx, int dy)[] { (0, -1), (0, 1), (1, 0), (-1, 0) };

        var routes = new List<(List<Point> History, int Score, Point? CheatPoint)>();
        var visited = new Dictionary<(Point Position, Point? CheatPoint), int>();

        var maze = result.Maze;
        var start = result.Start;
        var end = result.End;

        var queue = new Queue<(Point Pos, List<Point> History, int Score, Point? cheatPoint)>();
        queue.Enqueue((start, new List<Point> { start }, 0, null));

        while (queue.Count > 0)
        {
            var (pos, history, currScore, cheated) = queue.Dequeue();
            var x = pos.X;
            var y = pos.Y;

            if (pos.X == end.X && pos.Y == end.Y)
            {
                routes.Add((new List<Point>(history), currScore, cheated));
                continue;
            }

            if (visited.ContainsKey((pos, cheated)))
            {
                continue;
            }

            visited[(pos, cheated)] = currScore;

            foreach (var temp in directions)
            {
                var (dx, dy) = temp;

                int ny = y + dy;
                int nx = x + dx;

                var nPoint = new Point() { X = nx, Y = ny };

                if (
                    ny >= 0
                    && ny < maze.GetLength(0)
                    && nx >= 0
                    && nx < maze.GetLength(1)
                    && (
                        maze[ny, nx] != Space.Wall
                        || (maze[ny, nx] == Space.Wall && cheated == null)
                    //|| (
                    //maze[ny, nx] == Space.Wall
                    //&& cheated != null
                    //&& Distance(nPoint, cheated) == 1
                    //)
                    )
                    && !history.Contains(nPoint)
                )
                {
                    var newHistory = new List<Point>(history) { nPoint };
                    if (maze[ny, nx] == Space.Wall && cheated == null)
                    {
                        queue.Enqueue((nPoint, newHistory, currScore + 1, nPoint));
                    }
                    else
                    {
                        queue.Enqueue((nPoint, newHistory, currScore + 1, cheated));
                    }
                }
            }
        }

        return routes;
    }

    public static int Solution1(string[] fileContents, int minSavings)
    {
        var result = Parse(fileContents);
        var cost = FindCost(result);
        var noCheats = cost.Where(x => x.Cheat == null).Single();
        var totalTimeSavingCheats = cost.Where(x =>
                x.Score < noCheats.Score && noCheats.Score - x.Score >= 0
            )
            .Count();
        return totalTimeSavingCheats;
    }

    public static int Solution2(string[] fileContents)
    {
        throw new NotImplementedException();
    }
}
