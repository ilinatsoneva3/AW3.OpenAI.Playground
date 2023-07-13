namespace AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Constants;

public static class SearchHistoryConstants
{
    public static readonly string Id = "64e1f29a-45dd-4941-bc2f-591aa42dfc25";

    public static readonly DateTime SearchDate = DateTime.UtcNow;

    public static string SearchResult = OpenAIConstants.Content;

    public static string SearchTextFromIndex(int index)
        => AuthorConstants.AuthorFullNameFromIndex(index);

    public static string SearchResultFromIndex(int index)
        => $"{SearchResult} {index}";
}
