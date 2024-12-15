using Auction.Domain.Auctions;
using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Create;

public record CreateAuctionCommand : IRequest<ErrorOr<AuctionEntity>>
{
    public AuctionEntity AuctionEntityToCreate { get; init; } = null!;
}