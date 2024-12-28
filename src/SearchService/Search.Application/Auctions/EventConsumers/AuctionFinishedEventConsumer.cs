using Carsties.Shared.MessagingContracts;
using MassTransit;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Search.Domain.Auctions;

namespace Search.Application.Auctions.EventConsumers;

public sealed class AuctionFinishedEventConsumer : IConsumer<AuctionFinishedEvent>
{
    private readonly IAsyncDocumentSession _documentSession;

    public AuctionFinishedEventConsumer(IAsyncDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task Consume(ConsumeContext<AuctionFinishedEvent> context)
    {
        var auction = await _documentSession.Query<Auction>()
            .FirstOrDefaultAsync(a => a.ExternalId == context.Message.AuctionId, context.CancellationToken);

        if (context.Message.ItemSold)
        {
            auction.Winner = context.Message.Winner;
            auction.SoldAmount = context.Message.Amount;
        }

        auction.Status = auction.SoldAmount > auction.ReservePrice ? Status.Finished : Status.ReserveNotMet;

        await _documentSession.SaveChangesAsync(context.CancellationToken);
    }
}