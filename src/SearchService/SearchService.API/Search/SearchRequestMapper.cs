using Search.Contract.Searches;
using Search.Domain.Searches;

namespace SearchService.API.Search;

public static class SearchRequestMapper
{
    public static AuctionSearch ToAuctionSearch(this SearchRequest searchRequest)
    {
        return new AuctionSearch
        {
            SearchTerm = searchRequest.SearchTerm,
            PageNumber = searchRequest.PageNumber,
            PageSize = searchRequest.PageSize,
            Seller = searchRequest.Seller,
            Winner = searchRequest.Winner,
            OrderBy = searchRequest.OrderBy,
            FilterBy = searchRequest.FilterBy
        };
    }
}