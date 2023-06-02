using AW3.GR.OpenAI.Domain.Entities;

namespace AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}