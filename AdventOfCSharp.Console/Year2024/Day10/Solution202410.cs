namespace AdventOfCSharp.Console.Year2024.Day10;

public static class Solution202410
{
    private static (List<(int x, int y)> trailHeads, int[,] map) Parse(string[] fileContents)
    {
        var arr = new int[fileContents.Length, fileContents[0].Length];
        var trailHeads = new List<(int x, int y)>();
        for (int i = 0; i < fileContents.Length; i++)
        {
            for (int j = 0; j < fileContents[i].Length; j++)
            {
                var num = int.Parse(fileContents[i][j].ToString());
                arr[i, j] = num;
                if (num == 0)
                {
                    trailHeads.Add((i, j));
                }
            }
        }
        return (trailHeads, arr);
    }

    private static int TotalPaths(List<(int x, int y)> trailHeads, int[,] map)
    {
        var nines = new HashSet<(int x, int y)>();

        foreach (var (startX, startY) in trailHeads)
        {
            var stack = new Stack<(int x, int y)>();
            var visited = new HashSet<(int x, int y)>();
            stack.Push((startX, startY));
            while (stack.TryPop(out var result))
            {
                var (curX, curY) = result;
                //System.Console.WriteLine($"Visiting ({curX}, {curY})");
                visited.Add((curX, curY));
                var curNum = map[curX, curY];
                if (curNum == 9)
                {
                    nines.Add((curX, curY));
                }
                else
                {
                    HandlePoint(curX - 1, curY, curNum);
                    HandlePoint(curX + 1, curY, curNum);
                    HandlePoint(curX, curY - 1, curNum);
                    HandlePoint(curX, curY + 1, curNum);
                }
            }

            void HandlePoint(int x, int y, int curNum)
            {
                if (x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1))
                {
                    var num = map[x, y];

                    if (curNum + 1 == num)
                    {
                        stack.Push((x, y));
                    }
                }
            }
        }

        return nines.Count;
    }

    public static int Solution1(string[] fileContents)
    {
        var (trailHeads, map) = Parse(fileContents);
        return TotalPaths(trailHeads, map);
    }

    public static int Solution2(string[] fileContents)
    {
        throw new NotImplementedException();
    }
}
