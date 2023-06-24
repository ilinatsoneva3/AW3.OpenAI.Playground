using AW3.GR.OpenAI.Domain.SearchHistories.ValueObjects;

namespace AW3.GR.OpenAI.Application.Modules.SearchHistory.Queries.GetSearchHistoryForUser;

public sealed record SearchHistoryResponse(
    SearchHistoryId SearchHistoryId,
    string SearchText,
    string SearchResult,
    DateTime SearchedDate);