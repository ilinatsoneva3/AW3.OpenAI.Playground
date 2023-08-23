using AW3.GR.OpenAI.Domain.Common.Models;
using AW3.GR.OpenAI.Domain.SearchHistories.ValueObjects;
using AW3.GR.OpenAI.Domain.Users.ValueObjects;

namespace AW3.GR.OpenAI.Domain.SearchHistories;

public class SearchHistory : AggregateRoot<SearchHistoryId, Guid>
{
    public string SearchText { get; private set; }

    public DateTime SearchDate { get; private set; } = DateTime.UtcNow;

    public string SearchResult { get; private set; }

    public UserId UserId { get; private set; }

    public SearchHistory(
        string searchText,
        DateTime searchDate,
        string searchResult,
        UserId userId) : base(SearchHistoryId.CreateUnique())
    {
        SearchText = searchText;
        SearchDate = searchDate;
        SearchResult = searchResult;
        UserId = userId;
    }

    public static SearchHistory Create(
        string searchText,
        DateTime searchDate,
        string searchResult,
        UserId userId)
        => new(searchText, searchDate, searchResult, userId);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SearchHistory()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}
