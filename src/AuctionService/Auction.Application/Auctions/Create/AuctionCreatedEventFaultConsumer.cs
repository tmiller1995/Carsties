using Carsties.Shared.MessagingContracts;
using MassTransit;

namespace Auction.Application.Auctions.Create;

public class AuctionCreatedEventFaultConsumer : IConsumer<Fault<AuctionCreatedEvent>>
{
    public async Task Consume(ConsumeContext<Fault<AuctionCreatedEvent>> context)
    {
        await context.Publish(context.Message.Message, context.CancellationToken);
    }
}