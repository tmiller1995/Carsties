namespace Auction.Contract.Dtos;

public record CreateAuctionDto
{
    public string Make { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public int Year { get; init; }
    public string Color { get; init; } = string.Empty;
    public int Mileage { get; init; }
    public string ImageUrl { get; init; } = string.Empty;
    public decimal ReservePrice { get; init; }
    public DateTime AuctionEnd { get; init; }
}