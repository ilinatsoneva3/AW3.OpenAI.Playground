using AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;
using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Common.Interfaces.Services;
using AW3.GR.OpenAI.Application.Modules.Authentication.Common;
using AW3.GR.OpenAI.Domain.Common.Errors;
using AW3.GR.OpenAI.Domain.UserAggregate;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Authentication.Commands.Register;

internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<RegisterResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtGenerator jwtGenerator,
        IMapper mapper,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(request.Email) != null)
            return Errors.User.DuplicateEmail;

        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        var user = User.Create(request.Username, request.Email, hashedPassword);

        _userRepository.AddUser(user);

        return new RegisterResponse(_mapper.Map<UserDto>(user), _jwtGenerator.GenerateToken(user));
    }
}