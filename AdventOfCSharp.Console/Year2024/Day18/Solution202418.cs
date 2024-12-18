namespace AdventOfCSharp.Console.Year2024.Day18;

public static class Solution202418
{
    private static (int x, int y)[] Parse(string[] fileContents)
    {
        var bytes = new (int x, int y)[fileContents.Length];
        for (int i = 0; i < fileContents.Length; i++)
        {
            var split = fileContents[i].Split(",");
            bytes[i] = (int.Parse(split[0]), int.Parse(split[1]));
        }
        return bytes;
    }

    private enum Space
    {
        Safe,
        Corrupt,
    }

    public static int Solution1(string[] fileContents, int? totalBytes, int gridSize)
    {
        var bytes = Parse(fileContents);
        return FindPath(totalBytes, gridSize, bytes);
    }

    private static int FindPath(int? totalBytes, int gridSize, (int x, int y)[] bytes)
    {
        int simulateBytes = totalBytes ?? bytes.Length;
        var grid = new Space[gridSize, gridSize];

        foreach (var (x, y) in bytes.Take(simulateBytes))
        {
            grid[y, x] = Space.Corrupt;
        }

        var visited = new HashSet<(int x, int y)>();
        var queue = new Queue<(int x, int y, int distance)>();
        queue.Enqueue((0, 0, 0));

        var deltas = new (int dx, int dy)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };

        while (queue.Count > 0)
        {
            var (x, y, distance) = queue.Dequeue();

            if (x == gridSize - 1 && y == gridSize - 1)
            {
                return distance;
            }

            if (visited.Contains((x, y)))
            {
                continue;
            }
            visited.Add((x, y));

            foreach (var (dx, dy) in deltas)
            {
                var nx = x + dx;
                var ny = y + dy;

                if (
                    nx >= 0
                    && nx < gridSize
                    && ny >= 0
                    && ny < gridSize
                    && grid[ny, nx] != Space.Corrupt
                    && !visited.Contains((nx, ny))
                )
                {
                    queue.Enqueue((nx, ny, distance + 1));
                }
            }
        }

        return int.MinValue;
    }

    public static string Solution2(string[] fileContents, int? totalBytes, int gridSize)
    {
        var bytes = Parse(fileContents);
        int step = bytes.Length / 2;
        int index = bytes.Length / 2;
        int maxStepSize = 10;
        while (true)
        {
            int value = FindPathLocal(index);

            index = value == int.MaxValue ? index - step : index + step;

            if (step < maxStepSize)
            {
                break;
            }
            step /= 2;
        }

        var end = index - maxStepSize;
        if (end >= bytes.Length || end < 0)
        {
            end = bytes.Length - 1;
        }

        for (int i = end; i > 0; i--)
        {
            var (x, y) = bytes[i];
            var path = FindPathLocal(i);
            if (path > 0)
            {
                return $"{x},{y}";
            }
        }
        throw new Exception("No Point Found");
        int FindPathLocal(int total)
        {
            return FindPath(total, gridSize, bytes);
        }
    }
}
