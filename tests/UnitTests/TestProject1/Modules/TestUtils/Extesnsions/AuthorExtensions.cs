using AW3.GR.OpenAI.Application.Modules.Authors.Commands.CreateQuote;
using AW3.GR.OpenAI.Application.Modules.Authors.DTOs;
using FluentAssertions;

namespace AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Extensions;

public static partial class AuthorExtensions
{
    public static void ValidateCreatedFrom(this AuthorDTO author, CreateQuoteCommand command)
    {
        author.FullName.Should().Be(Constants.Constants.Author.FullName);
        author.Id.Should().Be(Constants.Constants.Author.Id);
        author.Quotes.Should().HaveCount(1);
        author.Quotes.First().Content.Should().Be(command.Quote.Content);
    }
}
