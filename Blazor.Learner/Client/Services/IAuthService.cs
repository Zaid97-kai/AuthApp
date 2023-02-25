using Blazor.Learner.Shared.Models;

namespace Blazor.Learner.Client.Services;

/// <summary>
/// Interface IAuthService
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Logins the specified login request.
    /// </summary>
    /// <param name="loginRequest">The login request.</param>
    /// <returns>Task.</returns>
    Task Login(LoginRequest loginRequest);

    /// <summary>
    /// Registers the specified register request.
    /// </summary>
    /// <param name="registerRequest">The register request.</param>
    /// <returns>Task.</returns>
    Task Register(RegisterRequest registerRequest);

    /// <summary>
    /// Logouts this instance.
    /// </summary>
    /// <returns>Task.</returns>
    Task Logout();

    /// <summary>
    /// Currents the user information.
    /// </summary>
    /// <returns>Task&lt;System.Nullable&lt;CurrentUser&gt;&gt;.</returns>
    Task<CurrentUser?> CurrentUserInfo();
}