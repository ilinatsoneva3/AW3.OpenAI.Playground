using System.Security.Claims;
using AW3.GR.OpenAI.Application.Common.Interfaces.Services;
using AW3.GR.OpenAI.Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace AW3.GR.OpenAI.Infrastructure.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContext;

    public UserContextService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public UserId? UserId => User is null ?
        null :
        UserId.Create(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!));

    public ClaimsPrincipal? User => _httpContext.HttpContext?.User;
}
