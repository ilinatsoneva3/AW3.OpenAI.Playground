using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Domain.AuthorAggregate.ValueObjects;
using AW3.GR.OpenAI.Domain.Quotes;
using Microsoft.EntityFrameworkCore;

namespace AW3.GR.OpenAI.Infrastructure.Persistence.Repositories;

public class QuoteRepository : IQuoteRepository
{
    private readonly GROpenAIDbContext _dbContext;

    public QuoteRepository(GROpenAIDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddQuoteAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        await _dbContext.Quotes.AddAsync(quote, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Quote?> GetByAuthorAndContentAsync(string partialMatch, AuthorId authorId, CancellationToken cancellationToken = default)
        => await _dbContext.Quotes
                .FirstOrDefaultAsync(q => q.Content.Equals(partialMatch)
                                          && q.AuthorId.Equals(authorId),
                                     cancellationToken);
}
