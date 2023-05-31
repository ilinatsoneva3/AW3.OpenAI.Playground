using AW3.GR.OpenAI.Application.Quotes.Commands.AskOpenAi;
using AW3.GR.OpenAI.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW3.GR.OpenAI.API.Controllers;

public class QuotesController : ApiController
{
    [HttpPost("ask-open-ai")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), HttpStatusCodes.OK)]
    [ProducesResponseType(HttpStatusCodes.BadRequest)]
    public async Task<AskOpenAiResponse> AskOpenAiAsync([FromBody] AskOpenAiQuery request)
    {
        return await Sender.Send(request);
    }
}