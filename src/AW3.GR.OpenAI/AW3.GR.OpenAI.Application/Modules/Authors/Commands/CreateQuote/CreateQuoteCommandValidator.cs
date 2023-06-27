using FluentValidation;

namespace AW3.GR.OpenAI.Application.Modules.Authors.Commands.CreateQuote;

public class CreateQuoteCommandValidator : AbstractValidator<CreateQuoteCommand>
{
    public CreateQuoteCommandValidator()
    {
        RuleFor(q => q.Content)
            .NotNull()
            .MinimumLength(10)
            .WithMessage("{PropertyName} should not be null or empty");
    }
}
