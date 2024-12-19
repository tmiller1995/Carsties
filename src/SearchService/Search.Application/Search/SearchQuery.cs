using Carsties.Core;
using ErrorOr;
using MediatR;
using Search.Domain.Items;

namespace Search.Application.Search;

public readonly record struct SearchQuery : IRequest<ErrorOr<PaginatedResponse<List<Item>>>>
{
    public string SearchTerm { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}