using System.Net;
using AW3.GR.OpenAI.Application.Modules.SearchHistory.Queries.GetSearchHistoryForUser;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AW3.GR.OpenAI.API.Controllers;

public class SearchHistoryController : ApiController
{
    [HttpGet("me")]
    [SwaggerOperation("Retrieves search history for the logged user")]
    [SwaggerResponse((int)HttpStatusCode.OK, "List of search history", typeof(IEnumerable<SearchHistoryResponse>))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid request")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized")]
    public async Task<IActionResult> GetSearchHistoryAsync()
    {
        var result = await Sender.Send(new GetSearchHistoryQuery());
        return result.Match(Ok, Problem);
    }
}
