using AW3.GR.OpenAI.Domain.Entities;

namespace AW3.GR.OpenAI.Application.Authentication.Queries.Login;

public sealed record LoginResponse(User User, string Token);