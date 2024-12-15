using Auction.Application.Interfaces;
using Auction.Domain.Auctions;
using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Update;

public sealed class UpdateAuctionCommandHandler : IRequestHandler<UpdateAuctionCommand, ErrorOr<AuctionEntity>>
{
    private readonly IAuctionsRepository _auctionsRepository;

    public UpdateAuctionCommandHandler(IAuctionsRepository auctionsRepository)
    {
        _auctionsRepository = auctionsRepository;
    }

    public async Task<ErrorOr<AuctionEntity>> Handle(UpdateAuctionCommand request, CancellationToken cancellationToken)
    {
        var existingAuction = await _auctionsRepository.GetAuctionByIdAsync(request.Id, cancellationToken);
        if (existingAuction is null)
        {
            return Error.NotFound($"No auction with id: {request.Id}");
        }

        existingAuction.ItemEntity.UpdateMake(request.Make);
        existingAuction.ItemEntity.UpdateModel(request.Model);
        existingAuction.ItemEntity.UpdateColor(request.Color);
        existingAuction.ItemEntity.UpdateMilage(request.Mileage);
        existingAuction.ItemEntity.UpdateYear(request.Year);

        var updatedAuction = await _auctionsRepository.UpdateAuctionByIdAsync(existingAuction, cancellationToken);

        return updatedAuction;
    }
}