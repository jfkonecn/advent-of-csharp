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
        int total = 0;
        for (int i = 1; i < fileContents.Length - 1; i++)
        {
            var preS = fileContents[i - 1];
            var s = fileContents[i];
            var nextS = fileContents[i + 1];
            for (int j = 1; j < s.Length - 1; j++)
            {
                if (s[j] != 'A')
                {
                    continue;
                }
                var fromTopLeft =
                    (preS[j - 1] == 'M' && nextS[j + 1] == 'S')
                    || (preS[j - 1] == 'S' && nextS[j + 1] == 'M');
                var fromBottomLeft =
                    (nextS[j - 1] == 'M' && preS[j + 1] == 'S')
                    || (nextS[j - 1] == 'S' && preS[j + 1] == 'M');
                if (fromTopLeft && fromBottomLeft)
                {
                    total++;
                }
            }
        }
        return total;
    }
}
