using AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAI;
using FluentAssertions;

namespace AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Extesnsions;

public static partial class OpenAIExtensions
{
    public static void ValidateOpenAIResponse(this OpenAIResponse response)
    {
        response.Response.Should().NotBeNullOrEmpty();
        response.Response.Should().Be(Constants.OpenAIConstants.Content);
    }
}
