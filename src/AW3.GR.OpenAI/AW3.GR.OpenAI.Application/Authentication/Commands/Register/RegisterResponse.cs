namespace AW3.GR.OpenAI.Application.Authentication.Commands.Register;

public sealed record RegisterResponse(Guid Id, string Email, string Username, string Token);
