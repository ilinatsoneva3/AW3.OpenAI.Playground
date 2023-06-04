using AW3.GR.OpenAI.Domain.Entities;

namespace AW3.GR.OpenAI.Application.Modules.Authentication.Commands.Register;

public sealed record RegisterResponse(User User, string Token);