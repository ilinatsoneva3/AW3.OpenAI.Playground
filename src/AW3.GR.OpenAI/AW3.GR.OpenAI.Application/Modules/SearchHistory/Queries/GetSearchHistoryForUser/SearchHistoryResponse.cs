using AW3.GR.OpenAI.Domain.SearchHistories.ValueObjects;

namespace AW3.GR.OpenAI.Application.Modules.SearchHistory.Queries.GetSearchHistoryForUser;

public sealed record SearchHistoryResponse(
    SearchHistoryId Id,
    string SearchText,
    string SearchResult,
    DateTime SearchDate);