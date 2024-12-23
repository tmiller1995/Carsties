using Carsties.Core;
using Search.Domain.Items;
using Search.Domain.Searches;

namespace Search.Application.Interfaces;

public interface ISearchRepository
{
    Task<PaginatedResponse<List<Item>>> SearchItemsAsync(AuctionSearch auctionSearch, CancellationToken cancellationToken = default);
}