using AW3.GR.OpenAI.Application.Modules.Authors.Commands.CreateQuote;
using AW3.GR.OpenAI.Application.Modules.Authors.DTOs;
using AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Constants;
using AW3.GR.OpenAI.Domain.Authors;

namespace AW3.GR.OpenAI.Application.UnitTests.Modules.Authors.Commands.CreateQuote.TestUtils;

public class CreateQuoteCommandUtils
{
    public static CreateQuoteCommand CreateQuote()
        => new(CreateQuoteDto(), AuthorConstants.Id.ToString()!);

    public static CreateQuoteCommand CreateQuoteInvalidAuthorId()
        => new(CreateQuoteDto(), AuthorConstants.InvalidId.ToString()!);

    public static CreateQuoteDto CreateQuoteDto()
        => new(AuthorConstants.QuoteContentFromIndex(0));

    public static AuthorDTO CreateAuthorDto()
        => new(AuthorConstants.Id, AuthorConstants.FullName, CreateQuoteList());

    public static Author GetAuthor()
        => Author.Create(
            AuthorConstants.FirstName, AuthorConstants.MiddleName, AuthorConstants.LastName, AuthorConstants.Id);

    public static List<QuoteDTO> CreateQuoteList(int count = 1)
        => Enumerable.Range(0, count)
                .Select(index => new QuoteDTO(
                    AuthorConstants.QuoteIdFromIndex(index),
                    AuthorConstants.QuoteContentFromIndex(index)))
                .ToList();
}
