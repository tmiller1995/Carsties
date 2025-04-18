using System.Text.Json.Serialization;

namespace Carsties.Core;

public record PaginatedResponse<T>
{
    [JsonPropertyName("data")] public T Data { get; init; } = default!;
    [JsonPropertyName("pageNumber")] public int PageNumber { get; init; }
    [JsonPropertyName("pageSize")] public double PageSize { get; init; }
    [JsonPropertyName("totalCount")] public double TotalCount { get; init; }
    [JsonPropertyName("totalPages")] public int TotalPages => (int)Math.Ceiling(TotalCount / PageSize);
}