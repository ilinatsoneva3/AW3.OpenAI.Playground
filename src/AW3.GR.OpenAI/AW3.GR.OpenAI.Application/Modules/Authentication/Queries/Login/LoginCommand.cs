using ErrorOr;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Authentication.Queries.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<ErrorOr<LoginResponse>>;