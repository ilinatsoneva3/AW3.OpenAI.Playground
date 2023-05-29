using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace AW3.GR.OpenAI.Infrastructure.Authentication;

public class JwtGenerator : IJwtGenerator
{
    public string GenerateToken(Guid userId, string username)
    {
        var signInCred = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key")),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()!),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var securityToken = new JwtSecurityToken(
            issuer: "AW3.GR.OpenAI",
            expires: DateTime.UtcNow.AddDays(1),
            claims: claims,
            signingCredentials: signInCred);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}

