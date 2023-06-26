using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Domain.AuthorAggregate.ValueObjects;
using AW3.GR.OpenAI.Domain.Common.Errors;
using AW3.GR.OpenAI.Domain.Quotes;
using AW3.GR.OpenAI.Domain.Quotes.Events;
using ErrorOr;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Quotes.Commands.CreateQuoteCommand;

public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, ErrorOr<CreateQuoteResponse>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IQuoteRepository _quoteRepository;

    public CreateQuoteCommandHandler(IAuthorRepository authorRepository, IQuoteRepository quoteRepository)
    {
        _authorRepository = authorRepository;
        _quoteRepository = quoteRepository;
    }

    public async Task<ErrorOr<CreateQuoteResponse>> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
    {
        var authorId = AuthorId.Create(request.AuthorId);
        var author = await _authorRepository.FirstOrDefaultAsync(a => a.Id == authorId, cancellationToken);

        if (author is null)
            return Errors.Author.AuthorNotFound;

        var newQuote = Quote.Create(request.Content, (AuthorId)author.Id);

        newQuote.AddDomainEvent(new QuoteCreated(newQuote));

        await _quoteRepository.AddQuoteAsync(newQuote, cancellationToken);
        return new CreateQuoteResponse(newQuote.Id.Value, newQuote.Content, author.GetFullName());
    }
}
