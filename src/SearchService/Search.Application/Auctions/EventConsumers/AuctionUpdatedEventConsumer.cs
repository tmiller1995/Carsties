using Carsties.Shared.MessagingContracts;
using MassTransit;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Search.Domain.Auctions;

namespace Search.Application.Auctions.EventConsumers;

public sealed class AuctionUpdatedEventConsumer : IConsumer<AuctionUpdatedEvent>
{
    private readonly IAsyncDocumentSession _documentSession;

    public AuctionUpdatedEventConsumer(IAsyncDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task Consume(ConsumeContext<AuctionUpdatedEvent> context)
    {
        var auctionUpdated = context.Message;
        var auction = await _documentSession.Query<Auction>()
            .FirstOrDefaultAsync(a => a.ExternalId == auctionUpdated.Id, context.CancellationToken);

        if (auction is not null)
        {
            if (!string.IsNullOrWhiteSpace(auctionUpdated.Make))
                auction.Item.Make = auctionUpdated.Make;

            if (!string.IsNullOrWhiteSpace(auctionUpdated.Model))
                auction.Item.Model = auctionUpdated.Model;

            if (!string.IsNullOrWhiteSpace(auctionUpdated.Color))
                auction.Item.Color = auctionUpdated.Color;

            if (auctionUpdated.Mileage.HasValue)
                auction.Item.Mileage = auctionUpdated.Mileage.Value;

            if (auctionUpdated.Year.HasValue)
                auction.Item.Year = auctionUpdated.Year.Value;

            await _documentSession.SaveChangesAsync(context.CancellationToken);
        }
    }
}