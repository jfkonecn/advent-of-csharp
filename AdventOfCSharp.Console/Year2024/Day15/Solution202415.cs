namespace AdventOfCSharp.Console.Year2024.Day15;

public static class Solution202415
{
    private enum Item
    {
        Unknown,
        Empty,
        Box,
        LeftBox,
        RightBox,
        Wall,
    }

    private enum Move
    {
        Unknown,
        Up,
        Down,
        Left,
        Right,
    }

    private record ParseResult
    {
        public required Item[,] Warehouse { get; init; }
        public required List<Move> Moves { get; init; }
        public required (int x, int y) RobotStart { get; init; }
    }

    private static ParseResult Parse(string[] fileContents)
    {
        var temp = new List<string>();
        int i = 0;
        while (!string.IsNullOrWhiteSpace(fileContents[i]))
        {
            temp.Add(fileContents[i]);
            i++;
        }

        var items = new Item[temp.Count, fileContents[0].Length];

        int? robotY = null;
        int? robotX = null;

        for (int y = 0; y < items.GetLength(0); y++)
        {
            for (int x = 0; x < items.GetLength(1); x++)
            {
                var c = fileContents[y][x];

                if (c == '@')
                {
                    robotY = y;
                    robotX = x;
                    items[y, x] = Item.Empty;
                }
                else
                {
                    items[y, x] = c switch
                    {
                        '#' => Item.Wall,
                        '.' => Item.Empty,
                        'O' => Item.Box,
                        _ => throw new Exception(
                            $"Item: {fileContents[i]} is an unknown character"
                        ),
                    };
                }
            }
        }

        if (!robotY.HasValue || !robotX.HasValue)
        {
            throw new Exception("Could not find the starting location of the robot");
        }

        i++;
        var moves = new List<Move>();
        for (; i < fileContents.Length; i++)
        {
            foreach (var c in fileContents[i])
            {
                var move = c switch
                {
                    '^' => Move.Up,
                    '>' => Move.Right,
                    'v' => Move.Down,
                    '<' => Move.Left,
                    _ => throw new Exception($"Move: {fileContents[i]} is an unknown character"),
                };
                moves.Add(move);
            }
        }

        return new()
        {
            Moves = moves,
            Warehouse = items,
            RobotStart = (robotX.Value, robotY.Value),
        };
    }

    private static void PrintWarehouse(
        Item[,] warehouse,
        int RobotX,
        int RobotY,
        bool solution2 = true
    )
    {
        System.Console.Write(' ');
        for (long x = 0; x < warehouse.GetLength(1); x++)
        {
            System.Console.Write((x % 10));
        }
        System.Console.WriteLine();
        for (long y = 0; y < warehouse.GetLength(0); y++)
        {
            System.Console.Write(y % 10);
            for (long x = 0; x < warehouse.GetLength(1); x++)
            {
                var c = warehouse[y, x] switch
                {
                    Item.LeftBox
                    or Item.RightBox
                    or Item.RightBox when y == RobotY && x == RobotX => 'X',
                    Item.Box => 'O',
                    Item.Wall => '#',
                    Item.Empty when y == RobotY && x == RobotX => '@',
                    Item.LeftBox => '[',
                    Item.RightBox => ']',
                    Item.Empty => '.',
                    _ => '?',
                };
                System.Console.Write(c);
            }
            System.Console.WriteLine();
        }
        System.Console.WriteLine();
    }

    public static long Solution1(string[] fileContents)
    {
        var result = Parse(fileContents);

        var warehouse = result.Warehouse;
        var (robotX, robotY) = result.RobotStart;
        foreach (var move in result.Moves)
        {
            var dy = move switch
            {
                Move.Up => -1,
                Move.Down => 1,
                _ => 0,
            };
            var dx = move switch
            {
                Move.Left => -1,
                Move.Right => 1,
                _ => 0,
            };

            bool canMove = true;

            var tempY = robotY + dy;
            var tempX = robotX + dx;
            int boxesToPush = 0;

            while (warehouse[tempY, tempX] != Item.Empty)
            {
                var item = warehouse[tempY, tempX];
                if (item == Item.Wall)
                {
                    canMove = false;
                    break;
                }
                else if (item == Item.Box)
                {
                    tempY += dy;
                    tempX += dx;
                    boxesToPush++;
                }
                else
                {
                    throw new Exception($"{item} should be either a Box or Wall");
                }
            }

            if (canMove)
            {
                robotY += dy;
                robotX += dx;

                warehouse[robotY, robotX] = Item.Empty;

                var y = robotY + dy;
                var x = robotX + dx;

                for (int i = 0; i < boxesToPush; i++)
                {
                    warehouse[y, x] = Item.Box;
                    y += dy;
                    x += dx;
                }
            }
        }

        long sum = 0;
        for (long y = 0; y < warehouse.GetLength(0); y++)
        {
            for (long x = 0; x < warehouse.GetLength(1); x++)
            {
                if (warehouse[y, x] == Item.Box)
                {
                    sum += (100 * y) + x;
                }
            }
        }
        return sum;
    }

