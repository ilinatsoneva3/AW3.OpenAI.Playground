using AW3.GR.OpenAI.Application.Modules.SearchHistory.Queries.GetSearchHistoryForUser;
using AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Constants;
using AW3.GR.OpenAI.Domain.Users.ValueObjects;

namespace AW3.GR.OpenAI.Application.UnitTests.Modules.SearchHistory.Queries.GetSearchHistoryForUser;

public static class GetSearchHistoryQueryHandlerTestUtils
{
    public static GetSearchHistoryQuery GetSearchHistoryQuery()
        => new();

    public static List<Domain.SearchHistories.SearchHistory> GetSearchHistory(int count = 1)
        => Enumerable.Range(0, count)
            .Select(index => Domain.SearchHistories.SearchHistory.Create(
                SearchHistoryConstants.SearchTextFromIndex(index),
                SearchHistoryConstants.SearchDate,
                SearchHistoryConstants.SearchResultFromIndex(index),
                UserId.Create(UserConstants.Id)))
            .ToList();

    public static SearchHistoryResponse GetSearchHistoryResponse()
        => new(SearchHistoryConstants.Id,
               SearchHistoryConstants.SearchTextFromIndex(0),
               SearchHistoryConstants.SearchResultFromIndex(0),
               SearchHistoryConstants.SearchDate);
}
