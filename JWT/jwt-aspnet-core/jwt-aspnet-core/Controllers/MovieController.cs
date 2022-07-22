using jwt_aspnet_core.Models;
using Microsoft.AspNetCore.Mvc;

namespace jwt_aspnet_core.Controllers;

[Route("api/movie")]
[ApiController]
public class MovieController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] Movie movie)
    {
        if (movie == null) return BadRequest("Movie object is null");
        
        if(!ModelState.IsValid) return BadRequest(ModelState);

        return Ok();
    }
}