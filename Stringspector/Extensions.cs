namespace Stringspector;

public static class Extensions
{
    public static string StringJoin(this IEnumerable<char> chars)
    {
        return string.Join("", chars);
    }

    public static string Xray(this string input)
    {
        return input
            .Replace(" ", "[SPACE]")
            .Replace("\t", "[TAB]")
            .Replace("\r", "[CR]")
            .Replace("\n", "[LF]");
    }
}