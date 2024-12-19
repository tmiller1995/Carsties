using Carsties.Core;
using Search.Domain.Items;

namespace Search.Application.Interfaces;

public interface ISearchRepository
{
    Task<PaginatedResponse<List<Item>>> SearchItemsAsync(string searchTerm,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);
}