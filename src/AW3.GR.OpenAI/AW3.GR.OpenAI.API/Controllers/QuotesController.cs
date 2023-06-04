using AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW3.GR.OpenAI.API.Controllers;

public class QuotesController : ApiController
{
    [HttpPost("ask-open-ai")]
    [AllowAnonymous]
    public async Task<AskOpenAiResponse> AskOpenAiAsync([FromBody] AskOpenAiQuery request)
    {
        return await Sender.Send(request);
    }
}