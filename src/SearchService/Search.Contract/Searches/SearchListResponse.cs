namespace Search.Contract.Searches;

public record SearchListResponse
{
    public List<SearchResponse> SearchListResponses { get; set; } = [];
}