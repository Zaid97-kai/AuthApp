using Blazor.Learner.Server.Data;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Learner.Server.Controllers;

/// <summary>
/// Class DeveloperController.
/// Implements the <see cref="ControllerBase" />
/// </summary>
/// <seealso cref="ControllerBase" />
[Route("api/[controller]")]
[ApiController]
public class DeveloperController : ControllerBase
{
    /// <summary>
    /// The context
    /// </summary>
    private readonly ApplicationDBContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeveloperController"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public DeveloperController(ApplicationDBContext context)
    {
        this._context = context;
    }

    /// <summary>
    /// Gets this instance.
    /// </summary>
    /// <returns>IActionResult.</returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var devs = await _context.Developers.ToListAsync();
        return Ok(devs);
    }

    /// <summary>
    /// Gets the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>IActionResult.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var dev = await _context.Developers.FirstOrDefaultAsync(a => a.Id == id);
        return Ok(dev);
    }

    /// <summary>
    /// Posts the specified developer.
    /// </summary>
    /// <param name="developer">The developer.</param>
    /// <returns>IActionResult.</returns>
    [HttpPost]
    public async Task<IActionResult> Post(Developer developer)
    {
        _context.Add(developer);
        await _context.SaveChangesAsync();
        return Ok(developer.Id);
    }

    /// <summary>
    /// Puts the specified developer.
    /// </summary>
    /// <param name="developer">The developer.</param>
    /// <returns>IActionResult.</returns>
    [HttpPut]
    public async Task<IActionResult> Put(Developer developer)
    {
        _context.Entry(developer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// Deletes the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>IActionResult.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var dev = new Developer { Id = id };
        _context.Remove(dev);
        await _context.SaveChangesAsync();
        return NoContent();
    }

}