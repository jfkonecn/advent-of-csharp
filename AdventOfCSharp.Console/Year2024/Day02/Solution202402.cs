namespace AdventOfCSharp.Console.Year2024.Day02;

public static class Solution202402
{
    private static int[][] Parse(string[] fileContents)
    {
        return fileContents
            .Select(line =>
            {
                return line.Split(' ').Select(int.Parse).ToArray();
            })
            .ToArray();
    }

    public static int Solution1(string[] fileContents)
    {
        return Parse(fileContents)
            .Where(arr =>
            {
                var isIncreasing = arr[1] - arr[0] >= 0;
                for (int i = 1; i < arr.Length; i++)
                {
                    var diff = arr[i] - arr[i - 1];
                    var curIsIncreasing = diff >= 0;
                    if (isIncreasing != curIsIncreasing || Math.Abs(diff) > 3 || diff == 0)
                    {
                        return false;
                    }
                }
                return true;
            })
            .Count();
    }

    public static int Solution2(string[] fileContents)
    {
        throw new NotImplementedException();
    }
}
