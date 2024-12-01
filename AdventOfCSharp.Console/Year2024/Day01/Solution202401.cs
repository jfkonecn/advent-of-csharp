namespace AdventOfCSharp.Console.Year2024.Day01;

public static class Solution202401
{
    public static int Solution1(string[] fileContents)
    {
        var (leftArr, rightArr) = Parse(fileContents);
        int sum = 0;
        for (int i = 0; i < fileContents.Length; i++)
        {
            sum += Math.Abs(leftArr[i] - rightArr[i]);
        }
        return sum;
    }

    public static int Solution2(string[] fileContents)
    {
        var (leftArr, rightArr) = Parse(fileContents);
        var lookup = rightArr.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

        int sum = 0;
        for (int i = 0; i < fileContents.Length; i++)
        {
            var num = leftArr[i];
            if (lookup.TryGetValue(num, out var frequency))
            {
                sum += Math.Abs(num * frequency);
            }
        }
        return sum;
    }

    private static (int[] leftArr, int[] rightArr) Parse(string[] fileContents)
    {
        var leftArr = new int[fileContents.Length];
        var rightArr = new int[fileContents.Length];
        for (int i = 0; i < fileContents.Length; i++)
        {
            var split = fileContents[i].Split("   ");
            leftArr[i] = int.Parse(split[0]);
            rightArr[i] = int.Parse(split[1]);
        }
        Array.Sort(leftArr);
        Array.Sort(rightArr);
        return (leftArr, rightArr);
    }
}
