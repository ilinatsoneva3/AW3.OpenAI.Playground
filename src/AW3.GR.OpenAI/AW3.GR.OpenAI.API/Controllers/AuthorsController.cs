using AW3.GR.OpenAI.Application.Modules.Authors.Queries.GetAuthors;
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
}
