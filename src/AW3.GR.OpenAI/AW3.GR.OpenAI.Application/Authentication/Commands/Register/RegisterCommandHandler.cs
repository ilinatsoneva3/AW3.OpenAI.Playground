using AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;
using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Domain.Common.Errors;
using AW3.GR.OpenAI.Domain.Entities;
using ErrorOr;
using MediatR;

namespace AW3.GR.OpenAI.Application.Authentication.Commands.Register;

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<RegisterResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtGenerator jwtGenerator)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<ErrorOr<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(request.Email) != null)
            return Errors.User.DuplicateEmail;

        var user = new User { Email = request.Email, Username = request.Username, PasswordHash = request.Password };
        _userRepository.AddUser(user);

        return new RegisterResponse(user, _jwtGenerator.GenerateToken(user));
    }
}