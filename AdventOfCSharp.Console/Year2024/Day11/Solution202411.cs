namespace AdventOfCSharp.Console.Year2024.Day11;

public static class Solution202411
{
    private static List<long> Parse(string[] fileContents)
    {
        return fileContents.Single().Split(' ').Select(long.Parse).ToList();
    }

    private static int Process(string[] fileContents, int maxBlinks)
    {
        var stones = Parse(fileContents);

        int total = 0;
        var cache = new Dictionary<(int blinks, long value), int>();
        foreach (var stone in stones)
        {
            total += ProcessRec(0, stone);
        }
        return total;

        int ProcessRec(int blinks, long cur)
        {
            if (blinks == maxBlinks)
            {
                return 1;
            }
            else if (cache.TryGetValue((blinks, cur), out var cached))
            {
                return cached;
            }
            int answer = 0;
            var numStr = cur.ToString();
            if (cur == 0)
            {
                answer += ProcessRec(blinks + 1, 1);
            }
            else if (numStr.Length % 2 == 0)
            {
                var length = numStr.Length / 2;
                var first = long.Parse(new string(numStr.Take(length).ToArray()));
                var second = long.Parse(new string(numStr.Skip(length).ToArray()));

                answer += ProcessRec(blinks + 1, first);
                answer += ProcessRec(blinks + 1, second);
            }
            else
            {
                answer += ProcessRec(blinks + 1, cur * 2024);
            }

            cache.Add((blinks, cur), answer);

            return answer;
        }
    }

    public static int Solution1(string[] fileContents, int blinks)
    {
        return Process(fileContents, blinks);
    }

    public static int Solution2(string[] fileContents, int blinks)
    {
        return Process(fileContents, blinks);
    }
}
