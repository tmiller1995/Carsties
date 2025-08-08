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
        Delete("/api/auctions/{id:guid}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var auctionId = Route<Guid>("id");

        var deletionResult =
            await _sender.Send(new DeleteAuctionCommand { Id = auctionId, UserDeleting = User.Identity?.Name! }, ct);

        if (!deletionResult.IsError)
        {
            await Send.NoContentAsync(ct);
            return;
        }

        if (deletionResult.Errors.Exists(e => e.Type == ErrorType.NotFound))
        {
            foreach (var error in deletionResult.Errors)
            {
                AddError(error.Description);
            }

            await Send.ErrorsAsync(StatusCodes.Status404NotFound, ct);
            return;
        }

        if (deletionResult.Errors.Exists(e => e.Type == ErrorType.Forbidden))
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        foreach (var error in deletionResult.Errors)
        {
            AddError(error.Description);
        }

        await Send.ErrorsAsync(StatusCodes.Status400BadRequest, ct);
    }
}