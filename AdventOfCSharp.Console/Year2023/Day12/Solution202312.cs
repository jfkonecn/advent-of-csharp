namespace AdventOfCSharp.Console.Year2023.Day12;

public static class Solution202312
{
    private enum Spring
    {
        Unknown,
        Damaged,
        Operational,
    }

    private record SpringRow
    {
        public required IReadOnlyList<Spring> Springs { get; init; }
        public required IReadOnlyList<uint> Groups { get; init; }
    }

    private static IReadOnlyList<SpringRow> Parse(string[] fileContents)
    {
        var rows = new List<SpringRow>();
        foreach (var row in fileContents)
        {
            var splitRow = row.Split(" ");
            var springsString = splitRow.First();
            var groupString = splitRow.Last();

            var springRow = new SpringRow()
            {
                Springs = springsString
                    .Select(x =>
                    {
                        _ = x;
                        return x switch
                        {
                            '#' => Spring.Damaged,
                            '?' => Spring.Unknown,
                            '.' => Spring.Operational,
                            _ => throw new Exception($"Unknown char '{x}'"),
                        };
                    })
                    .ToList(),
                Groups = groupString.Split(",").Select(uint.Parse).ToList(),
            };

            rows.Add(springRow);
        }
        return rows;
    }

    private static int Calculate(SpringRow row)
    {
        var cache = new Dictionary<(int, int), int>();
        var firstSpring = row.Springs[0];
        if (firstSpring == Spring.Unknown)
        {
            return Calculate(Spring.Damaged, 0, 0) + Calculate(Spring.Operational, 0, 0);
        }
        else
        {
            return Calculate(firstSpring, 0, 0);
        }
        int Calculate(Spring curSpring, int springIdx, int groupIdx)
        {
            if (cache.TryGetValue((springIdx, groupIdx), out var cachedAnswer))
            {
                return cachedAnswer;
            }
            int answer;
            var curGroup = row.Groups[groupIdx];
            var (hasNextSpring, nextSpring) = row.Springs.TryGetValueOrDefault(springIdx + 1);

            if (hasNextSpring)
            {
                answer = (curSpring, nextSpring) switch
                {
                    (_, Spring.Unknown) => HandleSprings(curSpring, Spring.Damaged)
                        + HandleSprings(curSpring, Spring.Operational),
                    _ => HandleSprings(curSpring, nextSpring),
                };
                int HandleSprings(Spring cur, Spring next)
                {
                    return (cur, next) switch
                    {
                        (Spring.Operational, Spring.Operational)
                        or (Spring.Damaged, Spring.Damaged) => Calculate(
                            next,
                            springIdx + 1,
                            groupIdx
                        ),
                        (Spring.Damaged, Spring.Operational)
                        or (Spring.Operational, Spring.Damaged) => HandleGroupChange(next),
                        _ => throw new Exception($"Unreachable ({cur}, {next})"),
                    };
                }
                int HandleGroupChange(Spring next)
                {
                    var nextGroupIdx = groupIdx + 1;
                    if (nextGroupIdx == row.Groups.Count)
                    {
                        return 0;
                    }
                    else
                    {
                        return Calculate(next, springIdx + 1, groupIdx + 1);
                    }
                }
            }
            else if (groupIdx + 1 != row.Groups.Count)
            {
                answer = 0;
            }
            else
            {
                answer = 1;
            }

            cache.Add((springIdx, groupIdx), answer);
            return answer;
        }
    }

    public static int Solution1(string[] fileContents)
    {
        //return Parse(fileContents).Select(Calculate).Sum();
        var temp = Parse(fileContents).Select(Calculate).ToList();
        return temp.Sum();
    }
}
