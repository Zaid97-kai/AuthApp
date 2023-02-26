using System.ComponentModel.DataAnnotations;

namespace Blazor.Learner.Shared.Models;

/// <summary>
/// Class AddRoleModel.
/// </summary>
public class AddRoleModel
{
    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>The email.</value>
    [Required]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    /// <value>The password.</value>
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the role.
    /// </summary>
    /// <value>The role.</value>
    [Required]
    public string Role { get; set; }
}