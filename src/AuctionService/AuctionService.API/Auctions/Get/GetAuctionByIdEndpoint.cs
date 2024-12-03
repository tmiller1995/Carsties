using Auction.Contract.Dtos;
using FastEndpoints;

namespace AuctionService.API.Auctions.Get;

public sealed class GetAuctionByIdEndpoint : EndpointWithoutRequest<AuctionDto>
{
    public override void Configure()
    {
        Get("/api/auctions/{id:guid}");
        AllowAnonymous();
    }
}