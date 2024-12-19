using Carsties.Core;
using Search.Domain.Items;

namespace Search.Domain.Auctions;

public sealed class Auction : Entity
{
    public new string Id { get; set; } = Guid.CreateVersion7().ToString();
    public decimal ReservePrice { get; set; }
    public string Seller { get; set; } = string.Empty;
    public string? Winner { get; set; }
    public decimal? SoldAmount { get; set; }
    public decimal? CurrentHighBid { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime AuctionEnd { get; set; }
    public Status Status { get; set; }
    public Item Item { get; set; } = null!;
}