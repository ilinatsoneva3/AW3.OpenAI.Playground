using AW3.GR.OpenAI.Application.Interfaces;
using MediatR;

namespace AW3.GR.OpenAI.Application.Quotes.AskOpenAi;

internal sealed class AskOpenAiQueryHandler : IRequestHandler<AskOpenAiQuery, AskOpenAiResponse>
{
    private readonly IOpenAiClient _aiClient;

    public AskOpenAiQueryHandler(IOpenAiClient aiClient)
    {
        _aiClient = aiClient;
    }

    public async Task<AskOpenAiResponse> Handle(AskOpenAiQuery request, CancellationToken cancellationToken)
    {
        var result = await _aiClient.GetMostPopularQuoteByAuthorNameAsync(request.AuthorName);

        return new AskOpenAiResponse(result);
    }
}
