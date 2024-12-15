using Auction.Domain.Auctions;
using Carsties.Core;
using ErrorOr;

namespace Auction.Domain.Items;

public sealed class ItemEntity : Entity
{
    public string Make { get; private set; } = string.Empty;
    public string Model { get; private set; } = string.Empty;
    public int Year { get; private set; }
    public string Color { get; private set; } = string.Empty;
    public int Mileage { get; private set; }
    public string ImageUrl { get; private set; } = string.Empty;
    public AuctionEntity AuctionEntity { get; init; } = null!;

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

    public ErrorOr<bool> UpdateColor(string? color)
    {
        if (string.IsNullOrWhiteSpace(color))
            return Error.Validation(nameof(color), "Color cannot be empty");

        Color = color;

        return true;
    }

    public ErrorOr<bool> UpdateMake(string? make)
    {
        if (string.IsNullOrWhiteSpace(make))
            return Error.Validation(nameof(make), "Make cannot be empty");

        Make = make;
        return true;
    }

    public ErrorOr<bool> UpdateModel(string? model)
    {
        if (string.IsNullOrWhiteSpace(model))
            return Error.Validation(nameof(model), "Model cannot be empty");

        Model = model;
        return true;
    }

    public ErrorOr<bool> UpdateMilage(int? mileage)
    {
        if (mileage is null)
            return Error.Validation(nameof(mileage), "Mileage cannot be empty");

        Mileage = mileage.Value;
        return true;
    }

    public ErrorOr<bool> UpdateYear(int? year)
    {
        if (year is null)
            return Error.Validation(nameof(year), "Year cannot be empty");

        Year = year.Value;
        return true;
    }
}