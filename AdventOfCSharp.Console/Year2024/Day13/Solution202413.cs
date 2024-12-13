namespace AdventOfCSharp.Console.Year2024.Day13;

public static class Solution202413
{
    private record Point
    {
        public required long X { get; init; }
        public required long Y { get; init; }
    }

    private record Machine
    {
        public required Point Prize { get; init; }
        public required Point AButton { get; init; }
        public required Point BButton { get; init; }
    }

    private static Machine[] Parse(string[] fileContents)
    {
        var machines = new List<Machine>();

        for (int i = 0; i < fileContents.Length; i++)
        {
            var aButton = ParseButton();
            i++;
            var bButton = ParseButton();
            i++;
            var prizeArr = fileContents[i].Split(":")[1].Trim().Split(",");
            var prize = new Point()
            {
                X = long.Parse(prizeArr[0].Replace("X=", "").Trim()),
                Y = long.Parse(prizeArr[1].Replace("Y=", "").Trim()),
            };
            i++;
            machines.Add(
                new Machine()
                {
                    Prize = prize,
                    AButton = aButton,
                    BButton = bButton,
                }
            );
            Point ParseButton()
            {
                var pointArr = fileContents[i].Split(":")[1].Trim().Split(",");
                return new Point()
                {
                    X = long.Parse(pointArr[0].Replace("X", "").Trim()),
                    Y = long.Parse(pointArr[1].Replace("Y", "").Trim()),
                };
            }
        }

        return machines.ToArray();
    }

    public static long Solution1(string[] fileContents)
    {
        var machines = Parse(fileContents);
        long totalCost = 0;

        foreach (var machine in machines)
        {
            var cache = new Dictionary<(long x, long y, int aPresses, int bPresses), long?>();

            var cost = GetCost(0, 0, 0, 0);
            totalCost += cost ?? 0;

            long? GetCost(long x, long y, int aPresses, int bPresses)
            {
                if (cache.TryGetValue((x, y, aPresses, bPresses), out var cacheValue))
                {
                    return cacheValue;
                }
                else if (machine.Prize.X == x && machine.Prize.Y == y)
                {
                    return 0;
                }
                else if (aPresses > 100 || bPresses > 100)
                {
                    return null;
                }

                var aCost =
                    3
                    + GetCost(x + machine.AButton.X, y + machine.AButton.Y, aPresses + 1, bPresses);
                var bCost =
                    1
                    + GetCost(x + machine.BButton.X, y + machine.BButton.Y, aPresses, bPresses + 1);
                long? answer = null;
                if (aCost.HasValue && bCost.HasValue)
                {
                    answer = Math.Min(aCost.Value, bCost.Value);
                }
                else if (aCost.HasValue)
                {
                    answer = aCost;
                }
                else
                {
                    answer = bCost;
                }
                cache.Add((x, y, aPresses, bPresses), answer);
                return answer;
            }
        }

        return totalCost;
    }

    public static int Solution2(string[] fileContents)
    {
        throw new NotImplementedException();
    }
}
