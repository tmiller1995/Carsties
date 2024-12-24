using Carsties.Shared.MessagingContracts;
using MassTransit;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Search.Domain.Auctions;

namespace Search.Application.Auctions.EventConsumers;

public sealed class AuctionDeletedConsumer : IConsumer<AuctionDeletedEvent>
{
    private readonly IAsyncDocumentSession _documentSession;

    public AuctionDeletedConsumer(IAsyncDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task Consume(ConsumeContext<AuctionDeletedEvent> context)
    {
        var auctionToDelete = context.Message;
        var auction = await _documentSession.Query<Auction>()
            .FirstOrDefaultAsync(a => a.ExternalId == auctionToDelete.Id, context.CancellationToken);

        if (auction is not null)
        {
            _documentSession.Delete(auction);
            await _documentSession.SaveChangesAsync(context.CancellationToken);
        }
    }
}