using Carsties.Core;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Search.Application.Interfaces;
using Search.Domain.Auctions;
using Search.Domain.Items;

namespace Search.Infrastructure.Auctions;

public sealed class SearchRepository : ISearchRepository
{
    private readonly IAsyncDocumentSession _documentSession;

    public SearchRepository(IAsyncDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task<PaginatedResponse<List<Item>>> SearchItemsAsync(string searchTerm, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var auctions = _documentSession.Query<Auction>()
            .Statistics(out var statistics);

        if (!string.IsNullOrWhiteSpace(searchTerm))
            auctions = auctions
                .Search(a => a.Item.Make, searchTerm)
                .Search(a => a.Item.Model, searchTerm)
                .Search(a => a.Item.Color, searchTerm);

        var items = await auctions
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(a => a.Item)
            .ToListAsync(cancellationToken);

        var totalResults = statistics.TotalResults;

        var paginatedResponse = new PaginatedResponse<List<Item>>
        {
            Data = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalResults
        };

        return paginatedResponse;
    }
}