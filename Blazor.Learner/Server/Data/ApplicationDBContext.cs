using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Learner.Server.Data;

/// <summary>
/// Class ApplicationDBContext.
/// Implements the <see cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext{Blazor.Learner.Server.Models.ApplicationUser}" />
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext{Blazor.Learner.Server.Models.ApplicationUser}" />
public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDBContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the developers.
    /// </summary>
    /// <value>The developers.</value>
    public DbSet<Developer> Developers { get; set; }
}