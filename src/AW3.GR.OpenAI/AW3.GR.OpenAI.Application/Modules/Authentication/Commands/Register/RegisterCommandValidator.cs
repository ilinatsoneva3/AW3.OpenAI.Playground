using FluentValidation;

namespace AW3.GR.OpenAI.Application.Modules.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private const string _regex = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$";
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(_regex);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
