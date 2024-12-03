namespace Auction.Contract.Dtos;

public record UpdateAuctionDto
{
    public string? Make { get; init; }
    public string? Model { get; init; }
    public string? Color { get; init; }
    public int? Mileage { get; init; }
    public int? Year { get; init; }
}