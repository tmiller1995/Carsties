using Auction.Contract.Dtos;
using FastEndpoints;

namespace AuctionService.API.Auctions.Update;

public sealed class UpdateAuctionEndpoint : Endpoint<UpdateAuctionDto>
{
    public override void Configure()
    {
        Put("/api/auctions/{id:guid}");
    }
}