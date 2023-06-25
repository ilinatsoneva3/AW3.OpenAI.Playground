using AW3.GR.OpenAI.Domain.AuthorAggregate.ValueObjects;
using AW3.GR.OpenAI.Domain.Quotes;

namespace AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;

public interface IQuoteRepository
{
    Task<Quote?> GetByAuthorAndContentAsync(string partialMatch, AuthorId authorId);

    Task AddQuoteAsync(Quote quote);
}
