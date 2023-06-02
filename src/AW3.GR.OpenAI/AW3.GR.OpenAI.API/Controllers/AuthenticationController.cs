using AW3.GR.OpenAI.Application.Authentication.Commands.Register;
using AW3.GR.OpenAI.Application.Authentication.Login;
using AW3.GR.OpenAI.Application.Authentication.Queries.Login;
using AW3.GR.OpenAI.Application.Authentication.Register;
using AW3.GR.OpenAI.Contracts.Authentication;
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

        return Ok(new AuthenticationResponse(result.User.Id,
                                             result.User.Username,
                                             result.User.Email,
                                             result.Token));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await Sender.Send(new LoginCommand(request.Email, request.Password));

        return Ok(new AuthenticationResponse(result.User.Id,
                                             result.User.Username,
                                             result.User.Email,
                                             result.Token));
    }
}