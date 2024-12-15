using Auction.Domain.Auctions;
using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Get;

public readonly record struct GetAuctionByIdQuery : IRequest<ErrorOr<AuctionEntity>>
{
    public Guid AuctionId { get; init; }
}