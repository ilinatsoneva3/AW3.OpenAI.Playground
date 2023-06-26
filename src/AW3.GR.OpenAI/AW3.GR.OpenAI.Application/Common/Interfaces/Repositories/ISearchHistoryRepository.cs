using AW3.GR.OpenAI.Domain.SearchHistories;
using AW3.GR.OpenAI.Domain.Users.ValueObjects;

namespace AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;

public interface ISearchHistoryRepository
{
    Task AddAsync(SearchHistory entity, CancellationToken cancellationToken = default);

    Task<IEnumerable<SearchHistory>> GetAllByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);
}
