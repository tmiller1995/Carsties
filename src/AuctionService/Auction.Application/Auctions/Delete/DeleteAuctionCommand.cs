using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Delete;

public readonly record struct DeleteAuctionCommand : IRequest<ErrorOr<bool>>
{
    public Guid Id { get; init; }
}