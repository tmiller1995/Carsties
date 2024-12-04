using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Create;

public record CreateAuctionCommand : IRequest<ErrorOr<Domain.Auctions.AuctionEntity>>
{
    public Domain.Auctions.AuctionEntity AuctionEntityToCreate { get; init; } = null!;
}