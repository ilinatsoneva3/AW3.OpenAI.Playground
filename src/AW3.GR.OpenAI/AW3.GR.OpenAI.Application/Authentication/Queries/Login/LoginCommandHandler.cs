using AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;
using MediatR;

namespace AW3.GR.OpenAI.Application.Authentication.Queries.Login;

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IJwtGenerator _jwtGenerator;

    public LoginCommandHandler(IJwtGenerator jwtGenerator)
    {
        _jwtGenerator = jwtGenerator;
    }

    public Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {

        //TODO: check if user exists

        //TODO check for correct password

        return Task.FromResult(new LoginResponse(Id: Guid.NewGuid(),
                                                 Username: string.Empty,
                                                 Email: request.Email,
                                                 Token: _jwtGenerator.GenerateToken(Guid.NewGuid(), request.Email)));
    }
}
