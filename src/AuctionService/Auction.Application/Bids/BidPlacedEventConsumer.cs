using Auction.Application.Interfaces;
using Carsties.Shared.MessagingContracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Auction.Application.Bids;

public sealed class BidPlacedEventConsumer : IConsumer<BidPlacedEvent>
{
    private readonly ILogger<BidPlacedEventConsumer> _logger;
    private readonly IAuctionsRepository _auctionsRepository;

    public BidPlacedEventConsumer(ILogger<BidPlacedEventConsumer> logger, IAuctionsRepository auctionsRepository)
    {
        _logger = logger;
        _auctionsRepository = auctionsRepository;
    }

    public async Task Consume(ConsumeContext<BidPlacedEvent> context)
    {
        var auction = await _auctionsRepository.GetAuctionByIdAsync(context.Message.AuctionId, context.CancellationToken);

        if (auction is null)
        {
            _logger.LogInformation("No auction found with ID {Id}", context.Message.AuctionId);
            return;
        }

        var updatedResult = auction.UpdateCurrentHighBid(context.Message.BidStatus, context.Message.Amount);

        if (updatedResult.IsError)
        {
            _logger.LogError("The following error occurred updating the highest bidder: {ErrorMessage}", updatedResult.Errors.First().Description);
            return;
        }

        await _auctionsRepository.UpdateAuctionByIdAsync(auction, context.CancellationToken);
    }
}