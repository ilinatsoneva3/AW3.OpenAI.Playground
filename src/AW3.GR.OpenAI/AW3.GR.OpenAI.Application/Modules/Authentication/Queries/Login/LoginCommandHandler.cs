using AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;
using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Modules.Authentication.Common;
using AW3.GR.OpenAI.Domain.Common.Errors;
using AW3.GR.OpenAI.Domain.Entities;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Authentication.Queries.Login;

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<LoginResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IMapper _mapper;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IJwtGenerator jwtGenerator,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _mapper = mapper;
    }

    public async Task<ErrorOr<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(request.Email) is not User user)
            return Errors.Authentication.InvalidCredentials;

        if (user.PasswordHash != request.Password)
            return Errors.Authentication.InvalidCredentials;

        return new LoginResponse(_mapper.Map<UserDto>(user), _jwtGenerator.GenerateToken(user));
    }
}