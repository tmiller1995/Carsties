using Carsties.Core;
using FastEndpoints;
using MediatR;
using Search.Application.Search;
using Search.Contract.Searches;
using Search.Domain.Items;
using SearchService.API.Mapper;

namespace SearchService.API.Search;

public sealed class SearchEndpoint : Endpoint<SearchRequest, PaginatedResponse<SearchListResponse>>
{
    private readonly ISender _sender;

    public SearchEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Get("/api/search");
    }

    public override async Task HandleAsync(SearchRequest request, CancellationToken ct)
    {
        var auctionSearch = request.ToAuctionSearch();
        var errorOrItems = await _sender.Send(new SearchQuery { AuctionSearch = auctionSearch }, ct);

        if (!errorOrItems.IsError)
        {
            var result = errorOrItems.Value.ToPaginatedSearchListResponse();
            await Send.OkAsync(result, ct);
            return;
        }

        await Send.ErrorsAsync(StatusCodes.Status400BadRequest, ct);
    }
}