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

    private static long FindCost(ParseResult result)
    {
        var directionArray = new (Direction, int dx, int dy)[]
        {
            (Direction.North, 0, -1),
            (Direction.South, 0, 1),
            (Direction.East, 1, 0),
            (Direction.West, -1, 0),
        };
        var dic = new Dictionary<(Direction, int x, int y), long?>();

        var endX = result.End.X;
        var endY = result.End.Y;
        var maze = result.Maze;

        return FindCostRec(Direction.East, result.Start.X, result.Start.Y)
            ?? throw new Exception("No Path Found");

        long? FindCostRec(Direction direction, int x, int y)
        {
            System.Console.WriteLine($"Getting ({x}, {y})");
            if (dic.TryGetValue((direction, x, y), out var dicValue))
            {
                return dicValue;
            }
            else if (x == endX && y == endY)
            {
                return 0;
            }
            var curSpace = maze[y, x];

            if (curSpace == Space.Wall)
            {
                return null;
            }
            else if (curSpace == Space.Empty)
            {
                var answer = directionArray
                    .Select(args =>
                    {
                        var (tempDir, dx, dy) = args;
                        var pathValue = FindCostRec(tempDir, x + dx, y + dy);
                        if (pathValue.HasValue)
                        {
                            pathValue += 1;
                        }
                        if (pathValue.HasValue && tempDir != direction)
                        {
                            pathValue += 1000;
                        }
                        return pathValue;
                    })
                    .Min();

                dic.Add((direction, x, y), answer);

                return answer;
            }
            else
            {
                throw new Exception($"Unexpected Space \"{curSpace}\"");
            }
        }
    }

    public static long Solution1(string[] fileContents)
    {
        var result = Parse(fileContents);
        return FindCost(result);
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
