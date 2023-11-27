using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ToDoListBk.Controllers;

/// <summary>
/// Endpoint default
/// </summary>
[Route("api/[controller]")]
[Consumes("aplication/json")]
[Produces("application/json")]
[ApiController]
public class DefaultController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public string Get()
    {
        return "Run Aplication";
    }
}
