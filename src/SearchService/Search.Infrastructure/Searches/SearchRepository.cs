using Carsties.Core;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using Search.Application.Interfaces;
using Search.Domain.Auctions;
using Search.Domain.Items;
using Search.Domain.Searches;

namespace Search.Infrastructure.Searches;

public sealed class SearchRepository : ISearchRepository
{
    private readonly IAsyncDocumentSession _documentSession;

    public SearchRepository(IAsyncDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task<PaginatedResponse<List<Auction>>> SearchItemsAsync(AuctionSearch auctionSearch,
        CancellationToken cancellationToken = default)
    {
        var auctions = _documentSession.Query<Auction>()
            .Statistics(out var statistics);

        if (!string.IsNullOrWhiteSpace(auctionSearch.Seller))
        {
            var seller = auctionSearch.Seller;
            auctions = auctions.Search(a => a.Seller, seller, options: SearchOptions.And);
        }

        if (!string.IsNullOrWhiteSpace(auctionSearch.Winner))
        {
            var winner = auctionSearch.Winner;
            auctions = auctions.Search(a => a.Winner, winner, options: SearchOptions.And);
        }

        if (!string.IsNullOrWhiteSpace(auctionSearch.SearchTerm))
        {
            var searchTerm = auctionSearch.SearchTerm;
            auctions = auctions
                .Search(a => a.Item.Make, searchTerm, options: SearchOptions.Or)
                .Search(a => a.Item.Model, searchTerm, options: SearchOptions.Or)
                .Search(a => a.Item.Color, searchTerm, options: SearchOptions.Or);
        }

        if (!string.IsNullOrWhiteSpace(auctionSearch.OrderBy))
        {
            auctions = auctionSearch.OrderBy.ToLower() switch
            {
                "make" => auctions.OrderBy(a => a.Item.Make).ThenBy(a => a.Item.Model),
                "new" => auctions.OrderByDescending(a => a.CreatedAt),
                _ => auctions.OrderBy(a => a.AuctionEnd)
            };
        }

        if (!string.IsNullOrWhiteSpace(auctionSearch.FilterBy))
        {
            var dateTimeUtcNow = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            auctions = auctionSearch.FilterBy.ToLower() switch
            {
                "finished" => auctions.Where(a => a.AuctionEnd < dateTimeUtcNow),
                "endingsoon" => auctions.Where(a =>
                    a.AuctionEnd < dateTimeUtcNow.AddHours(6) && a.AuctionEnd > dateTimeUtcNow),
                _ => auctions.Where(a => a.AuctionEnd > dateTimeUtcNow)
            };
        }

        var items = await auctions
            .Skip((auctionSearch.PageNumber - 1) * auctionSearch.PageSize)
            .Take(auctionSearch.PageSize)
            .ToListAsync(cancellationToken);

        var totalResults = statistics.TotalResults;

        var paginatedResponse = new PaginatedResponse<List<Auction>>
        {
            Data = items,
            PageNumber = auctionSearch.PageNumber,
            PageSize = auctionSearch.PageSize,
            TotalCount = totalResults
        };

        return paginatedResponse;
    }
}