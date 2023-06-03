using AW3.GR.OpenAI.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AW3.GR.OpenAI.API.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, title) = exception switch
        {
            IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.Message),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };

        return Problem(title: title, statusCode: statusCode);
    }
}
