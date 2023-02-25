namespace Blazor.Learner.Shared.Models;

/// <summary>
/// Class CurrentUser.
/// </summary>
public class CurrentUser
{
    /// <summary>
    /// Gets or sets a value indicating whether this instance is authenticated.
    /// </summary>
    /// <value><c>true</c> if this instance is authenticated; otherwise, <c>false</c>.</value>
    public bool IsAuthenticated { get; set; }

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    /// <value>The name of the user.</value>
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the claims.
    /// </summary>
    /// <value>The claims.</value>
    public Dictionary<string, string> Claims { get; set; }
}