using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Learner.Server.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SecuredController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetSecuredData()
    {
        return Ok("This Secured Data is available only for Authenticated Users.");
    }
}