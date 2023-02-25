namespace Blazor.Learner.Shared.Models;

/// <summary>
/// Class Developer.
/// Implements the <see cref="Blazor.Learner.Shared.Models.BaseEntity" />
/// </summary>
/// <seealso cref="Blazor.Learner.Shared.Models.BaseEntity" />
public class Developer : BaseEntity
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

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>The email.</value>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the experience.
    /// </summary>
    /// <value>The experience.</value>
    public decimal Experience { get; set; }
}