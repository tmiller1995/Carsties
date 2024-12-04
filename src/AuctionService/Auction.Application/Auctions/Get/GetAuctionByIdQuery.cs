using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Get;

public readonly record struct GetAuctionByIdQuery : IRequest<ErrorOr<Domain.Auctions.AuctionEntity>>
{
    public Guid AuctionId { get; init; }
}