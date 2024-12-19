using Carsties.Core;
using ErrorOr;
using MediatR;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using Search.Domain.Auctions;
using Search.Domain.Items;

namespace Search.Application.Search;

public sealed class SearchQueryHandler : IRequestHandler<SearchQuery, ErrorOr<PaginatedResponse<List<Item>>>>
{
    private readonly IDocumentSession _session;

    public SearchQueryHandler(IDocumentSession session)
    {
        _session = session;
    }

    public Task<ErrorOr<PaginatedResponse<List<Item>>>> Handle(SearchQuery request, CancellationToken cancellationToken)
    {
        var auctions = _session.Query<Auction>()
            .Statistics(out var statistics);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            auctions = auctions
                .Search(a => a.Item.Make, request.SearchTerm)
                .Search(a => a.Item.Model, request.SearchTerm)
                .Search(a => a.Item.Color, request.SearchTerm);

        var items = auctions
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(a => a.Item).ToList();

        if (items.Count == 0)
        {
            return Task.FromResult<ErrorOr<PaginatedResponse<List<Item>>>>(Error.NotFound("Items", "No items found"));
        }

        var totalResults = statistics.TotalResults;

        var paginatedResponse = new PaginatedResponse<List<Item>>
        {
            Data = items,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalResults
        };

        return Task.FromResult<ErrorOr<PaginatedResponse<List<Item>>>>(paginatedResponse);
    }
}