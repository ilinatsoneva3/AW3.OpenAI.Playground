using AW3.GR.OpenAI.Application.Modules.Authors.Commands.CreateQuote;
using AW3.GR.OpenAI.Application.Modules.Authors.Queries.GetAuthors;
using AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAi;
using Microsoft.AspNetCore.Mvc;

namespace AW3.GR.OpenAI.API.Controllers;

public class AuthorsController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await Sender.Send(new GetAuthorsQuery());

        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPost("open-ai")]
    public async Task<IActionResult> AskOpenAiAsync([FromBody] OpenAiQuery request)
    {
        var result = await Sender.Send(request);

        return result.Match(Ok, Problem);
    }

    [HttpPost("{authorId}/quotes")]
    public async Task<IActionResult> CreateQuoteAsync([FromRoute] string authorId, [FromBody] CreateQuoteDto request)
    {
        var result = await Sender.Send(new CreateQuoteCommand(request, authorId));

        return result.Match(Ok, Problem);
    }
}
