namespace Blazor.Learner.Server.Constants;

/// <summary>
/// Class Authorization.
/// </summary>
public class Authorization
{
    /// <summary>
    /// Enum Roles
    /// </summary>
    public enum Roles
    {
        /// <summary>
        /// The administrator
        /// </summary>
        Administrator,
        /// <summary>
        /// The moderator
        /// </summary>
        Moderator,
        /// <summary>
        /// The user
        /// </summary>
        User
    }

    /// <summary>
    /// The default username
    /// </summary>
    public const string DefaultUsername = "user";

    /// <summary>
    /// The default email
    /// </summary>
    public const string DefaultEmail = "user@secureapi.com";

    /// <summary>
    /// The default password
    /// </summary>
    public const string DefaultPassword = "Pa$$w0rd.";

    /// <summary>
    /// The default role
    /// </summary>
    public const Roles DefaultRole = Roles.User;
}