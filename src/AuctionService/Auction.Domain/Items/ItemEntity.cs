using Auction.Domain.Auctions;
using Carsties.Core;

namespace Auction.Domain.Items;

public sealed class ItemEntity : Entity
{
    public string Make { get; private init; }
    public string Model { get; private init; }
    public int Year { get; private init; }
    public string Color { get; private init; }
    public int Mileage { get; private init; }
    public string ImageUrl { get; private init; }
    public AuctionEntity AuctionEntity { get; init; } = default!;

    private ItemEntity()
    {
    }

    public ItemEntity(string make,
        string model,
        int year,
        string color,
        int mileage,
        string imageUrl,
        Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        Make = make;
        Model = model;
        Year = year;
        Color = color;
        Mileage = mileage;
        ImageUrl = imageUrl;
    }
}