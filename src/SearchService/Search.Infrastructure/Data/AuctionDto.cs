using System.Text.Json.Serialization;

namespace Search.Infrastructure.Data;

public class AuctionDto
{
    [JsonPropertyName("id")] public Guid Id { get; init; }
    [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; init; }
    [JsonPropertyName("updatedAt")] public DateTime UpdatedAt { get; init; }
    [JsonPropertyName("auctionEnd")] public DateTime AuctionEnd { get; init; }
    [JsonPropertyName("seller")] public string Seller { get; init; }
    [JsonPropertyName("winner")] public string? Winner { get; init; }
    [JsonPropertyName("make")] public string Make { get; init; }
    [JsonPropertyName("model")] public string Model { get; init; }
    [JsonPropertyName("year")] public int Year { get; init; }
    [JsonPropertyName("color")] public string Color { get; init; }
    [JsonPropertyName("mileage")] public int Mileage { get; init; }
    [JsonPropertyName("imageUrl")] public string ImageUrl { get; init; }
    [JsonPropertyName("status")] public string Status { get; init; }
    [JsonPropertyName("reservePrice")] public decimal ReservePrice { get; init; }
    [JsonPropertyName("soldAmount")] public decimal? SoldAmount { get; init; }
    [JsonPropertyName("currentHighBid")] public decimal? CurrentHighBid { get; init; }
}