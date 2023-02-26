using Blazor.Learner.Shared.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blazor.Learner.Client.Services;

/// <summary>
/// Class CustomStateProvider.
/// Implements the <see cref="AuthenticationStateProvider" />
/// </summary>
/// <seealso cref="AuthenticationStateProvider" />
public class CustomStateProvider : AuthenticationStateProvider
{
    /// <summary>
    /// The API
    /// </summary>
    private readonly IAuthService _api;

    /// <summary>
    /// The current user
    /// </summary>
    private CurrentUser? _currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomStateProvider"/> class.
    /// </summary>
    /// <param name="api">The API.</param>
    public CustomStateProvider(IAuthService api)
    {
        this._api = api;
    }

    /// <summary>
    /// Get authentication state as an asynchronous operation.
    /// </summary>
    /// <returns>A Task&lt;AuthenticationState&gt; representing the asynchronous operation.</returns>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        try
        {
            var userInfo = await GetCurrentUser();
            if (userInfo!.IsAuthenticated)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, _currentUser?.UserName!)
                }.Concat(_currentUser?.Claims.Select(c => new Claim(c.Key, c.Value))!);
                identity = new ClaimsIdentity(claims, "Server authentication");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Request failed:" + ex.ToString());
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    /// <summary>
    /// Gets the current user.
    /// </summary>
    /// <returns>System.Nullable&lt;CurrentUser&gt;.</returns>
    private async Task<CurrentUser?> GetCurrentUser()
    {
        if (_currentUser is { IsAuthenticated: true }) return _currentUser;
        _currentUser = await _api.CurrentUserInfo();
        return _currentUser;
    }

    /// <summary>
    /// Logouts this instance.
    /// </summary>
    public async Task Logout()
    {
        await _api.Logout();
        _currentUser = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    /// <summary>
    /// Logins the specified login parameters.
    /// </summary>
    /// <param name="loginParameters">The login parameters.</param>
    public async Task Login(LoginRequest loginParameters)
    {
        await _api.Login(loginParameters);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    /// <summary>
    /// Registers the specified register parameters.
    /// </summary>
    /// <param name="registerParameters">The register parameters.</param>
    public async Task Register(RegisterRequest registerParameters)
    {
        await _api.Register(registerParameters);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}