using Carsties.Shared.MessagingContracts;
using MassTransit;
using Raven.Client.Documents.Session;
using Search.Domain.Auctions;
using Search.Domain.Items;

namespace Search.Application.Auctions.EventConsumers;

public sealed class AuctionCreatedConsumer : IConsumer<AuctionCreatedEvent>
{
    private readonly IAsyncDocumentSession _documentSession;

    public AuctionCreatedConsumer(IAsyncDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task Consume(ConsumeContext<AuctionCreatedEvent> context)
    {
        var auctionCreated = context.Message;
        var auction = new Auction
        {
            ExternalId = auctionCreated.Id,
            CreatedAt = auctionCreated.CreatedAt,
            UpdatedAt = auctionCreated.UpdatedAt,
            AuctionEnd = auctionCreated.AuctionEnd,
            Seller = auctionCreated.Seller,
            Winner = auctionCreated.Winner,
            Status = Enum.Parse<Status>(auctionCreated.Status),
            ReservePrice = auctionCreated.ReservePrice,
            SoldAmount = auctionCreated.SoldAmount,
            CurrentHighBid = auctionCreated.CurrentHighBid,
            Item = new Item
            {
                Make = auctionCreated.Make,
                Model = auctionCreated.Model,
                Year = auctionCreated.Year,
                Color = auctionCreated.Color,
                Mileage = auctionCreated.Mileage,
                ImageUrl = auctionCreated.ImageUrl
            }
        };

        await _documentSession.StoreAsync(auction);
        await _documentSession.SaveChangesAsync();
    }
}