using Carsties.Core;
using Search.Domain.Auctions;
using Search.Domain.Items;
using Search.Domain.Searches;

namespace Search.Application.Interfaces;

public interface ISearchRepository
{
    Task<PaginatedResponse<List<Auction>>> SearchItemsAsync(AuctionSearch auctionSearch, CancellationToken cancellationToken = default);
}