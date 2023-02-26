using Blazor.Learner.Shared.Settings;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Json;
using Blazor.Learner.Shared.Models;

namespace Blazor.Learner.Client.Services;

/// <summary>
/// Class AuthService.
/// Implements the <see cref="Blazor.Learner.Client.Services.IAuthService" />
/// </summary>
/// <seealso cref="Blazor.Learner.Client.Services.IAuthService" />
public class AuthService : IAuthService
{
    /// <summary>
    /// The HTTP client
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthService" /> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Currents the user information.
    /// </summary>
    /// <returns>CurrentUser.</returns>
    public async Task<CurrentUser?> CurrentUserInfo()
    {
        var result = await _httpClient.GetFromJsonAsync<CurrentUser>("api/auth/currentuserinfo");
        return result;
    }

    /// <summary>
    /// Logins the specified login request.
    /// </summary>
    /// <param name="loginRequest">The login request.</param>
    /// <returns>Task.</returns>
    /// <exception cref="System.Exception"></exception>
    public async Task Login(LoginRequest loginRequest)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);
        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            throw new Exception(await result.Content.ReadAsStringAsync());
        result.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Logouts this instance.
    /// </summary>
    /// <returns>Task.</returns>
    public async Task Logout()
    {
        var result = await _httpClient.PostAsync("api/auth/logout", null);
        result.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Registers the specified register request.
    /// </summary>
    /// <param name="registerRequest">The register request.</param>
    /// <returns>Task.</returns>
    /// <exception cref="System.Exception"></exception>
    public async Task Register(RegisterRequest registerRequest)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/register", registerRequest);
        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            throw new Exception(await result.Content.ReadAsStringAsync());
        result.EnsureSuccessStatusCode();
    }
}