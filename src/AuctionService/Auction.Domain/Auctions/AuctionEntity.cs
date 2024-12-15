using Auction.Domain.Items;
using Carsties.Core;

namespace Auction.Domain.Auctions;

public sealed class AuctionEntity : Entity
{
    public decimal ReservePrice { get; private init; }
    public string Seller { get; private init; } = string.Empty;
    public string? Winner { get; private init; }
    public decimal? SoldAmount { get; private init; }
    public decimal? CurrentHighBid { get; private init; }
    public DateTime CreatedAt { get; private init; }
    public DateTime UpdatedAt { get; private init; }
    public DateTime AuctionEnd { get; private init; }
    public Status Status { get; private init; }
    public ItemEntity ItemEntity { get; init; } = null!;

    public AuctionEntity()
    {
    }

    public AuctionEntity(decimal reservePrice,
        string seller,
        string? winner,
        decimal? soldAmount,
        decimal? currentHighBid,
        DateTime createdAt,
        DateTime updatedAt,
        DateTime auctionEnd,
        Status status,
        Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        ReservePrice = reservePrice;
        Seller = seller;
        Winner = winner;
        SoldAmount = soldAmount;
        CurrentHighBid = currentHighBid;
        CreatedAt = DateTime.SpecifyKind(createdAt, DateTimeKind.Utc);
        UpdatedAt = DateTime.SpecifyKind(updatedAt, DateTimeKind.Utc);
        AuctionEnd = DateTime.SpecifyKind(auctionEnd, DateTimeKind.Utc);
        Status = status;
    }
}