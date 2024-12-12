using System.Numerics;

namespace AdventOfCSharp.Console.Year2024.Day11;

public static class Solution202411
{
    private static List<long> Parse(string[] fileContents)
    {
        return fileContents.Single().Split(' ').Select(long.Parse).ToList();
    }

    private static long Process(string[] fileContents, int maxBlinks)
    {
        var stones = Parse(fileContents);

        long total = 0;
        var cache = new Dictionary<(int blinks, BigInteger value), long>();
        foreach (var stone in stones)
        {
            total += ProcessRec(0, stone);
        }
        return total;

        long ProcessRec(int blinks, BigInteger cur)
        {
            if (blinks == maxBlinks)
            {
                return 1;
            }
            else if (cache.TryGetValue((blinks, cur), out var cached))
            {
                return cached;
            }
            long answer = 0;
            var numStr = cur.ToString();
            if (cur == 0)
            {
                answer += ProcessRec(blinks + 1, 1);
            }
            else if (numStr.Length % 2 == 0)
            {
                var length = numStr.Length / 2;
                var first = BigInteger.Parse(new string(numStr.Take(length).ToArray()));
                var second = BigInteger.Parse(new string(numStr.Skip(length).ToArray()));

                answer += ProcessRec(blinks + 1, first);
                if (answer < 1)
                {
                    throw new Exception("less than one");
                }
                answer += ProcessRec(blinks + 1, second);
            }
            else
            {
                answer += ProcessRec(blinks + 1, cur * (BigInteger)2024);
            }

            if (answer < 1)
            {
                throw new Exception("less than one");
            }
            cache.Add((blinks, cur), answer);

            return answer;
        }
    }

    public static long Solution1(string[] fileContents, int blinks)
    {
        return Process(fileContents, blinks);
    }

    public static long Solution2(string[] fileContents, int blinks)
    {
        return Process(fileContents, blinks);
    }
}
