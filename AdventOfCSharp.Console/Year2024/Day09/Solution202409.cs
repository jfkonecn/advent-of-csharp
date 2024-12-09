namespace AdventOfCSharp.Console.Year2024.Day09;

public static class Solution202409
{
    private static (long?[], List<(long, int)>) Parse(string[] fileContents)
    {
        var result = new List<long?>();
        var empties = new List<(long, int)>();
        int id = -1;
        var line = fileContents.Single();
        for (int i = 0; i < line.Length; i++)
        {
            int? toAdd = null;
            var total = long.Parse(line[i].ToString());

            if (i % 2 == 0)
            {
                id++;
                toAdd = id;
            }
            else
            {
                empties.Add((total, result.Count));
            }

            for (int j = 0; j < total; j++)
            {
                result.Add(toAdd);
            }
        }
        return (result.ToArray(), empties);
    }

    public static long Solution1(string[] fileContents)
    {
        var (result, _) = Parse(fileContents);
        int endIndex = result.Length - 1;

        for (int i = 0; i < endIndex; i++)
        {
            if (!result[i].HasValue)
            {
                while (!result[endIndex].HasValue)
                {
                    endIndex--;
                }
                result[i] = result[endIndex];
                result[endIndex] = null;
            }
        }
        return result.Select((x, idx) => x.HasValue ? ((long)x.Value) * idx : 0).Sum();
    }

    public static long Solution2(string[] fileContents)
    {
        var (result, empties) = Parse(fileContents);

        for (int i = result.Length - 1; i >= 0; i--)
        {
            var curValue = result[i];
            if (curValue.HasValue)
            {
                int size = 0;
                long id = curValue.Value;
                while (i > 0 && (result[i] ?? -1) == id)
                {
                    size++;
                    i--;
                }
                int? emptyIndex = null;

                for (int j = 0; j < empties.Count; j++)
                {
                    var (spaceAvailable, placeIndex) = empties[j];
                    if (i <= placeIndex)
                    {
                        break;
                    }
                    else if (size <= spaceAvailable)
                    {
                        if (spaceAvailable == size)
                        {
                            empties.RemoveAt(j);
                        }
                        else
                        {
                            empties[j] = (spaceAvailable - size, placeIndex + size);
                        }
                        emptyIndex = placeIndex;
                        break;
                    }
                }

                for (int j = 0; emptyIndex.HasValue && j < size; j++)
                {
                    result[j + emptyIndex.Value] = id;
                    result[j + 1 + i] = null;
                }

                if (size > 0)
                {
                    i++;
                }
            }
        }

        return result.Select((x, idx) => x.HasValue ? ((long)x.Value) * idx : 0).Sum();
    }
}
