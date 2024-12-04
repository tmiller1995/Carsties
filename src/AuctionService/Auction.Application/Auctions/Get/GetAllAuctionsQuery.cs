using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Get;

public readonly record struct GetAllAuctionsQuery : IRequest<ErrorOr<List<Domain.Auctions.AuctionEntity>>>
{
}