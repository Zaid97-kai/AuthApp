using Blazor.Learner.Server.Data;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace Blazor.Learner.Server;

/// <summary>
/// Class Program.
/// </summary>
public class Program
{

    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        //using (var scope = host.Services.CreateScope())
        //{
        //    var services = scope.ServiceProvider;
        //    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        //    try
        //    {
        //        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        //        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //        await ApplicationDbContextSeed.SeedEssentialsAsync(userManager, roleManager);
        //    }
        //    catch (Exception ex)
        //    {
        //        var logger = loggerFactory.CreateLogger<Program>();
        //        logger.LogError(ex, "An error occurred seeding the DB.");
        //    }
        //}

        host.Run();
    }

    /// <summary>
    /// Creates the host builder.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <returns>IHostBuilder.</returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}