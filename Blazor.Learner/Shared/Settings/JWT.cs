namespace Blazor.Learner.Shared.Settings;

/// <summary>
/// Class JWT.
/// </summary>
public class JWT
{
    /// <summary>
    /// Gets or sets the key.
    /// </summary>
    /// <value>The key.</value>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the issuer.
    /// </summary>
    /// <value>The issuer.</value>
    public string Issuer { get; set; }

    /// <summary>
    /// Gets or sets the audience.
    /// </summary>
    /// <value>The audience.</value>
    public string Audience { get; set; }

    /// <summary>
    /// Gets or sets the duration in minutes.
    /// </summary>
    /// <value>The duration in minutes.</value>
    public double DurationInMinutes { get; set; }
}