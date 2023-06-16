using AW3.GR.OpenAI.Application.Modules.SearchHistory.Queries.GetSearchHistoryForUser;
using Microsoft.AspNetCore.Mvc;

namespace AW3.GR.OpenAI.API.Controllers;

public class SearchHistoryController : ApiController
{
    [HttpGet("me")]
    public async Task<IActionResult> GetSearchHistoryAsync()
    {
        var result = await Sender.Send(new GetSearchHistoryQuery());
        return result.Match(Ok, Problem);
    }
}
