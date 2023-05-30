using MediatR;

namespace AW3.GR.OpenAI.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string Username,
    string Email,
    string Password) : IRequest<RegisterResponse>;