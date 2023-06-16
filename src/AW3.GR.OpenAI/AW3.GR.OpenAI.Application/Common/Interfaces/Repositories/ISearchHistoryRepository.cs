using AW3.GR.OpenAI.Domain.SearchHistoryAggregate;
using AW3.GR.OpenAI.Domain.UserAggregate.ValueObjects;

namespace AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;

public interface ISearchHistoryRepository
{
    void CreateSearchHistoryEntryAsync(SearchHistory entity);

    IEnumerable<SearchHistory> GetAllByUserIdAsync(UserId userId);
}
