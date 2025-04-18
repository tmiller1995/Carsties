using Carsties.Core;
using Search.Contract.Searches;
using Search.Domain.Auctions;

namespace SearchService.API.Mapper;

public static class SearchMapper
{
    public static PaginatedResponse<SearchListResponse> ToPaginatedSearchListResponse(this PaginatedResponse<List<Auction>> paginatedResponse)
    {
        var searchResponses = paginatedResponse.Data.Select(s => s.ToSearchResponse()).ToList();
        return new PaginatedResponse<SearchListResponse>
        {
            Data = new SearchListResponse {SearchListResponses = searchResponses},
            PageNumber = paginatedResponse.PageNumber,
            PageSize = paginatedResponse.PageSize,
            TotalCount = paginatedResponse.TotalCount
        };
    }

    private static SearchResponse ToSearchResponse(this Auction auctionEntity)
    {
        return new SearchResponse
        {
            Id = auctionEntity.ExternalId,
            Seller = auctionEntity.Seller,
            Winner = auctionEntity.Winner,
            SoldAmount = auctionEntity.SoldAmount,
            CurrentHighBid = auctionEntity.CurrentHighBid,
            CreatedAt = auctionEntity.CreatedAt,
            UpdatedAt = auctionEntity.UpdatedAt,
            AuctionEnd = auctionEntity.AuctionEnd,
            Status = auctionEntity.Status.ToString(),
            Make = auctionEntity.Item.Make,
            Model = auctionEntity.Item.Model,
            Year = auctionEntity.Item.Year,
            Color = auctionEntity.Item.Color,
            Mileage = auctionEntity.Item.Mileage,
            ImageUrl = auctionEntity.Item.ImageUrl,
            ReservePrice = auctionEntity.ReservePrice
        };
    }
}