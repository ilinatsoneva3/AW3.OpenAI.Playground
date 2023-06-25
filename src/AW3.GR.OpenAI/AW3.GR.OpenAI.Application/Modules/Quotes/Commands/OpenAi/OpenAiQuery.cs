using ErrorOr;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAi;

public sealed record OpenAiQuery(string Type, string Name)
    : IRequest<ErrorOr<OpenAiResponse>>;