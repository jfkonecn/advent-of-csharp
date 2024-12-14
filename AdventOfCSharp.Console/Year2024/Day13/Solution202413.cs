namespace AdventOfCSharp.Console.Year2024.Day13;

public static class Solution202413
{
    private record Point
    {
        public required long X { get; set; }
        public required long Y { get; set; }
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
        return Calculate(machines);
    }

    private static long Calculate(Machine[] machines)
    {
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
                long xAPush = x + machine.AButton.X;
                long yAPush = y + machine.AButton.Y;
                long xBPush = x + machine.BButton.X;
                long yBPush = y + machine.BButton.Y;

                long? aCost =
                    xAPush > machine.Prize.X || yAPush > machine.Prize.Y
                        ? null
                        : 3 + GetCost(xAPush, yAPush, aPresses + 1, bPresses);
                long? bCost =
                    xBPush > machine.Prize.X || yBPush > machine.Prize.Y
                        ? null
                        : 1 + GetCost(xBPush, yBPush, aPresses, bPresses + 1);
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

    public static long Solution2(string[] fileContents)
    {
        var machines = Parse(fileContents);
        foreach (var machine in machines)
        {
            machine.Prize.X += 10000000000000;
            machine.Prize.Y += 10000000000000;
        }
        return Calculate(machines);
    }
}
