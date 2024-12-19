using Carsties.Core;
using ErrorOr;
using FastEndpoints;
using MediatR;
using Search.Application.Search;
using Search.Domain.Items;

namespace SearchService.API.Search;

public sealed class SearchEndpoint : EndpointWithoutRequest<PaginatedResponse<List<Item>>>
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

    public override async Task HandleAsync(CancellationToken ct)
    {
        var searchTerm = Query<string>("searchTerm");
        var pageNumber = Query<int?>("pageNumber") ?? 1;
        var pageSize = Query<int?>("pageSize") ?? 4;

        var searchQuery = new SearchQuery {SearchTerm = searchTerm, PageNumber = pageNumber, PageSize = pageSize};
        var errorOrItems = await _sender.Send(searchQuery, ct);

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