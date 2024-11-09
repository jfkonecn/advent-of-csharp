namespace AdventOfCSharp.Console;

public static class Utils
{
    public static (bool found, T? value) TryGetValueOrDefault<T>(
        this IReadOnlyList<T> list,
        int index
    )
    {
        if (list.Count > index && index >= 0)
        {
            return (true, list[index]);
        }
        else
        {
            return (false, default);
        }
    }
}
