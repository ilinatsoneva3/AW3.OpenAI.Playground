using FluentValidation;

namespace AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAi;

public class OpenAiQueryValidator : AbstractValidator<OpenAiQuery>
{
    public OpenAiQueryValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
