using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Domain.Entities;
using AW3.GR.OpenAI.Domain.ValueObjects;

namespace AW3.GR.OpenAI.Infrastructure.Persistence.Repositories;

public class SearchHistoryRepository : ISearchHistoryRepository
{
    private static readonly List<SearchHistory> _searchHistory = new();
    public void CreateSearchHistoryEntryAsync(SearchHistory entity)
    {
        _searchHistory.Add(entity);
    }

    public IEnumerable<SearchHistory> GetAllByUserIdAsync(UserId userId)
        => _searchHistory.Where(sh => sh.UserId.Equals(userId));
}
