using Auction.Application.Interfaces;
using Auction.Domain.Auctions;
using Auction.Infrastructure.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Auction.Infrastructure.Auctions;

public sealed class AuctionsRepository : IAuctionsRepository
{
    private readonly AuctionDbContext _auctionDbContext;
    private readonly IPublishEndpoint _publishEndpoint;

    public AuctionsRepository(AuctionDbContext auctionDbContext, IPublishEndpoint publishEndpoint)
    {
        _auctionDbContext = auctionDbContext;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<List<AuctionEntity>> GetAuctionsAsync(CancellationToken cancellationToken = default)
    {
        var auctions = await _auctionDbContext.Auctions
            .Include(a => a.ItemEntity)
            .ToListAsync(cancellationToken);

        return auctions;
    }

    public async Task<AuctionEntity?> GetAuctionByIdAsync(Guid auctionId, CancellationToken cancellationToken = default)
    {
        var auction = await _auctionDbContext.Auctions
            .Include(a => a.ItemEntity)
            .FirstOrDefaultAsync(a => a.Id == auctionId, cancellationToken);

        return auction;
    }

    public async Task<AuctionEntity?> CreateAuctionAsync(AuctionEntity auctionToCreate, CancellationToken cancellationToken = default)
    {
        var existingAuction = await _auctionDbContext.Auctions
            .FindAsync([auctionToCreate.Id], cancellationToken);

        if (existingAuction is not null)
            return null;

        var auctionEntity = _auctionDbContext.Auctions.Add(auctionToCreate);
        await _publishEndpoint.Publish(auctionToCreate.GetCreatedEvent(), cancellationToken);
        await _auctionDbContext.SaveChangesAsync(cancellationToken);
        return auctionEntity.Entity;
    }

    public async Task<AuctionEntity> UpdateAuctionByIdAsync(AuctionEntity updatedAuction, CancellationToken cancellationToken = default)
    {
        var auction = _auctionDbContext.Auctions.Update(updatedAuction);
        await _publishEndpoint.Publish(auction.Entity.GetUpdatedEvent(), cancellationToken);
        await _auctionDbContext.SaveChangesAsync(cancellationToken);

        return auction.Entity;
    }

    public async Task<bool> DeleteAuctionAsync(AuctionEntity auctionToDelete, CancellationToken cancellationToken = default)
    {
        _auctionDbContext.Auctions.Remove(auctionToDelete);
        await _publishEndpoint.Publish(auctionToDelete.GetDeletedEvent(), cancellationToken);
        var result = await _auctionDbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}