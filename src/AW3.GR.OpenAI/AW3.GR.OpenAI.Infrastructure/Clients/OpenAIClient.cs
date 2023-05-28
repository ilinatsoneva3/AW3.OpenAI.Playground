using AW3.GR.OpenAI.Application.Interfaces;
using Rystem.OpenAi;

namespace AW3.GR.OpenAI.Infrastructure.Clients;

public class OpenAIClient : IOpenAiClient
{
    private const string AUTHOR_NAME_PROMPT = "Find the most liked quote by [AN] on Goodreads";
    private const string BOOK_NAME_PROMPT = "Find [BN] most liked quote on Goodreads";
    private readonly IOpenAiFactory _openAiFactory;

    public OpenAIClient(IOpenAiFactory openAiFactory)
    {
        _openAiFactory = openAiFactory;
    }

    public async Task<string> GetMostPopularQuoteByAuthorNameAsync(string authorName)
    {
        var openAiApi = _openAiFactory.Create();

        var prompt = AUTHOR_NAME_PROMPT.Replace("[AN]", authorName);

        var result = await openAiApi.Completion
                            .Request(prompt)
                            .WithModel(TextModelType.DavinciText3)
                            .WithTemperature(0.8)
                            .SetMaxTokens(256)
                            .ExecuteAsync();

        return result.Completions!.First().Text!;
    }

    public async Task<string> GetMostPopularQuoteBookNameAsync(string bookName)
    {
        var openAiApi = _openAiFactory.Create();

        var prompt = BOOK_NAME_PROMPT.Replace("[BN]", bookName);

        var result = await openAiApi.Completion
                            .Request(prompt)
                            .WithModel(TextModelType.DavinciText3)
                            .WithTemperature(0.8)
                            .SetMaxTokens(256)
                            .ExecuteAsync();

        return result.Completions!.First().Text!;
    }
}