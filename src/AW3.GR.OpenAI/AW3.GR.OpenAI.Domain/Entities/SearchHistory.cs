using AW3.GR.OpenAI.Domain.Common.Models;
using AW3.GR.OpenAI.Domain.Enums;
using AW3.GR.OpenAI.Domain.ValueObjects;

namespace AW3.GR.OpenAI.Domain.Entities;

public class SearchHistory : AggregateRoot<SearchHistoryId, Guid>
{
    public OpenAiQuestionType QuestionType { get; private set; }

    public string SearchText { get; private set; }

    public DateTime SearchDate { get; private set; } = DateTime.UtcNow;

    public string SearchResult { get; private set; }

    public UserId UserId { get; private set; }

    public SearchHistory(
        OpenAiQuestionType questionType,
        string searchText,
        DateTime searchDate,
        string searchResult,
        UserId userId) : base(SearchHistoryId.CreateUnique())
    {
        QuestionType = questionType;
        SearchText = searchText;
        SearchDate = searchDate;
        SearchResult = searchResult;
        UserId = userId;
    }

    public static SearchHistory Create(
        OpenAiQuestionType questionType,
        string searchText,
        DateTime searchDate,
        string searchResult,
        UserId userId)
        => new(questionType, searchText, searchDate, searchResult, userId);

    private SearchHistory()
    {
    }
}
