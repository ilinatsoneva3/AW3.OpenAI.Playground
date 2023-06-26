using AW3.GR.OpenAI.Domain.SearchHistories;
using AW3.GR.OpenAI.Domain.Users.ValueObjects;

namespace AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;

public interface ISearchHistoryRepository
{
    void AddAsync(SearchHistory entity);

    IEnumerable<SearchHistory> GetAllByUserIdAsync(UserId userId);
}
