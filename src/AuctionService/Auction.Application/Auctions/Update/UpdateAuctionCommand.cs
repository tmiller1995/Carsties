using Auction.Domain.Auctions;
using ErrorOr;
using MediatR;

namespace Auction.Application.Auctions.Update;

public readonly record struct UpdateAuctionCommand : IRequest<ErrorOr<AuctionEntity>>
{
    public Guid Id { get; init; }
    public string? Make { get; init; }
    public string? Model { get; init; }
    public string? Color { get; init; }
    public int? Mileage { get; init; }
    public int? Year { get; init; }
    public string UserUpdating { get; init; }
}