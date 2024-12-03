using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Create;

public record CreateAuctionCommand : IRequest<ErrorOr<Domain.Auctions.Auction>>
{
    public Domain.Auctions.Auction AuctionToCreate { get; init; } = null!;
}