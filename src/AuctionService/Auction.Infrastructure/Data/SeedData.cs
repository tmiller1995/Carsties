using System.Text.RegularExpressions;
using Auction.Domain.Auctions;
using Auction.Domain.Interfaces;
using Auction.Domain.Items;
using Bogus;

namespace Auction.Infrastructure.Data;

public sealed partial class SeedData
{
    private readonly IAiImageService _imageService;

    public SeedData(IAiImageService imageService)
    {
        _imageService = imageService;
    }

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

    private static readonly Dictionary<string, string[]> CarModels = new()
    {
        {
            "Toyota",
            [
                "Camry", "RAV4", "Corolla", "Highlander", "Tacoma", "Tundra", "4Runner", "Prius", "Sienna", "Avalon",
                "Venza", "GR86", "Supra", "Sequoia", "Land Cruiser"
            ]
        },
        {
            "Ford",
            [
                "F-150", "Mustang", "Escape", "Explorer", "Bronco", "Ranger", "Edge", "Expedition", "Maverick", "F-250",
                "F-350", "Transit", "Mach-E", "Bronco Sport", "EcoSport"
            ]
        },
        {
            "Chevrolet",
            [
                "Silverado", "Equinox", "Malibu", "Tahoe", "Suburban", "Traverse", "Colorado", "Camaro", "Blazer",
                "Trailblazer", "Corvette", "Bolt", "Express", "Trax", "Spark"
            ]
        },
        {
            "Honda",
            [
                "Civic", "CR-V", "Accord", "Pilot", "Odyssey", "HR-V", "Ridgeline", "Passport", "Insight", "Fit",
                "Clarity", "Element", "CR-Z", "S2000", "Prelude"
            ]
        },
        {
            "Nissan",
            [
                "Rogue", "Altima", "Sentra", "Frontier", "Pathfinder", "Murano", "Maxima", "Kicks", "Titan", "Armada",
                "Leaf", "Versa", "370Z", "GT-R", "Juke"
            ]
        },
        {
            "Jeep",
            [
                "Wrangler", "Grand Cherokee", "Cherokee", "Compass", "Renegade", "Gladiator", "Grand Wagoneer",
                "Wagoneer", "Commander", "Liberty", "Patriot", "Wrangler 4xe", "Grand Cherokee L", "Avenger", "Recon"
            ]
        },
        {
            "Hyundai",
            [
                "Elantra", "Sonata", "Santa Fe", "Tucson", "Kona", "Palisade", "Venue", "Accent", "Ioniq", "Nexo",
                "Veloster", "Genesis", "Ioniq 5", "Ioniq 6", "Santa Cruz"
            ]
        },
        {
            "Kia",
            [
                "Optima", "Sorento", "Sportage", "Telluride", "Forte", "Soul", "Seltos", "Carnival", "Stinger", "K5",
                "Niro", "Rio", "EV6", "EV9", "Sedona"
            ]
        },
        {
            "Subaru",
            [
                "Outback", "Forester", "Crosstrek", "Impreza", "Legacy", "Ascent", "WRX", "BRZ", "Solterra", "Baja",
                "Tribeca", "SVX", "XT", "Justy", "BRAT"
            ]
        },
        { "Tesla", ["Model 3", "Model S", "Model X", "Model Y", "Cybertruck", "Roadster", "Semi"] },
        {
            "BMW",
            [
                "3 Series", "5 Series", "7 Series", "X3", "X5", "X7", "i4", "iX", "Z4", "8 Series", "4 Series", "X1",
                "X6", "i7", "i8"
            ]
        },
        {
            "Mercedes-Benz",
            [
                "C-Class", "E-Class", "S-Class", "GLC", "GLE", "GLS", "A-Class", "EQS", "EQE", "G-Class", "CLA", "GLB",
                "GLA", "CLS", "AMG GT"
            ]
        },
        {
            "Audi",
            [
                "A4", "A6", "Q5", "Q7", "e-tron", "A3", "Q3", "A8", "Q8", "TT", "R8", "RS5", "RS7", "Q4 e-tron",
                "RS e-tron GT"
            ]
        },
        { "Lexus", ["RX", "ES", "NX", "IS", "GX", "LX", "UX", "LS", "LC", "RC", "GS", "CT", "SC", "LFA", "RZ"] },
        {
            "Volvo",
            [
                "XC90", "XC60", "S60", "V60", "XC40", "S90", "V90", "C40 Recharge", "EX30", "EX90", "S40", "V40", "C70",
                "S80", "XC70"
            ]
        },
        {
            "Mazda",
            [
                "CX-5", "Mazda3", "CX-9", "MX-5 Miata", "CX-30", "Mazda6", "CX-50", "CX-70", "CX-90", "MX-30", "RX-7",
                "RX-8", "CX-3", "5", "2"
            ]
        },
        {
            "Volkswagen",
            [
                "Jetta", "Tiguan", "Atlas", "Passat", "Golf", "ID.4", "Taos", "Arteon", "Atlas Cross Sport", "GTI",
                "Golf R", "ID. Buzz", "Touareg", "CC", "Beetle"
            ]
        },
        {
            "Porsche",
            [
                "911", "Cayenne", "Macan", "Panamera", "Taycan", "718 Cayman", "718 Boxster", "Cayenne Coupe",
                "918 Spyder", "Carrera GT", "944", "928", "968", "959", "Cayman GT4"
            ]
        },
        {
            "Acura",
            [
                "MDX", "RDX", "TLX", "ILX", "NSX", "Integra", "ZDX", "TSX", "RL", "RSX", "TL", "RLX", "CL", "SLX",
                "Legend"
            ]
        },
        {
            "Infiniti",
            ["QX60", "Q50", "QX50", "QX80", "Q60", "QX55", "QX30", "M", "G", "FX", "JX", "EX", "QX70", "QX4", "I35"]
        },
        {
            "Land Rover",
            [
                "Range Rover", "Discovery", "Defender", "Range Rover Sport", "Range Rover Evoque", "Range Rover Velar",
                "Discovery Sport", "Freelander", "LR3", "LR4", "LR2", "Series I", "Series II", "Series III",
                "Range Rover SV"
            ]
        },
        {
            "Cadillac",
            [
                "Escalade", "CT5", "XT5", "CT4", "XT4", "XT6", "LYRIQ", "CELESTIQ", "CTS", "SRX", "ATS", "XTS", "ELR",
                "Eldorado", "DeVille"
            ]
        },
        {
            "Buick",
            [
                "Enclave", "Encore", "Envision", "Encore GX", "Regal", "LaCrosse", "Cascada", "Verano", "Century",
                "Park Avenue", "Terraza", "Rendezvous", "Lucerne", "Rainier", "Riviera"
            ]
        },
        {
            "GMC",
            [
                "Sierra", "Terrain", "Yukon", "Acadia", "Canyon", "Hummer EV", "Savana", "Jimmy", "Envoy", "Safari",
                "Sonoma", "Typhoon", "Syclone", "Vandura", "TopKick"
            ]
        },
        {
            "Dodge",
            [
                "Charger", "Challenger", "Durango", "Hornet", "Journey", "Grand Caravan", "Viper", "Dart", "Avenger",
                "Nitro", "Neon", "Magnum", "Caliber", "Stealth", "Daytona"
            ]
        },
        {
            "Chrysler",
            [
                "Pacifica", "300", "Voyager", "Town & Country", "200", "Sebring", "PT Cruiser", "Crossfire", "Aspen",
                "Concorde", "LHS", "New Yorker", "Imperial", "Cirrus", "Fifth Avenue"
            ]
        },
        {
            "Ram",
            [
                "1500", "2500", "3500", "ProMaster", "ProMaster City", "Dakota", "700", "1200", "Ramcharger", "Van",
                "Wagon", "Tradesman", "Rebel", "Laramie", "Limited"
            ]
        },
        {
            "Jaguar",
            [
                "F-PACE", "XF", "E-PACE", "F-TYPE", "XE", "I-PACE", "XJ", "XK", "S-Type", "X-Type", "XJS", "Mark 2",
                "XJR", "C-X75", "XJ220"
            ]
        },
        {
            "Lincoln",
            [
                "Navigator", "Corsair", "Aviator", "Nautilus", "Continental", "MKZ", "Town Car", "MKX", "MKC", "MKT",
                "Mark LT", "Blackwood", "LS", "Zephyr", "Versailles"
            ]
        },
        {
            "Genesis",
            [
                "GV80", "G80", "G70", "GV70", "G90", "GV60", "Electrified GV70", "Electrified G80", "X Concept",
                "Mint Concept", "Essentia Concept"
            ]
        },
        {
            "Maserati",
            [
                "Levante", "Ghibli", "Grecale", "Quattroporte", "MC20", "GranTurismo", "GranCabrio", "Bora", "Merak",
                "Biturbo", "Spyder", "Shamal", "Khamsin", "3200 GT", "GranSport"
            ]
        },
        {
            "Alfa Romeo",
            [
                "Giulia", "Stelvio", "Tonale", "4C", "Giulietta", "MiTo", "8C", "159", "156", "147", "166", "Brera",
                "Spider", "GTV", "Montreal"
            ]
        },
        {
            "MINI",
            [
                "Cooper", "Countryman", "Clubman", "Cooper S", "Cooper SE", "John Cooper Works", "Paceman", "Roadster",
                "Coupe", "Convertible", "Hardtop 2 Door", "Hardtop 4 Door", "Electric", "Aceman", "Rocketman"
            ]
        },
        { "Rivian", ["R1T", "R1S", "R2", "R3", "R3X", "EDV"] },
        { "Lucid", ["Air", "Gravity", "Pure", "Touring", "Grand Touring", "Sapphire"] },
        { "Polestar", ["Polestar 2", "Polestar 3", "Polestar 4", "Polestar 5", "Polestar 6", "Polestar Precept"] }
    };

