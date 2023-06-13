using AW3.GR.OpenAI.Application.Modules.Authentication.Common;

namespace AW3.GR.OpenAI.Application.Modules.Authentication.Queries.Login;

public sealed record LoginResponse(UserDto User, string Token);