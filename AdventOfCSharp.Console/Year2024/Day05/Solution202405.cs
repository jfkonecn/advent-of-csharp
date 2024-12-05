namespace AdventOfCSharp.Console.Year2024.Day05;

public static class Solution202405
{
    private record OrderRule
    {
        public List<int> Before { get; } = new();
        public List<int> After { get; } = new();
    }

    private record ParsedResult
    {
        public Dictionary<int, OrderRule> Rules { get; } = new();
        public List<List<int>> Pages { get; } = new();
    }

    private static ParsedResult Parse(string[] fileContents)
    {
        var result = new ParsedResult();
        int i = 0;

        while (!string.IsNullOrWhiteSpace(fileContents[i]))
        {
            var split = fileContents[i].Split('|');
            var first = int.Parse(split[0]);
            var second = int.Parse(split[1]);
            AddKeyToRules(first, second, false);
            AddKeyToRules(second, first, true);
            i++;
        }
        i++;

        for (int j = i; j < fileContents.Length; j++)
        {
            var pages = new List<int>();
            foreach (var c in fileContents[j].Split(','))
            {
                pages.Add(int.Parse(c));
            }
            result.Pages.Add(pages);
        }
        return result;

        void AddKeyToRules(int key, int value, bool valueComesBefore)
        {
            result.Rules.TryAdd(key, new OrderRule());
            if (valueComesBefore)
            {
                result.Rules[key].Before.Add(value);
            }
            else
            {
                result.Rules[key].After.Add(value);
            }
        }
    }

    public static int Solution1(string[] fileContents)
    {
        var parsedResult = Parse(fileContents);
        var rules = parsedResult.Rules;

        return parsedResult
            .Pages.Where(pages =>
            {
                for (int i = 1; i < pages.Count; i++)
                {
                    var x = pages[i - 1];
                    var y = pages[i];
                    var rule = rules[x];
                    if (rule.Before.Contains(y))
                    {
                        return false;
                    }
                }
                return true;
            })
            .Select(x =>
            {
                var temp = x.Count / 2;
                var temp2 = x[x.Count / 2];
                return x[x.Count / 2];
            })
            .Sum();
    }

    public static int Solution2(string[] fileContents)
    {
        var parsedResult = Parse(fileContents);
        var rules = parsedResult.Rules;

        return parsedResult
            .Pages.Where(pages =>
            {
                for (int i = 1; i < pages.Count; i++)
                {
                    var x = pages[i - 1];
                    var y = pages[i];
                    var rule = rules[x];
                    if (rule.Before.Contains(y))
                    {
                        return true;
                    }
                }
                return false;
            })
            .Select(pages =>
            {
                pages.Sort(
                    (x, y) =>
                    {
                        var rule = rules[x];
                        if (rule.After.Contains(y))
                        {
                            return -1;
                        }
                        else if (rule.Before.Contains(y))
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                );
                return pages[pages.Count / 2];
            })
            .Sum();
        throw new NotImplementedException();
    }
}
