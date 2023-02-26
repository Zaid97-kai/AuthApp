using Blazor.Learner.Server.Constants;
using Blazor.Learner.Server.Services;
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
    /// The user service
    /// </summary>
    private readonly IUserService _userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController" /> class.
    /// </summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="signInManager">The sign in manager.</param>
    /// <param name="userService">The user service.</param>
    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserService userService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userService = userService;
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

        switch (result.Succeeded)
        {
            case false:
                return BadRequest(result.Errors.FirstOrDefault()?.Description);
            case true:
                await _userManager.AddToRoleAsync(user, Authorization.DefaultRole.ToString());
                break;
        }

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

    /// <summary>
    /// Get token as an asynchronous operation.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A Task&lt;IActionResult&gt; representing the asynchronous operation.</returns>
    [HttpPost("token")]
    public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
    {
        var result = await _userService.GetTokenAsync(model);
        return Ok(result);
    }
}