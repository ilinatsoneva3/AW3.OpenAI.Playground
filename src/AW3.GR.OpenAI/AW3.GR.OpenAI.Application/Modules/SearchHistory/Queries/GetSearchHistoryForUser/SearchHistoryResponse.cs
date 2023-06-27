namespace AW3.GR.OpenAI.Application.Modules.SearchHistory.Queries.GetSearchHistoryForUser;

public sealed record SearchHistoryResponse(
    string Id,
    string SearchText,
    string SearchResult,
    DateTime SearchDate);