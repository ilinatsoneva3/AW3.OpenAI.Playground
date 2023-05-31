namespace AW3.GR.OpenAI.Contracts.Authentication;

public record AuthenticationResponse(
    Guid Id,
    string Username,
    string Email,
    string Token);