    private static readonly HashSet<string> CarColors =
    [
        "White", "Pearl White", "Arctic White", "Glacier White", "Bright White", "Crystal White",
        "Black", "Jet Black", "Obsidian Black", "Carbon Black", "Midnight Black", "Ebony Black",
        "Gray", "Graphite Gray", "Granite Gray", "Charcoal Gray", "Magnetic Gray", "Slate Gray",
        "Silver", "Brilliant Silver", "Quicksilver", "Liquid Silver", "Sterling Silver", "Iconic Silver",
        "Blue", "Pacific Blue", "Deep Blue", "Navy Blue", "Sapphire Blue", "Electric Blue",
        "Red", "Crimson Red", "Ruby Red", "Racing Red", "Candy Apple Red", "Rapid Red",
        "Brown", "Bronze", "Mocha", "Espresso", "Copper", "Chestnut",
        "Beige", "Champagne", "Sand", "Ivory", "Cream", "Cashmere",
        "Green", "Forest Green", "Emerald Green", "Lime Green", "Hunter Green", "Sage Green",
        "Gold", "Amber Gold", "Champagne Gold", "Desert Sand", "Sunstone",
        "Yellow", "Taxi Yellow", "Sunburst Yellow", "Lemon Yellow", "Solar Yellow",
        "Orange", "Sunset Orange", "Tangerine", "Monarch Orange", "Mango Orange",
        "Purple", "Royal Purple", "Plum", "Amethyst", "Violet",
        "Pink", "Rose", "Magenta", "Dusty Rose", "Coral Pink",
        "Teal", "Turquoise", "Aqua", "Caribbean Blue", "Sea Glass"
    ];


