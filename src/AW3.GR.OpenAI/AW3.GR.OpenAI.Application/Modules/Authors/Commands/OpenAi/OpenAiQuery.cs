using ErrorOr;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAI;

public sealed record OpenAIQuery(string Name)
    : IRequest<ErrorOr<OpenAIResponse>>;