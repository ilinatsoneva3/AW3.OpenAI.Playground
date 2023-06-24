using AW3.GR.OpenAI.Domain.Users;

namespace AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}