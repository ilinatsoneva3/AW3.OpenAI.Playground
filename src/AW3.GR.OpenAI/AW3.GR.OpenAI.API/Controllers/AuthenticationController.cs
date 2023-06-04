using AW3.GR.OpenAI.Application.Authentication.Login;
using AW3.GR.OpenAI.Application.Authentication.Register;
using AW3.GR.OpenAI.Application.Modules.Authentication.Commands.Register;
using AW3.GR.OpenAI.Application.Modules.Authentication.Queries.Login;
using AW3.GR.OpenAI.Contracts.Authentication;
using AW3.GR.OpenAI.Domain.Common.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW3.GR.OpenAI.API.Controllers;

[AllowAnonymous]
[Route("api/auth")]
public class AuthenticationController : ApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        var result = await Sender.Send(new RegisterCommand(request.UserName, request.Email, request.Password));

        return result.Match(
            result => Ok(MapToResult(result)),
            Problem);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await Sender.Send(new LoginCommand(request.Email, request.Password));


        if (result.IsError && result.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: result.FirstError.Description);

        return result.Match(
            result => Ok(MapToResult(result)),
            Problem);
    }

    private static AuthenticationResponse MapToResult(LoginResponse result)
        => new(result.User.Id, result.User.Username, result.User.Email, result.Token);

    private static AuthenticationResponse MapToResult(RegisterResponse result)
        => new(result.User.Id, result.User.Username, result.User.Email, result.Token);
}