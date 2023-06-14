using FluentValidation;

namespace AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAi;

public class AskOpenAiQueryValidator : AbstractValidator<AskOpenAiQuery>
{
    public AskOpenAiQueryValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Type).IsInEnum();
    }
}
