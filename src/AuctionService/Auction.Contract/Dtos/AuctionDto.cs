namespace Auction.Contract.Dtos;

public record AuctionDto
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public DateTime AuctionEnd { get; init; }
    public string Seller { get; init; } = string.Empty;
    public string? Winner { get; init; }
    public string Make { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public int Year { get; init; }
    public string Color { get; init; } = string.Empty;
    public int Mileage { get; init; }
    public string ImageUrl { get; init; } = string.Empty;
    public string Status { get; init; }
    public decimal ReservePrice { get; init; }
    public decimal? SoldAmount { get; init; }
    public decimal? CurrentHighBid { get; init; }
}