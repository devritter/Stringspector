namespace Stringspector;

public static class Extensions
{
    public static string StringJoin(this IEnumerable<char> chars)
    {
        return string.Join("", chars);
    }
}