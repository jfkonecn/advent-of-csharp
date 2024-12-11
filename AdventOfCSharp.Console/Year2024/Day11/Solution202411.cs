namespace AdventOfCSharp.Console.Year2024.Day11;

public static class Solution202411
{
    private static List<long> Parse(string[] fileContents)
    {
        return fileContents.Single().Split(' ').Select(long.Parse).ToList();
    }

    public static int Solution1(string[] fileContents, int blinks)
    {
        var stones = Parse(fileContents);
        for (int i = 0; i < blinks; i++)
        {
            var newStones = new List<long>();
            for (int j = 0; j < stones.Count; j++)
            {
                var cur = stones[j];
                var numStr = cur.ToString();
                if (cur == 0)
                {
                    newStones.Add(1);
                }
                else if (numStr.Length % 2 == 0)
                {
                    var length = numStr.Length / 2;
                    var first = long.Parse(new string(numStr.Take(length).ToArray()));
                    var second = long.Parse(new string(numStr.Skip(length).ToArray()));
                    newStones.Add(first);
                    newStones.Add(second);
                }
                else
                {
                    newStones.Add(cur * 2024);
                }
            }
            stones = newStones;
        }
        return stones.Count;
    }

    public static int Solution2(string[] fileContents)
    {
        throw new NotImplementedException();
    }
}
