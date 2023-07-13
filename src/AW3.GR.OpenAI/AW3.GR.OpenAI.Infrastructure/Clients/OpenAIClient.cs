using AW3.GR.OpenAI.Application.Interfaces;
using Rystem.OpenAi;

namespace AW3.GR.OpenAI.Infrastructure.Clients;

public class OpenAIClient : IOpenAIClient
{
    private const string AUTHOR_NAME_PROMPT = "Provide a quote by [AN] in the form of: \"Quote: Author:\"";
    private readonly IOpenAiFactory _openAiFactory;
    private readonly IOpenAi _openAiApi;

    public OpenAIClient(IOpenAiFactory openAiFactory)
    {
        _openAiFactory = openAiFactory;
        _openAiApi = _openAiFactory.Create();
    }

    public async Task<string> GetMostPopularQuoteByAuthorNameAsync(string authorName)
    {
        var prompt = AUTHOR_NAME_PROMPT.Replace("[AN]", authorName);

        var result = await _openAiApi.Completion
                            .Request(prompt)
                            .WithModel(TextModelType.DavinciText3)
                            .WithTemperature(0.8)
                            .SetMaxTokens(512)
                            .ExecuteAsync();

        return result.Completions!.First().Text!;
    }
}