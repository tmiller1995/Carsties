using Auction.Application.Interfaces;
using Auction.Domain.Auctions;
using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Get;

public sealed class GetAllAuctionsQueryHandler : IRequestHandler<GetAllAuctionsQuery, ErrorOr<List<AuctionEntity>>>
{
    private readonly IAuctionsRepository _auctionsRepository;

    public GetAllAuctionsQueryHandler(IAuctionsRepository auctionsRepository)
    {
        _auctionsRepository = auctionsRepository;
    }

    public async Task<ErrorOr<List<AuctionEntity>>> Handle(GetAllAuctionsQuery request, CancellationToken cancellationToken)
    {
        var auctions = await _auctionsRepository.GetAuctionsAsync(cancellationToken);
        if (auctions.Count == 0)
        {
            return Error.NotFound("Auctions", "No auctions found");
        }

        return auctions;
    }
}