using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new("auctionApp", "Auction App API Full Access")
    ];

    public static IEnumerable<Client> Clients =>
    [
        new()
        {
            ClientId = "bruno",
            ClientName = "Bruno",
            AllowedScopes = ["openid", "profile", "auctionApp"],
            RedirectUris = ["http://localhost:7018/oauth2/callback"],
            ClientSecrets = [new Secret("SuperSecretDooDoo".Sha256())],
            AllowedGrantTypes = [GrantType.ResourceOwnerPassword]
        },
        new()
        {
            ClientId = "frontend",
            ClientName = "Carsties Frontend",
            ClientSecrets = [new Secret("SuperSecretDooDooDoo".Sha256())],
            AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
            RequirePkce = false,
            RedirectUris = ["http://localhost:3000/api/auth/callback/id-server"],
            AllowOfflineAccess = true,
            AllowedScopes = ["openid", "profile", "auctionApp"],
            AccessTokenLifetime = 3600 * 24 * 30,
            AlwaysIncludeUserClaimsInIdToken = true
        }
    ];
}