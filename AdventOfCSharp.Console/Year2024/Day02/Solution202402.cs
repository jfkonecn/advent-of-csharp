namespace AdventOfCSharp.Console.Year2024.Day02;

public static class Solution202402
{
    private static List<List<int>> Parse(string[] fileContents)
    {
        return fileContents
            .Select(line =>
            {
                return line.Split(' ').Select(int.Parse).ToList();
            })
            .ToList();
    }

    private static bool IsSafe(List<int> arr)
    {
        var isIncreasing = arr[1] - arr[0] >= 0;
        for (int i = 1; i < arr.Count; i++)
        {
            var diff = arr[i] - arr[i - 1];
            var curIsIncreasing = diff >= 0;
            if (isIncreasing != curIsIncreasing || Math.Abs(diff) > 3 || diff == 0)
            {
                return false;
            }
        }
        return true;
    }

    public static int Solution1(string[] fileContents)
    {
        return Parse(fileContents).Where(IsSafe).Count();
    }

    public static int Solution2(string[] fileContents)
    {
        return Parse(fileContents)
            .Select(arr =>
            {
                var toTest = new List<List<int>> { arr.ToList() };
                for (int i = 0; i < arr.Count; i++)
                {
                    var temp = arr.ToList();
                    temp.RemoveAt(i);
                    toTest.Add(temp);
                }
                return toTest;
            })
            .Where(x =>
            {
                return x.Where(IsSafe).Any();
            })
            .Count();
    }
}
