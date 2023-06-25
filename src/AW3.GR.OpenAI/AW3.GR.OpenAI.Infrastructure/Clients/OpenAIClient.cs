using AW3.GR.OpenAI.Application.Interfaces;
using Rystem.OpenAi;

namespace AW3.GR.OpenAI.Infrastructure.Clients;

public class OpenAIClient : IOpenAiClient
{
    private const string AUTHOR_NAME_PROMPT = "Provide the most famous quote by [AN] and the book it is from in the format \"Book: Author: Quote:\"";
    private const string BOOK_NAME_PROMPT = "Provide the most liked quote from [BN] and its author in the format \"Book: Author: Quote:\"";
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

    public async Task<string> GetMostPopularQuoteBookNameAsync(string bookName)
    {
        var prompt = BOOK_NAME_PROMPT.Replace("[BN]", bookName);

        var result = await _openAiApi.Completion
                            .Request(prompt)
                            .WithModel(TextModelType.DavinciText3)
                            .WithTemperature(0.8)
                            .SetMaxTokens(512)
                            .ExecuteAsync();

        return result.Completions!.First().Text!;
    }
}