using Blazor.Learner.Server.Constants;
using Blazor.Learner.Server.Services;
using Blazor.Learner.Shared.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
    /// The configuration
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController" /> class.
    /// </summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="signInManager">The sign in manager.</param>
    /// <param name="userService">The user service.</param>
    /// <param name="configuration">The configuration.</param>
    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserService userService, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userService = userService;
        _configuration = configuration;
    }

    /// <summary>
    /// Logins the specified model.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>IActionResult.</returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        await _signInManager.SignInAsync(user, request.RememberMe);

        if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }

    /// <summary>
    /// Registers the specified parameters.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    /// <returns>IActionResult.</returns>
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest parameters)
    {
        var user = new ApplicationUser
        {
            UserName = parameters.UserName
        };
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
            Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value)
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

    /// <summary>
    /// Add role as an asynchronous operation.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A Task&lt;IActionResult&gt; representing the asynchronous operation.</returns>
    [HttpPost("addrole")]
    public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
    {
        var result = await _userService.AddRoleAsync(model);
        return Ok(result);
    }
}