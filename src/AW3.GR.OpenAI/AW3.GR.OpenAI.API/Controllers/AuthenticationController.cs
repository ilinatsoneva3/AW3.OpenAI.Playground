using AW3.GR.OpenAI.Application.Authentication.Commands.Register;
using AW3.GR.OpenAI.Application.Authentication.Login;
using AW3.GR.OpenAI.Application.Authentication.Queries.Login;
using AW3.GR.OpenAI.Application.Authentication.Register;
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
        return Ok(await Sender.Send(new RegisterCommand(request.UserName, request.Email, request.Password)));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return Ok(await Sender.Send(new LoginCommand(request.Email, request.Password)));
    }
}