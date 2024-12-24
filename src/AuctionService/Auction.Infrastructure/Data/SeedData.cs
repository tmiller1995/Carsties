using Auction.Domain.Auctions;
using Auction.Domain.Items;
using Bogus;

namespace Auction.Infrastructure.Data;

public static class SeedData
{
    private static readonly HashSet<string> UserNames =
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

    public static List<AuctionEntity> GenerateAuctions()
    {
        Randomizer.Seed = new Random(2_145);

        var auctionFaker = new Faker<AuctionEntity>("en_US")
            .RuleFor(a => a.Id, f => Guid.CreateVersion7())
            .RuleFor(a => a.Status, f => f.PickRandom<Status>())
            .RuleFor(a => a.ReservePrice, f => f.Finance.Random.Decimal(10_000m, 600_000m))
            .RuleFor(a => a.Seller, f => f.Random.CollectionItem(UserNames))
            .RuleFor(a => a.AuctionEnd, f => DateTime.SpecifyKind(f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddMonths(4)), DateTimeKind.Utc))
            .RuleFor(a => a.Winner, f => f.PickRandom<string>(UserNames).OrNull(f, 0.8f))
            .RuleFor(a => a.ItemEntity, f =>
            {
                var vehicle = f.Vehicle;
                return new ItemEntity(vehicle.Manufacturer(),
                    vehicle.Model(),
                    f.Random.Int(1900, 2024),
                    f.Commerce.Color(),
                    vehicle.Random.Int(0, 350_001),
                    f.Image.PlaceholderUrl(150, 150));
            });

        var auctions = auctionFaker.Generate(1_000);
        auctions.ForEach(a => a.GetCreatedEvent());

        return auctions;
    }
}