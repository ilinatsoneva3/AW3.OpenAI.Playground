namespace AW3.GR.OpenAI.Application.Authentication.Queries.Login;

public sealed record LoginResponse(Guid Id, string Email, string Username, string Token);
