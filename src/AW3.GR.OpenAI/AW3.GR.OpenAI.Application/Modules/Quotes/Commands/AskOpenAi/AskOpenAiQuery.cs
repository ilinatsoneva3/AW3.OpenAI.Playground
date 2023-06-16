using AW3.GR.OpenAI.Domain.Enums;
using ErrorOr;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAi;

public sealed record AskOpenAiQuery(OpenAiQuestionType Type, string Name)
    : IRequest<ErrorOr<AskOpenAiResponse>>;