using Microsoft.AspNetCore.Identity;

namespace Blazor.Learner.Server.Models;

/// <summary>
/// Class ApplicationUser.
/// Implements the <see cref="IdentityUser" />
/// </summary>
/// <seealso cref="IdentityUser" />
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    /// <value>The first name.</value>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    /// <value>The last name.</value>
    public string LastName { get; set; }
}