namespace AdventOfCSharp.Console.Year2024.Day07;

public static class Solution202407
{
    private record CalibrationEquation
    {
        public required long Total { get; init; }
        public required long[] Numbers { get; init; }
    }

    private static CalibrationEquation[] Parse(string[] fileContents)
    {
        return fileContents
            .Select(x =>
            {
                var split = x.Split(": ");
                return new CalibrationEquation()
                {
                    Total = long.Parse(split[0]),
                    Numbers = split[1].Split(" ").Select(long.Parse).ToArray(),
                };
            })
            .ToArray();
    }

    private enum Operation
    {
        Unknown,
        Add,
        Multiply,
    }

    private static bool IsValid(CalibrationEquation equation)
    {
        var dic = new Dictionary<(long runningTotal, int index), bool>();
        return IsValidRec(equation.Numbers[0], 1);
        bool IsValidRec(long runningTotal, int index)
        {
            if (dic.TryGetValue((runningTotal, index), out var cachedIsValid))
            {
                return cachedIsValid;
            }
            else if (index == equation.Numbers.Length)
            {
                var isValid = runningTotal == equation.Total;
                dic.Add((runningTotal, index), isValid);
                return isValid;
            }
            else if (index > equation.Numbers.Length)
            {
                throw new Exception("Index should never be greater than length");
            }
            else
            {
                var curVal = equation.Numbers[index];

                var isValid =
                    IsValidRec(runningTotal * curVal, index + 1)
                    || IsValidRec(runningTotal + curVal, index + 1);
                dic.Add((runningTotal, index), isValid);
                return isValid;
            }
        }
    }

    public static long Solution1(string[] fileContents)
    {
        return Parse(fileContents).Where(IsValid).Select(x => x.Total).Sum();
    }

    public static int Solution2(string[] fileContents)
    {
        throw new NotImplementedException();
    }
}
