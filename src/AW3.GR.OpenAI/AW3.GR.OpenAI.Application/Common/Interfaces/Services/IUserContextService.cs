using System.Security.Claims;
using AW3.GR.OpenAI.Domain.Users.ValueObjects;

namespace AW3.GR.OpenAI.Application.Common.Interfaces.Services;

public interface IUserContextService
{
    UserId? UserId { get; }

    ClaimsPrincipal? User { get; }
}
