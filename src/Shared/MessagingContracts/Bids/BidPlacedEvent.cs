using System.Text.Json.Serialization;

namespace Carsties.Shared.MessagingContracts;

public record BidPlacedEvent
{
    [JsonPropertyName("id")]public Guid Id { get; set; }
    [JsonPropertyName("auctionId")]public Guid AuctionId { get; set; }
    [JsonPropertyName("bidder")] public string Bidder { get; set; } = null!;
    [JsonPropertyName("bidTime")]public DateTime BidTime { get; set; }
    [JsonPropertyName("amount")]public decimal Amount { get; set; }
    [JsonPropertyName("bidStatus")]public string BidStatus { get; set; } = null!;
}