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
            var prize = machine.Prize;
            var aButton = machine.AButton;
            var bButton = machine.BButton;
            var bTimeNumerator = (prize.Y * aButton.X - prize.X * aButton.Y);
            var bTimeDenominator = (bButton.Y * aButton.X - bButton.X * aButton.Y);
            var bTimesRemainder = bTimeNumerator % bTimeDenominator;

            if (bTimesRemainder == 0)
            {
                var bTimes = bTimeNumerator / bTimeDenominator;
                var aTimeNumerator = (prize.X - bButton.X * bTimes);
                var aTimeDenominator = aButton.X;
                var aTimesRemainder = aTimeNumerator % aTimeDenominator;
                if (aTimesRemainder == 0)
                {
                    var aTimes = aTimeNumerator / aTimeDenominator;
                    totalCost += bTimes + aTimes * 3;
                }
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
