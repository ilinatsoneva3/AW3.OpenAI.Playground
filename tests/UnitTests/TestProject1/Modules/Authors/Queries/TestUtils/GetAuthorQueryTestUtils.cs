using AW3.GR.OpenAI.Application.Modules.Authors.DTOs;
using AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Constants;
using AW3.GR.OpenAI.Domain.Authors;

namespace AW3.GR.OpenAI.Application.UnitTests.Modules.Authors.Queries.TestUtils;

public class GetAuthorQueryTestUtils
{
    public static Author GetAuthor()
        => Author.Create(AuthorConstants.FirstName, AuthorConstants.MiddleName, AuthorConstants.LastName, AuthorConstants.Id);

    public static List<Author> GetAuthorsList(int index = 0)
        => Enumerable.Range(0, index)
            .Select(index => Author.Create(
                AuthorConstants.AuthorFirstNameFromIndex(index),
                AuthorConstants.AuthorMiddleNameFromIndex(index),
                AuthorConstants.AuthorLastNameFromIndex(index),
                AuthorConstants.AuthorIdFromIndex(index)))
            .ToList();

    public static AuthorDTO GetAuthorDTO()
        => new(AuthorConstants.Id, AuthorConstants.FullName, new List<QuoteDTO>
            {
                new QuoteDTO(AuthorConstants.QuoteIdFromIndex(0),
                    AuthorConstants.QuoteContentFromIndex(0))
            });
}
