using AW3.GR.OpenAI.Application.Modules.Authors.Commands.CreateQuote;
using AW3.GR.OpenAI.Application.Modules.Authors.DTOs;
using AW3.GR.OpenAI.Domain.Authors;
using FluentAssertions;

namespace AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Extensions;

public static partial class AuthorExtensions
{
    public static void ValidateCreatedFrom(this AuthorDTO author, CreateQuoteCommand command)
    {
        author.FullName.Should().Be(Constants.AuthorConstants.FullName);
        author.Id.Should().Be(Constants.AuthorConstants.Id);
        author.Quotes.Should().HaveCount(1);
        author.Quotes.First().Content.Should().Be(command.Quote.Content);
    }

    public static void ValidateGetAuthorList(this IEnumerable<AuthorDTO> authorsList, IEnumerable<Author> authors)
    {
        authorsList.Count().Should().Be(1);
        authorsList.First().Id.Should().Be(authors.First().Id.ToString());
    }
}
