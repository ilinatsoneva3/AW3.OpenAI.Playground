using System.Net;
using AW3.GR.OpenAI.Application.Modules.Authors.Commands.CreateQuote;
using AW3.GR.OpenAI.Application.Modules.Authors.DTOs;
using AW3.GR.OpenAI.Application.Modules.Authors.Queries.GetAuthors;
using AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAI;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AW3.GR.OpenAI.API.Controllers;

public class AuthorsController : ApiController
{
    [HttpGet]
    [SwaggerOperation("Get list of authors")]
    [SwaggerResponse((int)HttpStatusCode.OK, "List of authors", typeof(IEnumerable<AuthorDTO>))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid request")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await Sender.Send(new GetAuthorsQuery());

        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPost("open-ai")]
    [SwaggerOperation("Send the name of the author to OpenAI and see one of his famous quotes")]
    [SwaggerResponse((int)HttpStatusCode.OK, "List of authors", typeof(OpenAIResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid request")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized")]
    public async Task<IActionResult> AskOpenAiAsync([FromBody] OpenAIQuery request)
    {
        var result = await Sender.Send(request);

        return result.Match(Ok, Problem);
    }

    [HttpPost("{authorId}/quotes")]
    [SwaggerOperation("Create a new quote for the author with the given id")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Author profile", typeof(AuthorDTO))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid request")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized")]
    public async Task<IActionResult> CreateQuoteAsync([FromRoute] string authorId, [FromBody] CreateQuoteDto request)
    {
        var result = await Sender.Send(new CreateQuoteCommand(request, authorId));

        return result.Match(Ok, Problem);
    }
}
