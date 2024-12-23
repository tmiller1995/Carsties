namespace Search.Domain.Searches;

public readonly record struct AuctionSearch
{
    public string? SearchTerm { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public string? Seller { get; init; }
    public string? Winner { get; init; }
    public string? OrderBy { get; init; }
    public string? FilterBy { get; init; }
}