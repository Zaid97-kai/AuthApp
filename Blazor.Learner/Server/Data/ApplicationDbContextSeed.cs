using Blazor.Learner.Server.Constants;
using Blazor.Learner.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace Blazor.Learner.Server.Data;

/// <summary>
/// Class ApplicationDbContextSeed.
/// </summary>
public class ApplicationDbContextSeed
{
    /// <summary>
    /// Seed essentials as an asynchronous operation.
    /// </summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="roleManager">The role manager.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public static async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Moderator.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));

        var defaultUser = new ApplicationUser
        {
            UserName = Authorization.DefaultUsername, 
            Email = Authorization.DefaultEmail, 
            EmailConfirmed = true, 
            PhoneNumberConfirmed = true
        };

        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            await userManager.CreateAsync(defaultUser, Authorization.DefaultPassword);
            await userManager.AddToRoleAsync(defaultUser, Authorization.DefaultRole.ToString());
        }
    }
}