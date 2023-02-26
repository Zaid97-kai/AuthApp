using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Learner.Server.Controllers;

/// <summary>
/// Class SecuredController.
/// Implements the <see cref="ControllerBase" />
/// </summary>
/// <seealso cref="ControllerBase" />
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SecuredController : ControllerBase
{
    /// <summary>
    /// Gets the secured data.
    /// </summary>
    /// <returns>IActionResult.</returns>
    [HttpGet]
    public async Task<IActionResult> GetSecuredData()
    {
        return Ok("This Secured Data is available only for Authenticated Users.");
    }

    /// <summary>
    /// Posts the secured data.
    /// </summary>
    /// <returns>IActionResult.</returns>
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> PostSecuredData()
    {
        return Ok("This Secured Data is available only for Authenticated Users.");
    }
}