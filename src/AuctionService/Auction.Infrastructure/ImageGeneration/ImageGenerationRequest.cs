using System.Text.Json.Serialization;

namespace Auction.Infrastructure.ImageGeneration;

public sealed record ImageGenerationRequest
{
    [JsonPropertyName("model")] public required string Model { get; init; }
    [JsonPropertyName("prompt")] public required string Prompt { get; init; }
    [JsonPropertyName("n")] public required int N { get; init; }
    [JsonPropertyName("size")] public required string Size { get; init; }
    [JsonPropertyName("quality")] public required string Quality { get; init; }
    [JsonPropertyName("style")] public required string Style { get; init; }
    [JsonPropertyName("response_format")] public required string ResponseFormat { get; init; }
    [JsonPropertyName("user")] public required string User { get; init; }
}