using AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;
using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Domain.Entities;
using MediatR;

namespace AW3.GR.OpenAI.Application.Authentication.Queries.Login;

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IJwtGenerator jwtGenerator)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
    }

    public Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(request.Email) is not User user)
            throw new Exception("User does not exist");

        if (user.PasswordHash != request.Password)
            throw new Exception("Wrong password");

        return Task.FromResult(new LoginResponse(user, _jwtGenerator.GenerateToken(user)));
    }
}