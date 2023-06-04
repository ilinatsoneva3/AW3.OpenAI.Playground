using AW3.GR.OpenAI.API.Http;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW3.GR.OpenAI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ApiController : ControllerBase
{
    private ISender _sender;

    /// <summary>
    /// Mediator sender
    /// </summary>
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();

    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items.Add(HttpContextConstants.Errors, errors);

        var firstError = errors.FirstOrDefault();

        var statusCode = firstError.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}