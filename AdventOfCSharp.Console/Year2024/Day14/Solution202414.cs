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

    private enum Quadrant
    {
        Unknown,
        UpperLeft,
        UpperRight,
        BottomLeft,
        BottomRight,
    }

    public static int Solution1(string[] fileContents, int width, int height)
    {
        var robots = Parse(fileContents);
        for (int i = 0; i < 100; i++)
        {
            foreach (var robot in robots)
            {
                robot.Position.X += robot.Velocity.X;
                robot.Position.Y += robot.Velocity.Y;
                if (robot.Position.X < 0)
                {
                    robot.Position.X += width;
                }
                else if (robot.Position.X >= width)
                {
                    robot.Position.X -= width;
                }

                if (robot.Position.Y < 0)
                {
                    robot.Position.Y += height;
                }
                else if (robot.Position.Y >= height)
                {
                    robot.Position.Y -= height;
                }
            }
        }

        var halfX = width / 2;
        var halfY = height / 2;
        return robots
            .GroupBy(x => (x.Position.X, x.Position.Y))
            .Select(x =>
            {
                if (x.Key.X < halfX && x.Key.Y < halfY)
                {
                    return (Quadrant.UpperLeft, x.Count());
                }
                else if (x.Key.X > halfX && x.Key.Y < halfY)
                {
                    return (Quadrant.UpperRight, x.Count());
                }
                else if (x.Key.X < halfX && x.Key.Y > halfY)
                {
                    return (Quadrant.BottomLeft, x.Count());
                }
                else if (x.Key.X > halfX && x.Key.Y > halfY)
                {
                    return (Quadrant.BottomRight, x.Count());
                }
                else
                {
                    return (Quadrant.Unknown, 0);
                }
            })
            .GroupBy(x => x.Item1)
            .Where(x => x.Key != Quadrant.Unknown)
            .Select(x => x.Select(y => y.Item2).Sum())
            .Aggregate(1, (pre, cur) => pre * cur);
    }

    public static int Solution2(string[] fileContents, int width, int height, int? renders)
    {
        var robots = Parse(fileContents);
        var picture = new int[width, height];
        foreach (var robot in robots)
        {
            picture[robot.Position.X, robot.Position.Y]++;
        }
        for (int i = 0; renders != null ? i < renders : true; i++)
        {
            foreach (var robot in robots)
            {
                picture[robot.Position.X, robot.Position.Y]--;
                robot.Position.X += robot.Velocity.X;
                robot.Position.Y += robot.Velocity.Y;
                if (robot.Position.X < 0)
                {
                    robot.Position.X += width;
                }
                else if (robot.Position.X >= width)
                {
                    robot.Position.X -= width;
                }

                if (robot.Position.Y < 0)
                {
                    robot.Position.Y += height;
                }
                else if (robot.Position.Y >= height)
                {
                    robot.Position.Y -= height;
                }
                picture[robot.Position.X, robot.Position.Y]++;
            }

            System.Console.WriteLine("******");
            for (int y = 0; y < picture.GetLength(1); y++)
            {
                for (int x = 0; x < picture.GetLength(0); x++)
                {
                    var num = picture[x, y];
                    if (num == 0)
                    {
                        System.Console.Write('.');
                    }
                    else
                    {
                        System.Console.Write(picture[x, y]);
                    }
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("******");
            System.Console.WriteLine($"Render {i + 1}");
        }
        return 0;
    }
}
