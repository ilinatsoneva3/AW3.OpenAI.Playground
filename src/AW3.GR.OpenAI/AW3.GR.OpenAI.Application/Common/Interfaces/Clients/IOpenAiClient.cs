namespace AW3.GR.OpenAI.Application.Interfaces;

public interface IOpenAIClient
{
    Task<string> GetMostPopularQuoteByAuthorNameAsync(string authorName);
}