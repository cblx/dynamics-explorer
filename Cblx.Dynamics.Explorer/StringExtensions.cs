using System.Text.RegularExpressions;

namespace Cblx.Dynamics.Explorer;

public static partial class StringExtensions
{
    public static string RemoveLineEndingsForODataQuery(this string query)
    {
        query = RemoveAfterQuestionMark().Replace(query, "?");
        query = RemoveAfterAmpersand().Replace(query, "&");
        return query;
    }

    [GeneratedRegex("\\?\\s+")]
    private static partial Regex RemoveAfterQuestionMark();
    [GeneratedRegex("&\\s+")]
    private static partial Regex RemoveAfterAmpersand();
}
