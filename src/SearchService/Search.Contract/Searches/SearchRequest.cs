using FastEndpoints;

namespace Search.Contract.Searches;

public sealed class SearchRequest
{
    [QueryParam, BindFrom("searchTerm")] public string? SearchTerm { get; set; }
    [QueryParam, BindFrom("pageNumber")] public int PageNumber { get; set; } = 1;
    [QueryParam, BindFrom("pageSize")] public int PageSize { get; set; } = 10;
    [QueryParam, BindFrom("seller")] public string? Seller { get; set; }
    [QueryParam, BindFrom("winner")] public string? Winner { get; set; }
    [QueryParam, BindFrom("orderBy")] public string? OrderBy { get; set; }
    [QueryParam, BindFrom("filterBy")] public string? FilterBy { get; set; }
}