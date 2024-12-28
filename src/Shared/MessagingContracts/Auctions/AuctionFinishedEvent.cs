using System.Text.Json.Serialization;

namespace Carsties.Shared.MessagingContracts;

public record AuctionFinishedEvent
{
    [JsonPropertyName("auctionId")] public Guid AuctionId { get; init; }
    [JsonPropertyName("winner")] public string Winner { get; init; } = null!;
    [JsonPropertyName("seller")] public string Seller { get; init; } = null!;
    [JsonPropertyName("amount")] public decimal Amount { get; init; }
    [JsonPropertyName("itemSold")] public bool ItemSold { get; init; }
}