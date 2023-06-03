using AW3.GR.OpenAI.Application.Common.Errors;
using AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;
using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Domain.Entities;
using MediatR;

namespace AW3.GR.OpenAI.Application.Authentication.Commands.Register;

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
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

    public Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(request.Email) != null)
            throw new DuplicateEmailAddressException();

        //TO DO: create user

        var user = new User { Email = request.Email, Username = request.Username, PasswordHash = request.Password };
        _userRepository.AddUser(user);

        return Task.FromResult(new RegisterResponse(user, _jwtGenerator.GenerateToken(user)));
    }
}