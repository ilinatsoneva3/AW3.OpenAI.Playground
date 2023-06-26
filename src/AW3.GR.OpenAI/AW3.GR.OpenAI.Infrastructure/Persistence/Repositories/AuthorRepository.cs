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

    public async Task<Author?> GetAuthorByFirstAndLastNameAsync(string firstName, string lastName)
        => await _dbContext.Authors
                .FirstOrDefaultAsync(a => a.FirstName.Equals(firstName) && a.LastName.Equals(lastName));

    public async Task<Author?> GetAuthorByLastNameAsync(string lastName)
        => await _dbContext.Authors
                .FirstOrDefaultAsync(a => a.LastName.Equals(lastName));

    public async Task AddAuthorAsync(Author author)
    {
        await _dbContext.Authors.AddAsync(author);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Author?> GetByIdAsync(Guid id)
        => await _dbContext.Authors.FirstOrDefaultAsync(a => a.Id.Value == id);

    public async Task SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
}
