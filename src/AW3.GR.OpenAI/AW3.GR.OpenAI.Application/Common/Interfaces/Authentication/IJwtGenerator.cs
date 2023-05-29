namespace AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;

public interface IJwtGenerator
{
    string GenerateToken(Guid userId, string username);
}
