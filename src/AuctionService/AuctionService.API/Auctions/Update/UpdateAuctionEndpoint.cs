using Auction.Application.Auctions.Update;
using Auction.Contract.Dtos;
using ErrorOr;
using FastEndpoints;
using MediatR;

namespace AuctionService.API.Auctions.Update;

public sealed class UpdateAuctionEndpoint : Endpoint<UpdateAuctionDto>
{
    private readonly ISender _sender;

    public UpdateAuctionEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put("/api/auctions/{id:guid}");
    }

    public override async Task HandleAsync(UpdateAuctionDto req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var auctionToUpdateCommand = new UpdateAuctionCommand
        {
            Id = id,
            Make = req.Make,
            Model = req.Model,
            Color = req.Color,
            Mileage = req.Mileage,
            Year = req.Year,
            UserUpdating = User.Identity?.Name!
        };

        var updatedAuction = await _sender.Send(auctionToUpdateCommand, ct);

        if (!updatedAuction.IsError)
        {
            await SendNoContentAsync(ct);
            return;
        }

        if (updatedAuction.Errors.Exists(e => e.Type == ErrorType.NotFound))
        {
            await SendAsync(updatedAuction.Errors, StatusCodes.Status404NotFound, ct);
            return;
        }

        if (updatedAuction.Errors.Exists(e => e.Type == ErrorType.Forbidden))
        {
            await SendForbiddenAsync(ct);
            return;
        }

        await SendAsync(updatedAuction.Errors, StatusCodes.Status400BadRequest, ct);
    }
}