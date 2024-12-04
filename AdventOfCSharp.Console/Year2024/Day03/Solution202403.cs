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

    private record DoInstruction : Instruction { }

    private record DontInstruction : Instruction { }

    private static bool TryParseAsDo(
        string s,
        int startIdx,
        out int newIndex,
        out DoInstruction instruction
    )
    {
        newIndex = startIdx;
        instruction = new DoInstruction() { };
        if (s.Length <= startIdx + 3)
        {
            return false;
        }
        if (s.Substring(startIdx, 4) != "do()")
        {
            return false;
        }
        newIndex += 4;
        return true;
    }

    private static bool TryParseAsDont(
        string s,
        int startIdx,
        out int newIndex,
        out DontInstruction instruction
    )
    {
        newIndex = startIdx;
        instruction = new DontInstruction() { };
        if (s.Length <= startIdx + 6)
        {
            return false;
        }
        if (s.Substring(startIdx, 7) != "don't()")
        {
            return false;
        }
        newIndex += 7;
        return true;
    }

    private static bool TryParseAsMultiply(
        string s,
        int startIdx,
        out int newIndex,
        out MultiplyInstruction instruction
    )
    {
        newIndex = startIdx;
        instruction = new MultiplyInstruction() { Value1 = 0, Value2 = 0 };
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
        newIndex++;

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

        instruction = new MultiplyInstruction() { Value1 = value1, Value2 = value2 };
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
            newIndex++;
        }
        if (digits.Count > 3 || digits.Count < 1)
        {
            return (false, 0, 0);
        }
        int number = int.Parse(string.Join("", digits));
        return (true, newIndex + 1, number);
    }

    public static int Solution1(string[] fileContents)
    {
        var s = string.Join("", fileContents);
        int i = 0;
        int answer = 0;
        while (i < s.Length)
        {
            if (TryParseAsMultiply(s, i, out var newIndex, out var instruction))
            {
                i = newIndex;
                answer += instruction.Value1 * instruction.Value2;
            }
            else
            {
                i++;
            }
        }
        return answer;
    }

    public static int Solution2(string[] fileContents)
    {
        var s = string.Join("", fileContents);
        int i = 0;
        bool shouldAdd = true;
        int answer = 0;
        while (i < s.Length)
        {
            if (TryParseAsMultiply(s, i, out var newIndex, out var instruction))
            {
                i = newIndex;
                if (shouldAdd)
                {
                    answer += instruction.Value1 * instruction.Value2;
                }
            }
            else if (TryParseAsDo(s, i, out newIndex, out var _))
            {
                i = newIndex;
                shouldAdd = true;
            }
            else if (TryParseAsDont(s, i, out newIndex, out var _))
            {
                i = newIndex;
                shouldAdd = false;
            }
            else
            {
                i++;
            }
        }
        return answer;
    }
}
