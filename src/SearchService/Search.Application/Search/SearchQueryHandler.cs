using Carsties.Core;
using ErrorOr;
using MediatR;
using Search.Application.Interfaces;
using Search.Domain.Items;

namespace Search.Application.Search;

public sealed class SearchQueryHandler : IRequestHandler<SearchQuery, ErrorOr<PaginatedResponse<List<Item>>>>
{
    private readonly ISearchRepository _searchRepository;

    public SearchQueryHandler(ISearchRepository searchRepository)
    {
        _searchRepository = searchRepository;
    }

    public async Task<ErrorOr<PaginatedResponse<List<Item>>>> Handle(SearchQuery request, CancellationToken cancellationToken)
    {
        var paginatedItems = await _searchRepository.SearchItemsAsync(request.SearchTerm, request.PageNumber, request.PageSize, cancellationToken);
        if (paginatedItems.TotalCount == 0)
            return Error.NotFound("Items", "No items found");

        return paginatedItems;
    }
}