using AW3.GR.OpenAI.Application.Quotes.AskOpenAi;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AW3.GR.OpenAI.API.Controllers;

[ApiController]
[Route("[controller]")]
public class QuotesController : ControllerBase
{
    private readonly ISender _sender;

    public QuotesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Route("ask-open-ai")]
    public async Task<IResult> AskOpenAiAsync([FromBody] AskOpenAiQuery request)
    {
        return Results.Ok(await _sender.Send(request));

    }
}