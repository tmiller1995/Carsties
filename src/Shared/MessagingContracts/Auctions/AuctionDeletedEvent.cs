using System.Text.Json.Serialization;

namespace Carsties.Shared.MessagingContracts;

public record AuctionDeletedEvent
{
    [JsonPropertyName("id")] public Guid Id { get; init; }
}