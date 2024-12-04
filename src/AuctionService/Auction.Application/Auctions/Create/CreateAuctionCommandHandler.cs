using Auction.Application.Interfaces;
using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Create;

public sealed class CreateAuctionCommandHandler : IRequestHandler<CreateAuctionCommand, ErrorOr<Domain.Auctions.AuctionEntity>>
{
    private readonly IAuctionsRepository _auctionsRepository;

    public CreateAuctionCommandHandler(IAuctionsRepository auctionsRepository)
    {
        _auctionsRepository = auctionsRepository;
    }

    public async Task<ErrorOr<Domain.Auctions.AuctionEntity>> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        var createdAuction = await _auctionsRepository.CreateAuctionAsync(request.AuctionEntityToCreate, cancellationToken);
        if (createdAuction is null)
        {
            return Error.Validation("AuctionId", "Auction already exists");
        }

        return createdAuction;
    }
}