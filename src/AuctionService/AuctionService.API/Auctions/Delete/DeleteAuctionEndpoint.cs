using Auction.Application.Auctions.Delete;
using ErrorOr;
using FastEndpoints;
using MediatR;

namespace AuctionService.API.Auctions.Delete;

public sealed class DeleteAuctionEndpoint : EndpointWithoutRequest
{
    private readonly ISender _sender;

    public DeleteAuctionEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Delete("/api/auctions/{id:guid}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var auctionId = Route<Guid>("id");

        var deletionResult = await _sender.Send(new DeleteAuctionCommand { Id = auctionId }, ct);

        if (!deletionResult.IsError)
        {
            await SendNoContentAsync(ct);
            return;
        }

        if (deletionResult.Errors.Exists(e => e.Type == ErrorType.NotFound))
        {
            await SendAsync(deletionResult.Errors, StatusCodes.Status404NotFound, ct);
            return;
        }

        await SendAsync(deletionResult.Errors, StatusCodes.Status400BadRequest, ct);
    }
}