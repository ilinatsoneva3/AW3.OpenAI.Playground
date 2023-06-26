using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Domain.SearchHistories;
using AW3.GR.OpenAI.Domain.Users.ValueObjects;

namespace AW3.GR.OpenAI.Infrastructure.Persistence.Repositories;

public class SearchHistoryRepository : ISearchHistoryRepository
{
    private readonly GROpenAIDbContext _dbContext;

    public SearchHistoryRepository(GROpenAIDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddAsync(SearchHistory entity)
    {
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
    }

    public IEnumerable<SearchHistory> GetAllByUserIdAsync(UserId userId)
        => _dbContext.SearchHistories.Where(sh => sh.UserId.Equals(userId));
}
