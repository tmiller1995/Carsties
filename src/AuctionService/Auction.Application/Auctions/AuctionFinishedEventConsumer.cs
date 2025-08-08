using Auction.Application.Interfaces;
using Carsties.Shared.MessagingContracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Auction.Application.Auctions;

public sealed class AuctionFinishedEventConsumer : IConsumer<AuctionFinishedEvent>
{
    private readonly ILogger<AuctionFinishedEventConsumer> _logger;
    private readonly IAuctionsRepository _auctionsRepository;

    public AuctionFinishedEventConsumer(ILogger<AuctionFinishedEventConsumer> logger,
        IAuctionsRepository auctionsRepository)
    {
        _logger = logger;
        _auctionsRepository = auctionsRepository;
    }

    public async Task Consume(ConsumeContext<AuctionFinishedEvent> context)
    {
        var auction =
            await _auctionsRepository.GetAuctionByIdAsync(context.Message.AuctionId, context.CancellationToken);

        if (auction is null)
        {
            _logger.LogInformation("No auction found with ID {Id}", context.Message.AuctionId);
            return;
        }

        if (context.Message.ItemSold)
        {
            auction.UpdateWinnerAndSoldAmount(context.Message.Winner, context.Message.Amount);
        }

        auction.UpdateStatus();

        await _auctionsRepository.UpdateAuctionByIdAsync(auction, context.CancellationToken);
    }
}