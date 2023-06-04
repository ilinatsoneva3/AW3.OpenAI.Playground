using ErrorOr;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Authentication.Commands.Register;

public record RegisterCommand(
    string Username,
    string Email,
    string Password) : IRequest<ErrorOr<RegisterResponse>>;