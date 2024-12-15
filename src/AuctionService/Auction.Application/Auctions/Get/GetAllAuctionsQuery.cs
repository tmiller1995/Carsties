using Auction.Domain.Auctions;
using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Get;

public readonly record struct GetAllAuctionsQuery : IRequest<ErrorOr<List<AuctionEntity>>>;