using Carsties.Shared.MessagingContracts;
using MassTransit;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Search.Domain.Auctions;

namespace Search.Application.Bids.EventConsumers;

public sealed class BidPlacedEventConsumer : IConsumer<BidPlacedEvent>
{
    private readonly IAsyncDocumentSession _documentSession;

    public BidPlacedEventConsumer(IAsyncDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task Consume(ConsumeContext<BidPlacedEvent> context)
    {
        var auction = await _documentSession.Query<Auction>()
            .FirstOrDefaultAsync(a => a.ExternalId == context.Message.AuctionId, context.CancellationToken);

        if (!auction.CurrentHighBid.HasValue ||
            string.Equals(context.Message.BidStatus, "Accepted", StringComparison.OrdinalIgnoreCase) &&
            context.Message.Amount > auction.CurrentHighBid)
        {
            auction.CurrentHighBid = context.Message.Amount;
            await _documentSession.SaveChangesAsync(context.CancellationToken);
        }
    }
}