using MediatR;

namespace AW3.GR.OpenAI.Application.Authentication.Queries.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;