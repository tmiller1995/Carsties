using Auction.Application.Interfaces;
using Auction.Domain.Auctions;
using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Get;

public sealed class GetAuctionByIdQueryHandler : IRequestHandler<GetAuctionByIdQuery, ErrorOr<AuctionEntity>>
{
    private readonly IAuctionsRepository _auctionsRepository;

    public GetAuctionByIdQueryHandler(IAuctionsRepository auctionsRepository)
    {
        _auctionsRepository = auctionsRepository;
    }

    public async Task<ErrorOr<AuctionEntity>> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken)
    {
        var auction = await _auctionsRepository.GetAuctionByIdAsync(request.AuctionId, cancellationToken);

        if (auction is null)
        {
            return Error.NotFound($"No auction with id: {request.AuctionId}");
        }

        return auction;
    }
}