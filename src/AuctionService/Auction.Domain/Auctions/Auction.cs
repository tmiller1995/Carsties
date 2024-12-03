using Auction.Domain.Items;
using Carsties.Core;

namespace Auction.Domain.Auctions;

public sealed class Auction : Entity
{
    public decimal ReservePrice { get; }
    public string Seller { get; }
    public string? Winner { get; }
    public decimal? SoldAmount { get; }
    public decimal? CurrentHighBid { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public DateTime AuctionEnd { get; }
    public Status Status { get; }
    public Item Item { get; set; }

    private Auction()
    {
    }

    public Auction(decimal reservePrice,
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
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        AuctionEnd = auctionEnd;
        Status = status;
    }
}