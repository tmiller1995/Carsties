using Auction.Application.Interfaces;
using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Delete;

public sealed class DeleteAuctionCommandHandler : IRequestHandler<DeleteAuctionCommand, ErrorOr<bool>>
{
    private readonly IAuctionsRepository _auctionsRepository;

    public DeleteAuctionCommandHandler(IAuctionsRepository auctionsRepository)
    {
        _auctionsRepository = auctionsRepository;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteAuctionCommand request, CancellationToken cancellationToken)
    {
        var existingAuction = await _auctionsRepository.GetAuctionByIdAsync(request.Id, cancellationToken);
        if (existingAuction is null)
            return Error.NotFound($"No auction with id: {request.Id}");

        if (existingAuction.Seller != request.UserDeleting)
            return Error.Forbidden("You are not authorized to delete this auction");

        var deleted = await _auctionsRepository.DeleteAuctionAsync(existingAuction, cancellationToken);
        return deleted ? true : Error.Failure("Failed to delete auction");
    }
}