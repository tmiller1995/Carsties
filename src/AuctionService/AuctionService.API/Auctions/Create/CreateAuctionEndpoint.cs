﻿using Auction.Application.Auctions.Create;
using Auction.Contract.Dtos;
using AuctionService.API.Auctions.Get;
using AuctionService.API.Auctions.Mapper;
using FastEndpoints;
using MediatR;

namespace AuctionService.API.Auctions.Create;

public sealed class CreateAuctionEndpoint : Endpoint<CreateAuctionDto>
{
    private readonly ISender _sender;

    public CreateAuctionEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("/api/auctions");
    }

    public override async Task HandleAsync(CreateAuctionDto req, CancellationToken ct)
    {
        var auctionToCreate = req.ToAuction(User.Identity?.Name!);
        var createdAuction =
            await _sender.Send(new CreateAuctionCommand { AuctionEntityToCreate = auctionToCreate }, ct);

        if (createdAuction.IsError)
        {
            foreach (var error in createdAuction.Errors)
            {
                AddError(error.Description);
            }

            await Send.ErrorsAsync(StatusCodes.Status400BadRequest, ct);
            return;
        }

        await Send.CreatedAtAsync(endpointName: nameof(GetAuctionByIdEndpoint),
            new { id = createdAuction.Value.Id },
            createdAuction.Value.ToAuctionDto(),
            cancellation: ct);
    }
}