using AW3.GR.OpenAI.Application.Interfaces;
using AW3.GR.OpenAI.Domain.Enums;
using MediatR;

namespace AW3.GR.OpenAI.Application.Quotes.Commands.AskOpenAi;

internal sealed class AskOpenAiQueryHandler : IRequestHandler<AskOpenAiQuery, AskOpenAiResponse>
{
    private readonly IOpenAiClient _aiClient;

    public AskOpenAiQueryHandler(IOpenAiClient aiClient)
    {
        _aiClient = aiClient;
    }

    public async Task<AskOpenAiResponse> Handle(AskOpenAiQuery request, CancellationToken cancellationToken)
    {
        var result = request.Type switch
        {
            OpenAiQuestionType.Book => await _aiClient.GetMostPopularQuoteBookNameAsync(request.Name),
            OpenAiQuestionType.Author => await _aiClient.GetMostPopularQuoteByAuthorNameAsync(request.Name),
            _ => throw new NotImplementedException()
        };

        return new AskOpenAiResponse(result);
    }
}