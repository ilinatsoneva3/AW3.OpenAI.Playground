using AW3.GR.OpenAI.Domain.Entities;
using AW3.GR.OpenAI.Domain.ValueObjects;

namespace AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;

public interface ISearchHistoryRepository
{
    void CreateSearchHistoryEntryAsync(SearchHistory entity);

    IEnumerable<SearchHistory> GetAllByUserIdAsync(UserId userId);
}
