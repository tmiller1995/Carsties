namespace Carsties.Core;

public sealed class PaginatedResponse<T>
{
    public T Data { get; init; }
    public int PageNumber { get; init; }
    public double PageSize { get; init; }
    public double TotalCount { get; init; }

    public int TotalPages => (int)Math.Ceiling(TotalCount / PageSize);
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;
}