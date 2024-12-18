using Carsties.Core;

namespace Search.Domain.Items;

public sealed class ItemEntity : Entity
{
    public string Make { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public int Year { get; init; }
    public string Color { get; init; } = string.Empty;
    public int Mileage { get; init; }
    public string ImageUrl { get; init; } = string.Empty;
    public AuctionEntity AuctionEntity { get; init; } = null!;

    public ItemEntity(Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
    }
}