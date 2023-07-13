using AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAI;
using AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Constants;
using AW3.GR.OpenAI.Domain.Users;

namespace AW3.GR.OpenAI.Application.UnitTests.Modules.Authors.Commands.OpenAI;

public class OpenAIQueryHandlerUtils
{
    public static OpenAIQuery CreateQuery()
        => new(AuthorConstants.FullName);

    public static User GetUser()
        => User.Create(UserConstants.Username, UserConstants.Email, UserConstants.Password);

    public static string GetOpenAIResponse()
        => OpenAIConstants.Content;
}
