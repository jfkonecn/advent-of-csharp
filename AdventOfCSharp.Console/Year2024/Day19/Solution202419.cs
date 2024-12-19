namespace AdventOfCSharp.Console.Year2024.Day19;

public static class Solution202419
{
    private record ParseResult
    {
        public required string[] Towels { get; init; }
        public required string[] Patterns { get; init; }
    }

    private static ParseResult Parse(string[] fileContents)
    {
        return new()
        {
            Towels = fileContents[0].Split(',').Select(x => x.Trim()).ToArray(),
            Patterns = fileContents.Skip(2).ToArray(),
        };
    }

    public static int Solution1(string[] fileContents)
    {
        var result = Parse(fileContents);
        var patterns = result.Patterns;
        var towels = result.Towels;
        int answer = 0;
        var dic = new Dictionary<string, bool>();
        foreach (var curPattern in patterns)
        {
            if (IsPossible(curPattern))
            {
                answer += 1;
            }
        }
        return answer;

        bool IsPossible(string pattern)
        {
            if (dic.TryGetValue(pattern, out var cacheValue))
            {
                return cacheValue;
            }
            foreach (var towel in towels)
            {
                if (
                    pattern.StartsWith(towel)
                    && (
                        towel.Length == pattern.Length
                        || (
                            pattern.Length > towel.Length
                            && IsPossible(pattern.Substring(towel.Length))
                        )
                    )
                )
                {
                    dic.Add(pattern, true);
                    return true;
                }
            }
            dic.Add(pattern, false);
            return false;
        }
    }

    public static long Solution2(string[] fileContents)
    {
        var result = Parse(fileContents);
        var patterns = result.Patterns;
        var towels = result.Towels;
        long answer = 0;
        var dic = new Dictionary<string, long>();
        foreach (var curPattern in patterns)
        {
            answer += IsPossible(curPattern);
        }
        return answer;

        long IsPossible(string pattern)
        {
            if (dic.TryGetValue(pattern, out var cacheValue))
            {
                return cacheValue;
            }
            long answer = 0;
            foreach (var towel in towels)
            {
                if (pattern.StartsWith(towel))
                {
                    if (towel.Length == pattern.Length)
                    {
                        answer += 1;
                    }
                    else if (pattern.Length > towel.Length)
                    {
                        answer += IsPossible(pattern.Substring(towel.Length));
                    }
                }
            }
            dic.Add(pattern, answer);
            return answer;
        }
    }
}
