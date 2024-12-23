using System.Text.Json.Serialization;

namespace Carsties.Shared.MessagingContracts;

public record AuctionCreatedEvent
{
    [JsonPropertyName("id")] public Guid Id { get; init; }
    [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; init; }
    [JsonPropertyName("updatedAt")] public DateTime UpdatedAt { get; init; }
    [JsonPropertyName("auctionEnd")] public DateTime AuctionEnd { get; init; }
    [JsonPropertyName("seller")] public string Seller { get; init; } = string.Empty;
    [JsonPropertyName("winner")] public string? Winner { get; init; }
    [JsonPropertyName("make")] public string Make { get; init; } = string.Empty;
    [JsonPropertyName("model")] public string Model { get; init; } = string.Empty;
    [JsonPropertyName("year")] public int Year { get; init; }
    [JsonPropertyName("color")] public string Color { get; init; } = string.Empty;
    [JsonPropertyName("mileage")] public int Mileage { get; init; }
    [JsonPropertyName("imageUrl")] public string ImageUrl { get; init; } = string.Empty;
    [JsonPropertyName("status")] public string Status { get; init; } = string.Empty;
    [JsonPropertyName("reservePrice")] public decimal ReservePrice { get; init; }
    [JsonPropertyName("soldAmount")] public decimal? SoldAmount { get; init; }
    [JsonPropertyName("currentHighBid")] public decimal? CurrentHighBid { get; init; }
}