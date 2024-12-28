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
            AllowedScopes = new List<string> { "openid", "profile", "auctionApp" },
            RedirectUris = { "https://localhost:7018/oauth2/callback" },
            ClientSecrets = [new Secret("SuperSecretDooDoo".Sha256())],
            AllowedGrantTypes = [GrantType.ResourceOwnerPassword]
        }
    ];
}