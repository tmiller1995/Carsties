using Auction.Application.Interfaces;
using Auction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Auction.Infrastructure.Auctions;

public sealed class AuctionsRepository : IAuctionsRepository
{
    private readonly AuctionDbContext _auctionDbContext;

    public AuctionsRepository(AuctionDbContext auctionDbContext)
    {
        _auctionDbContext = auctionDbContext;
    }

    public async Task<List<Domain.Auctions.Auction>> GetAuctionsAsync(CancellationToken cancellationToken = default)
    {
        var auctions = await _auctionDbContext.Auctions
            .Include(a => a.Item)
            .ToListAsync(cancellationToken);

        return auctions;
    }

    public async Task<Domain.Auctions.Auction?> GetAuctionByIdAsync(Guid auctionId, CancellationToken cancellationToken = default)
    {
        var auction = await _auctionDbContext.Auctions
            .Include(a => a.Item)
            .FirstOrDefaultAsync(a => a.Id == auctionId, cancellationToken);

        return auction;
    }

    public async Task<Domain.Auctions.Auction?> CreateAuctionAsync(Domain.Auctions.Auction auction, CancellationToken cancellationToken = default)
    {
        var existingAuction = await _auctionDbContext.Auctions
            .FindAsync([auction.Id], cancellationToken);

        if (existingAuction is not null)
            return null;

        var auctionEntity = _auctionDbContext.Auctions.Add(auction);
        await _auctionDbContext.SaveChangesAsync(cancellationToken);
        return auctionEntity.Entity;
    }
}