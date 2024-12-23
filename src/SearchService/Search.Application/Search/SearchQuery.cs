using Carsties.Core;
using ErrorOr;
using MediatR;
using Search.Domain.Items;
using Search.Domain.Searches;

namespace Search.Application.Search;

public readonly record struct SearchQuery : IRequest<ErrorOr<PaginatedResponse<List<Item>>>>
{
    public AuctionSearch AuctionSearch { get; init; }
}