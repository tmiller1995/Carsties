using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Services;

public sealed class CustomProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public CustomProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);
        if (user?.UserName is null)
            return;

        var existingClaims = await _userManager.GetClaimsAsync(user);
        List<Claim> claims = [new(JwtClaimTypes.PreferredUserName, user.UserName),
            new(JwtClaimTypes.Name, existingClaims.FirstOrDefault(c => c.Type == JwtClaimTypes.Name)?.Value!)];

        context.IssuedClaims.AddRange(claims);
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        return Task.CompletedTask;
    }
}