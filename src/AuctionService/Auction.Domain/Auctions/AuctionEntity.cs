using Auction.Domain.Items;
using Carsties.Core;
using Carsties.Shared.MessagingContracts;
using System.Text.Json.Serialization;
using ErrorOr;

namespace Auction.Domain.Auctions;

public sealed class AuctionEntity : Entity
{
    public decimal ReservePrice { get; private init; }
    public string Seller { get; private init; } = string.Empty;
    public string? Winner { get; private set; }
    public decimal? SoldAmount { get; private set; }
    public decimal? CurrentHighBid { get; private set; }
    public DateTime CreatedAt { get; private init; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    public DateTime UpdatedAt { get; private init; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    public DateTime AuctionEnd { get; private init; }
    public Status Status { get; private set; }
    public ItemEntity ItemEntity { get; init; } = null!;

    public AuctionEntity()
    {
    }

    public AuctionEntity(decimal reservePrice,
        string seller,
        string? winner,
        decimal? soldAmount,
        decimal? currentHighBid,
        DateTime createdAt,
        DateTime updatedAt,
        DateTime auctionEnd,
        Status status,
        Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        ReservePrice = reservePrice;
        Seller = seller;
        Winner = winner;
        SoldAmount = soldAmount;
        CurrentHighBid = currentHighBid;
        CreatedAt = DateTime.SpecifyKind(createdAt, DateTimeKind.Utc);
        UpdatedAt = DateTime.SpecifyKind(updatedAt, DateTimeKind.Utc);
        AuctionEnd = DateTime.SpecifyKind(auctionEnd, DateTimeKind.Utc);
        Status = status;
    }

    public void UpdateWinnerAndSoldAmount(string winner, decimal soldAmount)
    {
        Winner = winner;
        SoldAmount = soldAmount;
    }

    public void UpdateStatus()
    {
        Status = SoldAmount > ReservePrice ? Status.Finished : Status.ReserveNotMet;
    }

    public ErrorOr<bool> UpdateCurrentHighBid(string bidStatus, decimal bidAmount)
    {
        if (CurrentHighBid.HasValue &&
            (!string.Equals(bidStatus, "Accepted", StringComparison.OrdinalIgnoreCase) ||
             !(bidAmount > CurrentHighBid)))
            return Error.Validation("CurrentHighBid", "Bid amount is not higher than current high bid");

        CurrentHighBid = bidAmount;
        return true;
    }

    public AuctionCreatedEvent GetCreatedEvent()
    {
        var auctionCreatedEvent = new AuctionCreatedEvent
        {
            Id = Id,
            ReservePrice = ReservePrice,
            Seller = Seller,
            Winner = Winner,
            SoldAmount = SoldAmount,
            CurrentHighBid = CurrentHighBid,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            AuctionEnd = AuctionEnd,
            Status = Status.ToString(),
            Make = ItemEntity.Make,
            Model = ItemEntity.Model,
            Year = ItemEntity.Year,
            Color = ItemEntity.Color,
            Mileage = ItemEntity.Mileage,
            ImageUrl = ItemEntity.ImageUrl
        };

        return auctionCreatedEvent;
    }

    public AuctionUpdatedEvent GetUpdatedEvent()
    {
        var auctionUpdatedEvent = new AuctionUpdatedEvent
        {
            Id = Id,
            Make = ItemEntity.Make,
            Model = ItemEntity.Model,
            Color = ItemEntity.Color,
            Mileage = ItemEntity.Mileage,
            Year = ItemEntity.Year
        };

        return auctionUpdatedEvent;
    }

    public AuctionDeletedEvent GetDeletedEvent()
    {
        var auctionDeletedEvent = new AuctionDeletedEvent { Id = Id };
        return auctionDeletedEvent;
    }
}