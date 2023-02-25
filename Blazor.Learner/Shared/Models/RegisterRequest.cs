using System.ComponentModel.DataAnnotations;

namespace Blazor.Learner.Shared.Models;

/// <summary>
/// Class RegisterRequest.
/// </summary>
public class RegisterRequest
{
    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    /// <value>The name of the user.</value>
    [Required]
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    /// <value>The password.</value>
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the password confirm.
    /// </summary>
    /// <value>The password confirm.</value>
    [Required]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
    public string PasswordConfirm { get; set; }
}