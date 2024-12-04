namespace AdventOfCSharp.Console.Year2024.Day04;

public static class Solution202404
{
    private static int TotalMatches(string[] fileContents, int i, int j, string toCheck)
    {
        int total = 0;
        var reversedToCheck = toCheck.Reverse().ToString();
        if (
            fileContents[i].Length >= j + toCheck.Length
            && fileContents[i].Substring(j, toCheck.Length) == toCheck
        )
        {
            total++;
        }
        if (j + 1 >= toCheck.Length && fileContents.Length >= i + toCheck.Length)
        {
            int curI = i;
            int curJ = j;
            bool shouldAdd = true;
            foreach (var c in toCheck)
            {
                if (fileContents[curI][curJ] != c)
                {
                    shouldAdd = false;
                    break;
                }
                curI++;
                curJ--;
            }
            if (shouldAdd)
            {
                total++;
            }
        }
        if (fileContents.Length >= i + toCheck.Length)
        {
            int curI = i;
            int curJ = j;
            bool shouldAdd = true;
            foreach (var c in toCheck)
            {
                if (fileContents[curI][curJ] != c)
                {
                    shouldAdd = false;
                    break;
                }
                curI++;
            }
            if (shouldAdd)
            {
                total++;
            }
        }
        if (
            j + toCheck.Length <= fileContents[i].Length
            && fileContents.Length >= i + toCheck.Length
        )
        {
            int curI = i;
            int curJ = j;
            bool shouldAdd = true;
            foreach (var c in toCheck)
            {
                if (fileContents[curI][curJ] != c)
                {
                    shouldAdd = false;
                    break;
                }
                curI++;
                curJ++;
            }
            if (shouldAdd)
            {
                total++;
            }
        }

        return total;
    }

    public static int Solution1(string[] fileContents)
    {
        int total = 0;
        for (int i = 0; i < fileContents.Length; i++)
        {
            var s = fileContents[i];
            for (int j = 0; j < s.Length; j++)
            {
                var c = s[j];
                if (c == 'X')
                {
                    total += TotalMatches(fileContents, i, j, "XMAS");
                }
                else if (c == 'S')
                {
                    total += TotalMatches(fileContents, i, j, "SAMX");
                }
            }
        }
        return total;
    }

    public static int Solution2(string[] fileContents)
    {
        throw new NotImplementedException();
    }
}
