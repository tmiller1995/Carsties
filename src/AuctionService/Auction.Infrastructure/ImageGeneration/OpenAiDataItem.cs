using System.Text.Json.Serialization;

namespace Auction.Infrastructure.ImageGeneration;

public sealed record OpenAiDataItem
{
    [JsonPropertyName("revised_prompt")] public required string RevisedPrompt { get; init; }
    [JsonPropertyName("url")] public required string Url { get; init; }
}