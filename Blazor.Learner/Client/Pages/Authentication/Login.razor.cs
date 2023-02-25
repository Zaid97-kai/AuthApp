using Blazor.Learner.Client.Services;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Learner.Client.Pages.Authentication;

/// <summary>
/// Class Login.
/// Implements the <see cref="Microsoft.AspNetCore.Components.ComponentBase" />
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
public partial class Login
{
    /// <summary>
    /// Gets or sets the navigation manager.
    /// </summary>
    /// <value>The navigation manager.</value>
    [Inject] 
    private NavigationManager? NavigationManager { get; set; }

    /// <summary>
    /// Gets or sets the authentication state provider.
    /// </summary>
    /// <value>The authentication state provider.</value>
    [Inject] 
    private CustomStateProvider? AuthStateProvider { get; set; }

    /// <summary>
    /// Gets or sets the login request.
    /// </summary>
    /// <value>The login request.</value>
    private LoginRequest LoginRequest { get; set; } = new LoginRequest();

    /// <summary>
    /// Gets or sets the error.
    /// </summary>
    /// <value>The error.</value>
    private string? Error { get; set; }

    /// <summary>
    /// Called when [submit].
    /// </summary>
    private async Task OnSubmit()
    {
        Error = null;
        try
        {
            await AuthStateProvider?.Login(LoginRequest)!;
            NavigationManager?.NavigateTo("");
        }
        catch (Exception ex)
        {
            Error = ex.Message;
        }
    }
}