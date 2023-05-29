using AW3.GR.OpenAI.Application.Quotes.AskOpenAi;
using AW3.GR.OpenAI.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AW3.GR.OpenAI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuotesController : ControllerBase
{
    private readonly ISender _sender;

    public QuotesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("ask-open-ai")]
    [ProducesResponseType(typeof(string), HttpStatusCodes.OK)]
    [ProducesResponseType(HttpStatusCodes.BadRequest)]
    public async Task<AskOpenAiResponse> AskOpenAiAsync([FromBody] AskOpenAiQuery request)
    {
        return await _sender.Send(request);

    }
}