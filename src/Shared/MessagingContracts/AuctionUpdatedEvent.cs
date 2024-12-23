using System.Text.Json.Serialization;

namespace Carsties.Shared.MessagingContracts;

public record AuctionUpdatedEvent
{
    [JsonPropertyName("id")] public Guid Id { get; init; }
    [JsonPropertyName("make")] public string? Make { get; init; }
    [JsonPropertyName("model")] public string? Model { get; init; }
    [JsonPropertyName("color")] public string? Color { get; init; }
    [JsonPropertyName("mileage")] public int? Mileage { get; init; }
    [JsonPropertyName("year")] public int? Year { get; init; }
}