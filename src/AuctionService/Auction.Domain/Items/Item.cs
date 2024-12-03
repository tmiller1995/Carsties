using Carsties.Core;

namespace Auction.Domain.Items;

public sealed class Item : Entity
{
    public string Make { get; }
    public string Model { get; }
    public int Year { get; }
    public string Color { get; }
    public int Mileage { get; }
    public string ImageUrl { get; }
    public Auctions.Auction Auction { get; set; } = null!;
    public Guid AuctionId { get; set; }

    private Item()
    {
    }

    public Item(string make,
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