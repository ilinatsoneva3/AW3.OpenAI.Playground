using AW3.GR.OpenAI.Application.Authentication.Login;
using AW3.GR.OpenAI.Application.Authentication.Register;
using Microsoft.AspNetCore.Mvc;

namespace AW3.GR.OpenAI.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult RegisterAsync(RegisterRequest request)
    {
        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        return Ok();
    }
}
