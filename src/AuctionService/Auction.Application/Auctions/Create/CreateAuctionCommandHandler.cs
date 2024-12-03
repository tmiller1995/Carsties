using Auction.Application.Interfaces;
using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Create;

public sealed class CreateAuctionCommandHandler : IRequestHandler<CreateAuctionCommand, ErrorOr<Domain.Auctions.Auction>>
{
    private readonly IAuctionsRepository _auctionsRepository;

    public CreateAuctionCommandHandler(IAuctionsRepository auctionsRepository)
    {
        _auctionsRepository = auctionsRepository;
    }

    public async Task<ErrorOr<Domain.Auctions.Auction>> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        var createdAuction = await _auctionsRepository.CreateAuctionAsync(request.AuctionToCreate, cancellationToken);
        if (createdAuction is null)
        {
            return Error.Validation("AuctionId", "Auction already exists");
        }

        return createdAuction;
    }
}