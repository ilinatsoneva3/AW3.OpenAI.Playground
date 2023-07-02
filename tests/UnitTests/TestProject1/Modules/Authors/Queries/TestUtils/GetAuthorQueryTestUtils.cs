using AW3.GR.OpenAI.Application.Modules.Authors.DTOs;
using AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Constants;
using AW3.GR.OpenAI.Domain.Authors;

namespace AW3.GR.OpenAI.Application.UnitTests.Modules.Authors.Queries.TestUtils;

public class GetAuthorQueryTestUtils
{
    public static Author GetAuthor()
        => Author.Create(Constants.Author.FirstName, Constants.Author.MiddleName, Constants.Author.LastName, Constants.Author.Id);

    public static List<Author> GetAuthorsList(int index = 0)
        => Enumerable.Range(0, index)
            .Select(index => Author.Create(
                Constants.Author.AuthorFirstNameFromIndex(index),
                Constants.Author.AuthorMiddleNameFromIndex(index),
                Constants.Author.AuthorLastNameFromIndex(index),
                Constants.Author.AuthorIdFromIndex(index)))
            .ToList();

    public static AuthorDTO GetAuthorDTO()
        => new(Constants.Author.Id, Constants.Author.FullName, new List<QuoteDTO>
            {
                new QuoteDTO(Constants.Author.QuoteIdFromIndex(0),
                    Constants.Author.QuoteContentFromIndex(0))
            });
}
