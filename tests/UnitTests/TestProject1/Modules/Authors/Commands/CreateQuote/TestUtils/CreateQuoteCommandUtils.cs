using AW3.GR.OpenAI.Application.Modules.Authors.Commands.CreateQuote;
using AW3.GR.OpenAI.Application.Modules.Authors.DTOs;
using AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Constants;
using AW3.GR.OpenAI.Domain.Authors;

namespace AW3.GR.OpenAI.Application.UnitTests.Modules.Authors.Commands.CreateQuote.TestUtils;

public class CreateQuoteCommandUtils
{
    public static CreateQuoteCommand CreateQuote()
        => new(CreateQuoteDto(), Constants.Author.Id.ToString()!);

    public static CreateQuoteCommand CreateQuoteInvalidAuthorId()
        => new(CreateQuoteDto(), Constants.Author.InvalidId.ToString()!);

    public static CreateQuoteDto CreateQuoteDto()
        => new(Constants.Author.QuoteContentFromIndex(0));

    public static AuthorDTO CreateAuthorDto()
        => new(Constants.Author.Id, Constants.Author.FullName, CreateQuoteList());

    public static Author GetAuthor()
        => Author.Create(Constants.Author.FirstName, Constants.Author.MiddleName, Constants.Author.LastName, Constants.Author.Id);

    public static List<QuoteDTO> CreateQuoteList(int count = 1)
        => Enumerable.Range(0, count)
                .Select(index => new QuoteDTO(
                    Constants.Author.QuoteIdFromIndex(index),
                    Constants.Author.QuoteContentFromIndex(index)))
                .ToList();
}
