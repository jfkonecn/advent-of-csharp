namespace AdventOfCSharp.Console.Year2024.Day03;

public static class Solution202403
{
    private enum Operation
    {
        Unknown,
        Multiply,
    }

    private record Instruction { }

    private record MultiplyInstruction : Instruction
    {
        public required int Value1 { get; init; }
        public required int Value2 { get; init; }
    }

    private static bool TryParseAsMultiply(
        string s,
        int startIdx,
        out int newIndex,
        out Instruction instruction
    )
    {
        newIndex = startIdx;
        instruction = new Instruction();
        if (s.Length <= startIdx + 8)
        {
            return false;
        }

        if (s.Substring(startIdx, 3) != "mul")
        {
            return false;
        }

        newIndex = startIdx + 3;

        if (s[newIndex] != '(')
        {
            return false;
        }

        bool isValid;
        int value1;
        (isValid, newIndex, value1) = ParseNumber(s, newIndex, ',');
        if (!isValid)
        {
            return false;
        }

        int value2;
        (isValid, newIndex, value2) = ParseNumber(s, newIndex, ')');

        if (!isValid)
        {
            return false;
        }

        return true;
    }

    private static (bool isValid, int newIndex, int value1) ParseNumber(
        string s,
        int newIndex,
        char stopChar
    )
    {
        var digits = new List<char>();
        while (true)
        {
            newIndex++;
            if (newIndex >= s.Length)
            {
                return (false, 0, 0);
            }
            char curChar = s[newIndex];
            if (s[newIndex] == stopChar)
            {
                break;
            }
            else if (char.IsDigit(curChar))
            {
                digits.Add(curChar);
            }
            else
            {
                return (false, 0, 0);
            }
        }
        if (digits.Count > 3 || digits.Count < 1)
        {
            return (false, 0, 0);
        }
        int number = int.Parse(string.Join("", digits));
        return (true, newIndex + 1, number);
    }

    private IReadOnlyCollection<Instruction> Parse(string fileContents) { }

    public static int Solution1(string[] fileContents)
    {
        if (fileContents.Length != 1)
        {
            throw new Exception("Expected the length of the array to be 1");
        }
        throw new NotImplementedException();
    }

    public static int Solution2(string[] fileContents)
    {
        if (fileContents.Length != 1)
        {
            throw new Exception("Expected the length of the array to be 1");
        }
        throw new NotImplementedException();
    }
}
