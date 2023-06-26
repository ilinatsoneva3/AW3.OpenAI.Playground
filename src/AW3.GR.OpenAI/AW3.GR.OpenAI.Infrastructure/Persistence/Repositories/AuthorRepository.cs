using System.Linq.Expressions;
using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Domain.Authors;
using Microsoft.EntityFrameworkCore;

namespace AW3.GR.OpenAI.Infrastructure.Persistence.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly GROpenAIDbContext _dbContext;

    public AuthorRepository(GROpenAIDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAuthorAsync(Author author, CancellationToken cancellationToken = default)
    {
        await _dbContext.Authors.AddAsync(author, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
        => await _dbContext.SaveChangesAsync(cancellationToken);

    public async Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Authors.ToListAsync(cancellationToken);

    public async Task<Author?> FirstOrDefaultAsync(Expression<Func<Author, bool>>? predicate = null, CancellationToken cancellationToken = default)
        => await _dbContext.Authors.FirstOrDefaultAsync(predicate, cancellationToken);
}
