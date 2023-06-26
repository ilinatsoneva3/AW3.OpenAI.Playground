﻿using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Domain.AuthorAggregate.ValueObjects;
using AW3.GR.OpenAI.Domain.Authors;
using AW3.GR.OpenAI.Domain.Quotes;
using AW3.GR.OpenAI.Domain.Quotes.ValueObjects;
using AW3.GR.OpenAI.Domain.SearchHistories.Events;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Authors;

public class SearchHistoryCreatedEventHandler : INotificationHandler<SearchHistoryCreated>
{
    private const string FIRST_NAME = "FirstName";
    private const string LAST_NAME = "LastName";
    private const string MIDDLE_NAME = "MiddleName";

    private readonly IAuthorRepository _authorRepository;
    private readonly IQuoteRepository _quoteRepository;

    public SearchHistoryCreatedEventHandler(
        IAuthorRepository authorRepository,
        IQuoteRepository quoteRepository)
    {
        _authorRepository = authorRepository;
        _quoteRepository = quoteRepository;
    }

    public async Task Handle(SearchHistoryCreated notification, CancellationToken cancellationToken)
    {
        var content = notification.SearchHistory.SearchResult;

        ArgumentException.ThrowIfNullOrEmpty(content, nameof(content));

        var (quoteText, authorName) = GenerateQuoteAuthorFromResponse(content);

        Author author = await GetAuthorByFullNameAsync(authorName, cancellationToken);

        Quote quote = await GetQuoteAsync(quoteText, (AuthorId)author.Id);

        author.AddQuote((QuoteId)quote.Id);

        await _authorRepository.AddAuthorAsync(author, cancellationToken);
        await _quoteRepository.AddQuoteAsync(quote, cancellationToken);
    }

    private async Task<Quote> GetQuoteAsync(string text, AuthorId authorId)
    {
        var quote = await _quoteRepository.GetByAuthorAndContentAsync(text[..20], authorId);

        quote ??= Quote.Create(text, authorId);

        return quote;
    }

    #region RetrieveAuthor
    private async Task<Author> GetAuthorByFullNameAsync(string authorName, CancellationToken cancellationToken)
    {
        var authorNames = GetAuthorNames(authorName);

        var author = authorNames.Count == 1
            ? await _authorRepository.FirstOrDefaultAsync(a => a.LastName.Equals(authorNames[LAST_NAME]), cancellationToken)
            : await _authorRepository.FirstOrDefaultAsync(a => a.FirstName.Equals(authorNames[FIRST_NAME]) && a.LastName.Equals(authorNames[LAST_NAME]), cancellationToken);

        author ??= Author.Create(authorNames[FIRST_NAME], authorNames[LAST_NAME], authorNames.GetValueOrDefault(MIDDLE_NAME));
        return author;
    }

    private Dictionary<string, string> GetAuthorNames(string authorName)
    {
        var namesList = authorName.Split(" ");

        var result = new Dictionary<string, string>
        {
            { LAST_NAME , namesList[^1] }
        };

        if (namesList.Length > 1)
            result.Add(FIRST_NAME, namesList[0]);

        if (namesList.Length > 2)
            result.Add(MIDDLE_NAME, string.Join(" ", namesList[1..(namesList.Length - 1)]));

        return result;
    }

    #endregion

    #region ProcessOpenAiResponseContent
    private static (string quote, string authorName) GenerateQuoteAuthorFromResponse(string text)
    {
        var strippedText = StripSpecialCharacters(text);

        var lastIndex = strippedText.LastIndexOf('-');

        if (lastIndex < 0)
            lastIndex = strippedText.LastIndexOf(':');

        var quote = strippedText[..lastIndex].Trim();
        var authorName = strippedText[(lastIndex + 1)..].Trim();

        return (quote, authorName);
    }

    private static string StripSpecialCharacters(string text)
    {
        return text
            .Replace("\n", string.Empty)
            .Replace("\"", string.Empty)
            .Replace("\\", string.Empty);
    }
    #endregion
}
