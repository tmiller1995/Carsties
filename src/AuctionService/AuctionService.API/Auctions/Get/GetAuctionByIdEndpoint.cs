using Auction.Application.Auctions.Get;
using Auction.Contract.Dtos;
using AuctionService.API.Auctions.Mapper;
using FastEndpoints;
using MediatR;

namespace AuctionService.API.Auctions.Get;

public sealed class GetAuctionByIdEndpoint : EndpointWithoutRequest<AuctionDto>
{
    private readonly ISender _sender;

    public GetAuctionByIdEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("/api/auctions/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var auctionIdFromRoute = Route<Guid>("id");
        var auction = await _sender.Send(new GetAuctionByIdQuery { AuctionId = auctionIdFromRoute }, ct);

        if (auction.IsError)
        {
            await SendErrorsAsync(StatusCodes.Status400BadRequest, ct);
            return;
        }

        var auctionDto = auction.Value.ToAuctionDto();
        await SendOkAsync(auctionDto, ct);
    }
}