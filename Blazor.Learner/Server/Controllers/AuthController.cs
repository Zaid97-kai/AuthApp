﻿using Blazor.Learner.Server.Models;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Learner.Server.Controllers;

/// <summary>
/// Class AuthController.
/// Implements the <see cref="ControllerBase" />
/// </summary>
/// <seealso cref="ControllerBase" />
[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    /// <summary>
    /// The user manager
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary>
    /// The sign in manager
    /// </summary>
    private readonly SignInManager<ApplicationUser> _signInManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="signInManager">The sign in manager.</param>
    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    /// <summary>
    /// Logins the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>IActionResult.</returns>
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null) return BadRequest("User does not exist");
        var singInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!singInResult.Succeeded) return BadRequest("Invalid password");
        await _signInManager.SignInAsync(user, request.RememberMe);
        return Ok();
    }

    /// <summary>
    /// Registers the specified parameters.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    /// <returns>IActionResult.</returns>
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest parameters)
    {
        var user = new ApplicationUser();
        user.UserName = parameters.UserName;
        var result = await _userManager.CreateAsync(user, parameters.Password);
        if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

        return await Login(new LoginRequest
        {
            UserName = parameters.UserName,
            Password = parameters.Password
        });
    }

    /// <summary>
    /// Logouts this instance.
    /// </summary>
    /// <returns>IActionResult.</returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    /// <summary>
    /// Currents the user information.
    /// </summary>
    /// <returns>CurrentUser.</returns>
    [HttpGet]
    public CurrentUser CurrentUserInfo()
    {
        return new CurrentUser
        {
            IsAuthenticated = User.Identity!.IsAuthenticated,
            UserName = User.Identity.Name!,
            Claims = User.Claims
                .ToDictionary(c => c.Type, c => c.Value)
        };
    }
}