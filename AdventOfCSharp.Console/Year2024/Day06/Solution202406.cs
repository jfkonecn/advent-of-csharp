namespace AdventOfCSharp.Console.Year2024.Day06;

public static class Solution202406
{
    private enum Point
    {
        Unknown,
        Empty,
        Occupied,
    }

    private enum Direction
    {
        Unknown,
        Up,
        Down,
        Left,
        Right,
    }

    private static (Point[,], (int x, int y)) Parse(string[] fileContents)
    {
        var arr = new Point[fileContents.Length, fileContents[0].Length];

        int x = -1;
        int y = -1;

        for (int i = 0; i < fileContents.Length; i++)
        {
            for (int j = 0; j < fileContents[i].Length; j++)
            {
                var c = fileContents[i][j];

                if (c == '^')
                {
                    if (x >= 0 || y >= 0)
                    {
                        throw new Exception(
                            $"There is already a starting point at ({x}, {y}), but ({i}, {j}) is a starting point too."
                        );
                    }
                    x = i;
                    y = j;
                }
                else
                {
                    arr[i, j] = c switch
                    {
                        '#' => Point.Occupied,
                        '.' => Point.Empty,
                        _ => throw new Exception($"Unknown character '{c}'"),
                    };
                }
            }
        }

        if (x < 0 || y < 0)
        {
            throw new Exception("No starting point found!");
        }

        return (arr, (x, y));
    }

    public static int Solution1(string[] fileContents)
    {
        var (points, (i, j)) = Parse(fileContents);
        var direction = Direction.Up;
        var visited = new HashSet<(int i, int j)>();
        while (points.GetLength(0) > i && points.GetLength(1) > j && i >= 0 && j >= 0)
        {
            visited.Add((i, j));
            if (direction == Direction.Up && i > 0 && points[i - 1, j] == Point.Occupied)
            {
                direction = Direction.Right;
            }
            else if (
                direction == Direction.Right
                && points.GetLength(1) > j + 1
                && points[i, j + 1] == Point.Occupied
            )
            {
                direction = Direction.Down;
            }
            else if (
                direction == Direction.Down
                && points.GetLength(0) > i + 1
                && points[i + 1, j] == Point.Occupied
            )
            {
                direction = Direction.Left;
            }
            else if (direction == Direction.Left && j > 0 && points[i, j - 1] == Point.Occupied)
            {
                direction = Direction.Up;
            }
            else if (direction == Direction.Up)
            {
                i--;
            }
            else if (direction == Direction.Right)
            {
                j++;
            }
            else if (direction == Direction.Down)
            {
                i++;
            }
            else if (direction == Direction.Left)
            {
                j--;
            }
        }
        return visited.Count;
    }

    public static int Solution2(string[] fileContents)
    {
        throw new NotImplementedException();
    }
}
