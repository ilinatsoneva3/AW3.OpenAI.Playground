using AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAi;
using AW3.GR.OpenAI.Application.Modules.Quotes.Commands.CreateQuoteCommand;
using Microsoft.AspNetCore.Mvc;

namespace AW3.GR.OpenAI.API.Controllers;

public class QuotesController : ApiController
{
    [HttpPost("open-ai")]
    public async Task<IActionResult> AskOpenAiAsync([FromBody] OpenAiQuery request)
    {
        var result = await Sender.Send(request);

        return result.Match(Ok, Problem);
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuoteAsync([FromBody] CreateQuoteCommand request)
    {
        var result = await Sender.Send(request);

        return result.Match(Ok, Problem);
    }
}