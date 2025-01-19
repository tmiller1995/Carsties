using Carsties.Core;
using ErrorOr;
using MediatR;
using Search.Domain.Auctions;
using Search.Domain.Items;
using Search.Domain.Searches;

namespace Search.Application.Search;

public readonly record struct SearchQuery : IRequest<ErrorOr<PaginatedResponse<List<Auction>>>>
{
    public AuctionSearch AuctionSearch { get; init; }
}