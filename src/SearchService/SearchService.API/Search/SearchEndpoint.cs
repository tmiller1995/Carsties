using Carsties.Core;
using ErrorOr;
using FastEndpoints;
using MediatR;
using Search.Application.Search;
using Search.Contract.Searches;
using Search.Domain.Items;

namespace SearchService.API.Search;

public sealed class SearchEndpoint : Endpoint<SearchRequest, PaginatedResponse<List<Item>>>
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
            await SendOkAsync(errorOrItems.Value, ct);
            return;
        }

        if (errorOrItems.Errors.Exists(e => e.Type == ErrorType.NotFound))
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendErrorsAsync(StatusCodes.Status400BadRequest, ct);
    }
}