namespace AW3.GR.OpenAI.Application.Interfaces;

public interface IOpenAiClient
{
    Task<string> GetMostPopularQuoteByAuthorNameAsync(string authorName);

    Task<string> GetMostPopularQuoteBookNameAsync(string bookName);
}