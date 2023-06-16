using AW3.GR.OpenAI.Application.Modules.Authentication.Commands.Register;
using AW3.GR.OpenAI.Application.Modules.Authentication.Queries.Login;
using AW3.GR.OpenAI.Domain.Common.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW3.GR.OpenAI.API.Controllers;

[AllowAnonymous]
[Route("api/auth")]
public class AuthenticationController : ApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterCommand request)
    {
        var result = await Sender.Send(request);

        return result.Match(
            Ok,
            Problem);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand request)
    {
        var result = await Sender.Send(request);

        if (result.IsError && result.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: result.FirstError.Description);

        return result.Match(
            Ok,
            Problem);
    }
}