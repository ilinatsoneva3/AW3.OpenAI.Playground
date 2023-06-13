using AW3.GR.OpenAI.Application.Modules.Authentication.Common;

namespace AW3.GR.OpenAI.Application.Modules.Authentication.Commands.Register;

public sealed record RegisterResponse(UserDto User, string Token);