    public static long Solution2(string[] fileContents)
    {
        var result = Parse(fileContents);

        var oldWarehouse = result.Warehouse;
        var warehouse = new Item[oldWarehouse.GetLength(0), oldWarehouse.GetLength(1) * 2];
        var previousWarehouse = new Item[oldWarehouse.GetLength(0), oldWarehouse.GetLength(1) * 2];

        var (robotX, robotY) = result.RobotStart;
        robotX *= 2;
        void Print(Move move)
        {
            //System.Console.WriteLine($"Robot at ({robotX}, {robotY})");
            //System.Console.WriteLine($"Move {move}:");
            //PrintWarehouse(warehouse, robotX, robotY);
        }
        for (int y = 0; y < oldWarehouse.GetLength(0); y++)
        {
            for (int x = 0; x < oldWarehouse.GetLength(1); x++)
            {
                var item = oldWarehouse[y, x];
                int curX = x * 2;
                if (item == Item.Box)
                {
                    warehouse[y, curX] = Item.LeftBox;
                    warehouse[y, curX + 1] = Item.RightBox;
                }
                else
                {
                    warehouse[y, curX] = item;
                    warehouse[y, curX + 1] = item;
                }
            }
        }
        BackupWarehouse();
        Print(Move.Unknown);
        var coordinatesToMove = new List<(int x, int y)>();
        foreach (var move in result.Moves)
        {
            var dy = move switch
            {
                Move.Up => -1,
                Move.Down => 1,
                _ => 0,
            };
            var dx = move switch
            {
                Move.Left => -2,
                Move.Right => 2,
                _ => 0,
            };

            int robotDx = dx switch
            {
                2 => 1,
                -2 => -1,
                0 => 0,
                _ => throw new Exception($"Unexpected dx {dx}"),
            };
            var tempY = robotY + dy;
            var tempX = robotX + robotDx;

            bool canMove = true;

            if (dx != 0 && dy == 0)
            {
                while (warehouse[tempY, tempX] != Item.Empty)
                {
                    var item = warehouse[tempY, tempX];
                    if (item == Item.Wall)
                    {
                        coordinatesToMove.Clear();
                        canMove = false;
                        break;
                    }
                    else if (item == Item.LeftBox || item == Item.RightBox)
                    {
                        coordinatesToMove.Add((tempX, tempY));
                        coordinatesToMove.Add((tempX + robotDx, tempY));
                        tempY += dy;
                        tempX += dx;
                    }
                    else
                    {
                        throw new Exception(
                            $"Pushing {item} should be either a Left Box, Right Box, or Wall"
                        );
                    }
                }
            }
            else if (dx == 0 && dy != 0)
            {
                var visited = new HashSet<(int x, int y)>();
                var s = new Stack<(int x, int y)>();
                if (warehouse[tempY, tempX] != Item.Empty)
                {
                    s.Push((tempX, tempY));
                }
                while (s.TryPop(out var popValue))
                {
                    var (curX, curY) = popValue;
                    visited.Add((curX, curY));
                    var item = warehouse[curY, curX];
                    if (item == Item.Wall)
                    {
                        coordinatesToMove.Clear();
                        canMove = false;
                        break;
                    }
                    if (
                        warehouse[curY + dy, curX + dx] != Item.Empty
                        && !visited.Contains((curX + dx, curY + dy))
                    )
                    {
                        s.Push((curX + dx, curY + dy));
                    }

                    if (item == Item.RightBox)
                    {
                        if (!visited.Contains((curX - 1, curY)))
                        {
                            s.Push((curX - 1, curY));
                        }
                    }
                    else if (item == Item.LeftBox)
                    {
                        if (!visited.Contains((curX + 1, curY)))
                        {
                            s.Push((curX + 1, curY));
                        }
                    }
                    else if (item != Item.Empty)
                    {
                        throw new Exception(
                            $"Pushing {item} should be either a Left Box, Right Box, Empty, or Wall"
                        );
                    }
                    coordinatesToMove.Add((curX, curY));
                }
            }
            else
            {
                throw new Exception($"dx {dx} dy {dy} one must be zero and other not");
            }

            if (canMove)
            {
                robotX += robotDx;
                robotY += dy;
            }
            foreach (var (oldX, oldY) in coordinatesToMove)
            {
                int newX = oldX + robotDx;
                int newY = oldY + dy;

                warehouse[oldY, oldX] = Item.Empty;
            }
            foreach (var (oldX, oldY) in coordinatesToMove)
            {
                int newX = oldX + robotDx;
                int newY = oldY + dy;

                warehouse[newY, newX] = previousWarehouse[oldY, oldX];
            }
            coordinatesToMove.Clear();
            Print(move);
            BackupWarehouse();
        }

        long sum = 0;
        for (long y = 0; y < warehouse.GetLength(0); y++)
        {
            for (long x = 0; x < warehouse.GetLength(1); x++)
            {
                if (warehouse[y, x] == Item.LeftBox)
                {
                    sum += (100 * y) + x;
                }
            }
        }
        return sum;

        void BackupWarehouse()
        {
            for (int y = 0; y < previousWarehouse.GetLength(0); y++)
            {
                for (int x = 0; x < previousWarehouse.GetLength(1); x++)
                {
                    previousWarehouse[y, x] = warehouse[y, x];
                }
            }
        }
    }
}