    public List<AuctionEntity> GenerateAuctions()
    {
        Randomizer.Seed = new Random(2_145);

        var auctionFaker = new Faker<AuctionEntity>("en_US")
            .RuleFor(a => a.Id, f => Guid.CreateVersion7())
            .RuleFor(a => a.Status, f => f.PickRandom<Status>())
            .RuleFor(a => a.ReservePrice, f => f.Finance.Random.Number(10_000, 600_000))
            .RuleFor(a => a.Seller, f => f.PickRandom<string>(UserNames))
            .RuleFor(a => a.AuctionEnd,
                f => DateTime.SpecifyKind(f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddMonths(4)),
                    DateTimeKind.Utc))
            .RuleFor(a => a.Winner, f => f.PickRandom<string>(UserNames).OrNull(f, 0.8f))
            .RuleFor(a => a.ItemEntity, f =>
            {
                var randomManufacturer = f.PickRandom<string>(CarModels.Keys);
                var randomModel = f.PickRandom<string>(CarModels[randomManufacturer]);
                var randomColor = f.PickRandom<string>(CarColors);
                var randomYear = f.Random.Int(2005, 2024);

                var urlErrorOr = _imageService.GetOrCreateAsync(randomYear, randomManufacturer, randomModel, randomColor).GetAwaiter().GetResult();

                return new ItemEntity(randomManufacturer,
                    randomModel,
                    randomYear,
                    randomColor,
                    f.Random.Int(0, 350_001),
                    urlErrorOr.Value
                );
            });

        var auctions = auctionFaker.Generate(30);

        return auctions;
    }
}