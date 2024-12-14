namespace AdventOfCSharp.Console.Year2024.Day14;

public static class Solution202414
{
    private record Position
    {
        public required int X { get; set; }
        public required int Y { get; set; }
    }

    private record Velocity
    {
        public required int X { get; set; }
        public required int Y { get; set; }
    }

    private record Robot
    {
        public required Position Position { get; init; }
        public required Velocity Velocity { get; init; }
    }

    private static Robot[] Parse(string[] fileContents)
    {
        var robots = new Robot[fileContents.Length];

        for (int i = 0; i < fileContents.Length; i++)
        {
            var split = fileContents[i].Split(" ");
            var positionSplit = split[0].Replace("p=", "").Split(",");
            var velocitySplit = split[1].Replace("v=", "").Split(",");
            robots[i] = new Robot()
            {
                Position = new Position()
                {
                    X = int.Parse(positionSplit[0]),
                    Y = int.Parse(positionSplit[1]),
                },
                Velocity = new Velocity()
                {
                    X = int.Parse(velocitySplit[0]),
                    Y = int.Parse(velocitySplit[1]),
                },
            };
        }

        return robots;
    }

    public static int Solution1(string[] fileContents)
    {
        var robots = Parse(fileContents);
        throw new NotImplementedException();
    }

    public static int Solution2(string[] fileContents)
    {
        throw new NotImplementedException();
    }
}
