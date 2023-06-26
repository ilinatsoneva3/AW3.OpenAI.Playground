using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Domain.SearchHistories;
using AW3.GR.OpenAI.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace AW3.GR.OpenAI.Infrastructure.Persistence.Repositories;

public class SearchHistoryRepository : ISearchHistoryRepository
{
    private readonly GROpenAIDbContext _dbContext;

    public SearchHistoryRepository(GROpenAIDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(SearchHistory entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<SearchHistory>> GetAllByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
        => await _dbContext.SearchHistories.Where(sh => sh.UserId.Equals(userId)).ToListAsync(cancellationToken);
}
