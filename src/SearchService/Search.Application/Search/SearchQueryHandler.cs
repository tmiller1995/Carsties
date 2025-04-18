using Carsties.Core;
using ErrorOr;
using MediatR;
using Search.Application.Interfaces;
using Search.Domain.Auctions;

namespace Search.Application.Search;

public sealed class SearchQueryHandler : IRequestHandler<SearchQuery, ErrorOr<PaginatedResponse<List<Auction>>>>
{
    private readonly ISearchRepository _searchRepository;

    public SearchQueryHandler(ISearchRepository searchRepository)
    {
        _searchRepository = searchRepository ?? throw new ArgumentNullException(nameof(searchRepository));
    }

    public async Task<ErrorOr<PaginatedResponse<List<Auction>>>> Handle(SearchQuery request,
        CancellationToken cancellationToken) => 
        await _searchRepository.SearchItemsAsync(request.AuctionSearch, cancellationToken);
}