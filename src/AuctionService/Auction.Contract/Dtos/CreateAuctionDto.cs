using System.Text.Json.Serialization;

namespace Auction.Contract.Dtos;

public record CreateAuctionDto
{
    [JsonPropertyName("make")] public string Make { get; init; } = string.Empty;
    [JsonPropertyName("model")] public string Model { get; init; } = string.Empty;
    [JsonPropertyName("year")] public int Year { get; init; }
    [JsonPropertyName("color")] public string Color { get; init; } = string.Empty;
    [JsonPropertyName("mileage")] public int Mileage { get; init; }
    [JsonPropertyName("imageUrl")] public string ImageUrl { get; init; } = string.Empty;
    [JsonPropertyName("reservePrice")] public decimal ReservePrice { get; init; }
    [JsonPropertyName("auctionEnd")] public DateTime AuctionEnd { get; init; }
}