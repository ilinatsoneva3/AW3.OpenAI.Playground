using FluentValidation;

namespace AW3.GR.OpenAI.Application.Modules.Authentication.Queries.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    private const string _regex = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$";

    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(_regex);
    }
}
