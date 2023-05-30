using AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;
using MediatR;

namespace AW3.GR.OpenAI.Application.Authentication.Commands.Register;

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IJwtGenerator _jwtGenerator;

    public RegisterCommandHandler(IJwtGenerator jwtGenerator)
    {
        _jwtGenerator = jwtGenerator;
    }

    public Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        //TO DO: check if user exists
        //TO DO: create user
        return Task.FromResult(new RegisterResponse(Guid.NewGuid(),
                                                    request.Email,
                                                    request.Username,
                                                    _jwtGenerator.GenerateToken(Guid.NewGuid(), request.Email)));
    }
}
