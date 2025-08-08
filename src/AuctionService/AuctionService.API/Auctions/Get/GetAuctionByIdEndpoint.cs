using Auction.Application.Auctions.Get;
using Auction.Contract.Dtos;
using AuctionService.API.Auctions.Mapper;
using ErrorOr;
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
        AllowAnonymous();
        Get("/api/auctions/{id:guid}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var auctionIdFromRoute = Route<Guid>("id");
        var auction = await _sender.Send(new GetAuctionByIdQuery { AuctionId = auctionIdFromRoute }, ct);

        if (auction.Errors.Exists(e => e.Type == ErrorType.NotFound))
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var auctionDto = auction.Value.ToAuctionDto();
        await Send.OkAsync(auctionDto, ct);
    }
}