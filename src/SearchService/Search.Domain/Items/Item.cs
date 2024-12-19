using Carsties.Core;

namespace Search.Domain.Items;

public sealed class Item : Entity
{
    public new string Id { get; init; } = Guid.CreateVersion7().ToString();
    public string Make { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public int Year { get; init; }
    public string Color { get; init; } = string.Empty;
    public int Mileage { get; init; }
    public string ImageUrl { get; init; } = string.Empty;
}