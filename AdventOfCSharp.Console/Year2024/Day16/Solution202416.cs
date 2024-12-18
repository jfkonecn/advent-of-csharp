namespace AdventOfCSharp.Console.Year2024.Day16;

public static class Solution202416
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

    private enum Direction
    {
        Unknown,
        North,
        South,
        East,
        West,
    }

    private static List<(List<(int x, int y)> History, int Score)> FindCost(ParseResult result)
    {
        var directions = new (Direction, int dx, int dy)[]
        {
            (Direction.North, 0, -1),
            (Direction.South, 0, 1),
            (Direction.East, 1, 0),
            (Direction.West, -1, 0),
        };

        var routes = new List<(List<(int x, int y)> History, int Score)>();
        var visited = new Dictionary<((int x, int y), Direction), int>();

        var maze = result.Maze;
        var start = result.Start;
        var end = result.End;

        var queue =
            new Queue<((int x, int y) Pos, List<(int x, int y)> History, int Score, Direction)>();
        queue.Enqueue(
            ((start.X, start.Y), new List<(int, int)> { (start.X, start.Y) }, 0, Direction.East)
        );

        while (queue.Count > 0)
        {
            var (pos, history, currScore, currDir) = queue.Dequeue();
            var (x, y) = pos;

            if (pos.x == end.X && pos.y == end.Y)
            {
                routes.Add((new List<(int x, int y)>(history), currScore));
                continue;
            }

            if (visited.ContainsKey((pos, currDir)) && visited[(pos, currDir)] < currScore)
                continue;

            visited[(pos, currDir)] = currScore;

            foreach (var temp in directions)
            {
                var (dir, dx, dy) = temp;

                // Prevent reversing direction
                var isOppositeDirection = (currDir, dir) switch
                {

                    (Direction.North, Direction.South)
                    or
                    (Direction.South, Direction.North)
                    or
                    (Direction.East, Direction.West)
                    or (Direction.West, Direction.East) => true,
                    _ => false,
                };
                if (isOppositeDirection)
                    continue;

                int ny = y + dy;
                int nx = x + dx;

                if (
                    ny >= 0
                    && ny < maze.GetLength(0)
                    && nx >= 0
                    && nx < maze.GetLength(1)
                    && maze[ny, nx] != Space.Wall
                    && !history.Contains((nx, ny))
                )
                {
                    var newHistory = new List<(int, int)>(history) { (nx, ny) };
                    if (dir == currDir)
                    {
                        queue.Enqueue(((nx, ny), newHistory, currScore + 1, dir)); // Move forward
                    }
                    else
                    {
                        queue.Enqueue((pos, history, currScore + 1000, dir)); // Turn
                    }
                }
            }
        }

        return routes;
    }

    public static long Solution1(string[] fileContents)
    {
        var result = Parse(fileContents);
        var costs = FindCost(result);
        return costs.Select(x => x.Score).Min();
    }

    private static void PrintResult(ParseResult result)
    {
        var maze = result.Maze;
        for (int y = 0; y < maze.GetLength(0); y++)
        {
            for (int x = 0; x < maze.GetLength(1); x++)
            {
                System.Console.Write(
                    maze[y, x] switch
                    {
                        Space.Empty when result.Start.X == x && result.Start.Y == y => 'S',
                        Space.Empty when result.End.X == x && result.End.Y == y => 'E',
                        Space.Wall => '#',
                        Space.Empty => '.',
                        _ => '?',
                    }
                );
            }
            System.Console.WriteLine();
        }
    }

    public static int Solution2(string[] fileContents)
    {
        throw new NotImplementedException();
    }
}
