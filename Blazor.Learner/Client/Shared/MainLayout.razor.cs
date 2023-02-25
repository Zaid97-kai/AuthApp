using Blazor.Learner.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System;

namespace Blazor.Learner.Client.Shared;

/// <summary>
/// Class MainLayout.
/// Implements the <see cref="LayoutComponentBase" />
/// </summary>
/// <seealso cref="LayoutComponentBase" />
public partial class MainLayout
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
    /// Gets or sets the state of the authentication.
    /// </summary>
    /// <value>The state of the authentication.</value>
    [CascadingParameter] 
    private Task<AuthenticationState>? AuthenticationState { get; set; }

    /// <summary>
    /// On parameters set as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnParametersSetAsync()
    {
        var userIdentity = (await AuthenticationState!).User.Identity;
        if (userIdentity is { IsAuthenticated: false })
        {
            NavigationManager!.NavigateTo("/login");
        }
    }

    /// <summary>
    /// Logouts the click.
    /// </summary>
    private async Task LogoutClick()
    {
        await AuthStateProvider?.Logout()!;
        NavigationManager?.NavigateTo("/login");
    }
}