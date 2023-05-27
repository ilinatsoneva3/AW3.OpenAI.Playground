using AW3.GR.OpenAI.Application.Interfaces;
using Rystem.OpenAi;

public class OpenAIClient : IOpenAiClient
{
    private const string PROMPT = "Find the most liked quote by [AN] on Goodreads";
    private readonly IOpenAiFactory _openAiFactory;

    public OpenAIClient(IOpenAiFactory openAiFactory)
    {
        _openAiFactory = openAiFactory;
    }

    public async Task<string> GetMostPopularQuoteByAuthorNameAsync(string authorName)
    {
        var openAiApi = _openAiFactory.Create();

        var prompt = PROMPT.Replace("[AN]", authorName);

        var result = await openAiApi.Completion
                            .Request(prompt)
                            .WithModel(TextModelType.DavinciText3)
                            .WithTemperature(0.8)
                            .SetMaxTokens(256)
                            .ExecuteAsync();

        return result.Completions!.First().Text!;
    }
}