using System.Text.Json.Serialization;

namespace Auction.Infrastructure.ImageGeneration;

public sealed record OpenAiImageResponse
{
    [JsonPropertyName("created")] public required long Created { get; init; }
    [JsonPropertyName("data")] public required OpenAiDataItem[] Data { get; init; }
}