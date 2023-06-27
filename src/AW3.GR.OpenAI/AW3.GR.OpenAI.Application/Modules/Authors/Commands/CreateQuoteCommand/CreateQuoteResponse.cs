namespace AW3.GR.OpenAI.Application.Modules.Quotes.Commands.CreateQuoteCommand;

public sealed record CreateQuoteResponse(Guid QuoteId, string Content, string AuthorName);