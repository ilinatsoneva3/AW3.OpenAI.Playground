using ErrorOr;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Quotes.Commands.CreateQuoteCommand;

public sealed record CreateQuoteCommand(string Content, Guid AuthorId)
    : IRequest<ErrorOr<CreateQuoteResponse>>;