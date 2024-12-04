using Auction.Application.Interfaces;
using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Get;

public sealed class GetAuctionByIdQueryHandler : IRequestHandler<GetAuctionByIdQuery, ErrorOr<Domain.Auctions.AuctionEntity>>
{
    private readonly IAuctionsRepository _auctionsRepository;

    public GetAuctionByIdQueryHandler(IAuctionsRepository auctionsRepository)
    {
        _auctionsRepository = auctionsRepository;
    }

    public async Task<ErrorOr<Domain.Auctions.AuctionEntity>> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken)
    {
        var auction = await _auctionsRepository.GetAuctionByIdAsync(request.AuctionId, cancellationToken);

        if (auction is null)
        {
            return Error.NotFound($"No auction with id: {request.AuctionId}");
        }

        return auction;
    }
}