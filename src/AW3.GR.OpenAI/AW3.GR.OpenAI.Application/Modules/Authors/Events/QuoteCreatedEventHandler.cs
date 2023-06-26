using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Domain.Quotes.Events;
using AW3.GR.OpenAI.Domain.Quotes.ValueObjects;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Authors.Events;

public class QuoteCreatedEventHandler : INotificationHandler<QuoteCreated>
{
    private readonly IAuthorRepository _authorRepository;

    public QuoteCreatedEventHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task Handle(QuoteCreated notification, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(notification.Quote.AuthorId.Value);

        author!.AddQuote((QuoteId)notification.Quote.Id);

        await _authorRepository.SaveChangesAsync();
    }
}
