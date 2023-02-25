using Blazor.Learner.Client.Services;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Learner.Client.Pages.Authentication;

/// <summary>
/// Class Register.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class Register
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
    /// Gets or sets the register request.
    /// </summary>
    /// <value>The register request.</value>
    private RegisterRequest RegisterRequest { get; set; } = new RegisterRequest();

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
            await AuthStateProvider?.Register(RegisterRequest)!;
            NavigationManager?.NavigateTo("");
        }
        catch (Exception ex)
        {
            Error = ex.Message;
        }
    }
}