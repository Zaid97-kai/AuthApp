using Blazor.Learner.Shared.Models;

namespace Blazor.Learner.Server.Services;

/// <summary>
/// Interface IUserService
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Gets the token asynchronous.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>Task&lt;AuthenticationModel&gt;.</returns>
    Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);

    /// <summary>
    /// Adds the role asynchronous.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>Task&lt;System.String&gt;.</returns>
    Task<string> AddRoleAsync(AddRoleModel model);
}