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

    private static readonly Dictionary<string, List<string>> CarModels = new()
    {
        { "Toyota", ["Camry", "RAV4", "Corolla"] },
        { "Ford", ["F-150", "Mustang", "Escape"] },
        { "Chevrolet", ["Silverado", "Equinox", "Malibu"] },
        { "Honda", ["Civic", "CR-V", "Accord"] },
        { "Nissan", ["Rogue", "Altima", "Sentra"] },
        { "Jeep", ["Wrangler", "Grand Cherokee", "Cherokee"] },
        { "Hyundai", ["Elantra", "Sonata", "Santa Fe"] },
        { "Kia", ["Optima", "Sorento", "Sportage"] },
        { "Subaru", ["Outback", "Forester", "Crosstrek"] },
        { "Tesla", ["Model 3", "Model S", "Model X", "Model Y"] }
    };

    public static List<AuctionEntity> GenerateAuctions()
    {
        Randomizer.Seed = new Random(2_145);

        var auctionFaker = new Faker<AuctionEntity>("en_US")
            .RuleFor(a => a.Id, f => Guid.CreateVersion7())
            .RuleFor(a => a.Status, f => f.PickRandom<Status>())
            .RuleFor(a => a.ReservePrice, f => f.Finance.Random.Number(10_000, 600_000))
            .RuleFor(a => a.Seller, f => f.PickRandom<string>(UserNames))
            .RuleFor(a => a.AuctionEnd, f => DateTime.SpecifyKind(f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddMonths(4)), DateTimeKind.Utc))
            .RuleFor(a => a.Winner, f => f.PickRandom<string>(UserNames).OrNull(f, 0.8f))
            .RuleFor(a => a.ItemEntity, f =>
            {
                var randomManufacturer = f.PickRandom<string>(CarModels.Keys);
                var randomModel = f.PickRandom<string>(CarModels[randomManufacturer]);

                return new ItemEntity(randomManufacturer,
                    randomModel,
                    f.Random.Int(2005, 2024),
                    f.Commerce.Color(),
                    f.Random.Int(0, 350_001),
                    f.Image.PlaceholderUrl(150, 150));
            });

        var auctions = auctionFaker.Generate(1_000_000);

        return auctions;
    }
}