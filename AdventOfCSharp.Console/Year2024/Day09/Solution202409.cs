namespace AdventOfCSharp.Console.Year2024.Day09;

public static class Solution202409
{
    private static int?[] Parse(string[] fileContents)
    {
        var result = new List<int?>();
        int id = -1;
        var line = fileContents.Single();
        for (int i = 0; i < line.Length; i++)
        {
            int? toAdd = null;
            if (i % 2 == 0)
            {
                id++;
                toAdd = id;
            }
            var total = int.Parse(line[i].ToString());
            for (int j = 0; j < total; j++)
            {
                result.Add(toAdd);
            }
        }
        return result.ToArray();
    }

    public static long Solution1(string[] fileContents)
    {
        var result = Parse(fileContents);
        int endIndex = result.Length - 1;

        for (int i = 0; i < endIndex; i++)
        {
            if (!result[i].HasValue)
            {
                while (!result[endIndex].HasValue)
                {
                    endIndex--;
                }
                result[i] = result[endIndex];
                result[endIndex] = null;
            }
        }
        return result.Select((x, idx) => x.HasValue ? ((long)x.Value) * idx : 0).Sum();
    }

    public static long Solution2(string[] fileContents)
    {
        throw new NotImplementedException();
    }
}
