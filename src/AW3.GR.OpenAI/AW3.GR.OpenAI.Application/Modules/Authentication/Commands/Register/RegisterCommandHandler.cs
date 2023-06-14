using AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;
using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Modules.Authentication.Common;
using AW3.GR.OpenAI.Domain.Common.Errors;
using AW3.GR.OpenAI.Domain.Entities;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Authentication.Commands.Register;

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<RegisterResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtGenerator jwtGenerator,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _mapper = mapper;
    }

    public async Task<ErrorOr<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(request.Email) != null)
            return Errors.User.DuplicateEmail;

        var user = User.Create(request.Username, request.Email, request.Password);

        _userRepository.AddUser(user);

        return new RegisterResponse(_mapper.Map<UserDto>(user), _jwtGenerator.GenerateToken(user));
    }
}