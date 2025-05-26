using System.Security.Claims;
using Bogus;
using IdentityModel;
using IdentityService.Data;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityService;

public static class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationUserIdentityDbContext>();
        context.Database.Migrate();

        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        if (userMgr.Users.Any())
            return;

        foreach (var (user, claims) in GetUsersWithClaims())
        {
            var existingUser = userMgr.FindByNameAsync(user.UserName!).Result;
            if (existingUser == null)
            {
                var result = userMgr.CreateAsync(user, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(user, claims).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("Created {Username}", user.UserName);
            }
            else
            {
                Log.Debug("User {Username} already exists", user.UserName);
            }
        }
    }

    private static Dictionary<ApplicationUser, List<Claim>> GetUsersWithClaims()
    {
        HashSet<string> userNames =
        [
            "PixelPhantom",
            "ShadowHunter88",
            "GalacticNova",
            "MysticFalcon",
            "CyberWolf42",
            "FrostedNinja",
            "LunarEclipse",
            "ElectricVortex",
            "TurboTornado",
            "CrimsonPhoenix",
            "NeonSpecter",
            "QuantumKnight",
            "SilentBlaze",
            "CosmicRaven",
            "ThunderFury",
            "BlazeTitan",
            "MidnightCyclone",
            "PhantomOrbit",
            "FrostbiteX",
            "VividSpectrum",
            "TurboGamerX",
            "GalaxyRaider",
            "SolarFlareX",
            "MysticHorizon",
            "ElectricPulse",
            "CyberGlitch",
            "DarkMatter99",
            "CosmicSurge",
            "NovaBlaster",
            "FrozenInferno",
            "PhantomPixel",
            "ShadowByte",
            "TurboNova",
            "LunarDrifter",
            "FrostyStorm",
            "ElectricFury",
            "MysticRogue",
            "CrimsonTide",
            "ThunderNova",
            "SolarRogue",
            "FrostWolf",
            "CosmicStriker",
            "PhantomRaven",
            "DarkNova",
            "TurboEclipse",
            "GalacticPulse",
            "ShadowTitan",
            "MidnightPulse"
        ];

        var users = new Dictionary<ApplicationUser, List<Claim>>();

        foreach (var userName in userNames)
        {
            var faker = new Faker();
            var appUser = new ApplicationUser
            {
                UserName = userName,
                Email = faker.Internet.Email(),
                EmailConfirmed = true
            };
            var appUserFullName = faker.Name.FullName();
            var appUserFirstName = appUserFullName[..appUserFullName.IndexOf(' ')];
            var appUserLastName = appUserFullName[(appUserFullName.IndexOf(' ') + 1)..];
            users.Add(appUser, [
                new Claim(JwtClaimTypes.Name, appUserFullName),
                new Claim(JwtClaimTypes.GivenName, appUserFirstName),
                new Claim(JwtClaimTypes.FamilyName, appUserLastName)
            ]);
        }

        return users;
    }
}