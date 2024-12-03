using Auction.Application.Auctions.Get;
using Auction.Contract.Dtos;
using AuctionService.API.Auctions.Mapper;
using FastEndpoints;
using MediatR;

namespace AuctionService.API.Auctions.Get;

public sealed class GetAuctionsEndpoint : EndpointWithoutRequest<List<AuctionDto>>
{
    private readonly ISender _sender;

    public GetAuctionsEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("/api/auctions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var auctions = await _sender.Send(new GetAllAuctionsQuery(), ct);
        if (auctions.IsError)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var auctionDtos = auctions.Value.Select(a => a.ToAuctionDto());
        await SendOkAsync(auctionDtos.ToList(), ct);
    }
}