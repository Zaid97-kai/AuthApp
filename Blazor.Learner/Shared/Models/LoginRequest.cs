using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blazor.Learner.Shared.Models;

/// <summary>
/// Class LoginRequest.
/// </summary>
public class LoginRequest
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
    /// Gets or sets a value indicating whether [remember me].
    /// </summary>
    /// <value><c>true</c> if [remember me]; otherwise, <c>false</c>.</value>
    public bool RememberMe { get; set; }
}