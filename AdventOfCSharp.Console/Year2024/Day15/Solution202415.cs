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
        for (long y = 0; y < warehouse.GetLength(0); y++)
        {
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

        var (robotX, robotY) = result.RobotStart;
        robotX *= 2;
        void Print(Move move)
        {
            System.Console.WriteLine($"Move {move}:");
            PrintWarehouse(warehouse, robotX, robotY);
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
        Print(Move.Unknown);
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
                else if (item == Item.LeftBox || item == Item.RightBox)
                {
                    tempY += dy;
                    tempX += dx;
                    if (dy != 0)
                    {
                        if (item == Item.RightBox && warehouse[tempY, tempX - 1] == Item.Wall)
                        {
                            canMove = false;
                            break;
                        }
                        else if (item == Item.LeftBox && warehouse[tempY, tempX + 1] == Item.Wall)
                        {
                            canMove = false;
                            break;
                        }
                    }
                    boxesToPush++;
                }
                else
                {
                    throw new Exception(
                        $"Pushing {item} should be either a Left Box, Right Box, or Wall"
                    );
                }
            }

            if (canMove)
            {
                robotY += dy;
                var robotDx = dx;
                if (dx != 0)
                {
                    robotDx = dx switch
                    {
                        -2 => -1,
                        2 => 1,
                        _ => throw new Exception($"Unexpected dx: {dx}"),
                    };
                }
                robotX += robotDx;

                var preItem = warehouse[robotY, robotX];

                warehouse[robotY, robotX] = Item.Empty;

                var y = robotY + dy;
                var x = robotX + robotDx;

                for (int i = 0; i < boxesToPush; i++)
                {
                    var curItem = warehouse[y, x];
                    warehouse[y, x] = preItem;
                    if (dx == 0 && dy != 0)
                    {
                        if (preItem == Item.RightBox)
                        {
                            warehouse[y + dy, x - 1] = Item.Empty;
                            warehouse[y, x - 1] = Item.LeftBox;
                        }
                        else if (preItem == Item.LeftBox)
                        {
                            warehouse[y + dy, x + 1] = Item.Empty;
                            warehouse[y, x + 1] = Item.RightBox;
                        }
                        else
                        {
                            throw new Exception(
                                $"PreItem: {preItem} should be either a Left Box or Right Box"
                            );
                        }
                        preItem = curItem;
                    }
                    else if (dx != 0 && dy == 0)
                    {
                        int newX = dx switch
                        {
                            -2 => x - 1,
                            2 => x + 1,
                            _ => throw new Exception($"Unexpected dx: {dx}"),
                        };
                        var temp = warehouse[y, newX];
                        warehouse[y, newX] = preItem switch
                        {
                            Item.RightBox when dx == -2 => Item.LeftBox,
                            Item.LeftBox when dx == 2 => Item.RightBox,
                            _ => throw new Exception($"Unexpected preItem dx combo {dx} {preItem}"),
                        };
                        preItem = temp;
                    }
                    else
                    {
                        throw new Exception(
                            $"dx {dx} or dy: {dy} only one should be zero and only one should be not zero"
                        );
                    }

                    y += dy;
                    x += dx;
                }
            }
            Print(move);
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
    }
}
