using FluentValidation;

namespace AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAI;

public class OpenAIQueryValidator : AbstractValidator<OpenAIQuery>
{
    public OpenAIQueryValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
