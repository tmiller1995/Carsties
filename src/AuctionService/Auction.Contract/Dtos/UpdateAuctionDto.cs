﻿using System.Text.Json.Serialization;

namespace Auction.Contract.Dtos;

public record UpdateAuctionDto
{
    [JsonPropertyName("make")] public string? Make { get; init; }
    [JsonPropertyName("model")] public string? Model { get; init; }
    [JsonPropertyName("color")] public string? Color { get; init; }
    [JsonPropertyName("mileage")] public int? Mileage { get; init; }
    [JsonPropertyName("year")] public int? Year { get; init; }
}