using MediatR;

namespace AW3.GR.OpenAI.Application.Quotes.AskOpenAi;

public sealed record AskOpenAiQuery(string AuthorName) : IRequest<AskOpenAiResponse>;