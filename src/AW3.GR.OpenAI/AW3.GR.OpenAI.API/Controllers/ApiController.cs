using AW3.GR.OpenAI.API.Http;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW3.GR.OpenAI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ApiController : ControllerBase
{
    private ISender? _sender;
    private IMapper? _mapper;

    /// <summary>
    /// Mediator sender
    /// </summary>
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>() ?? throw new ArgumentNullException(nameof(ISender));

    /// <summary>
    /// Mapster mapper
    /// </summary>
    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>() ?? throw new ArgumentNullException(nameof(IMapper));

    /// <summary>
    /// Custom implementation of <see cref="BadRequestObjectResult"/> that returns <see cref="ProblemDetails"/> with <see cref="Error"/>s
    /// </summary>
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