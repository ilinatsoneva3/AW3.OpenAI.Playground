using AW3.GR.OpenAI.Domain.Enums;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAi;

public sealed record AskOpenAiQuery(OpenAiQuestionType Type, string Name) : IRequest<AskOpenAiResponse>;