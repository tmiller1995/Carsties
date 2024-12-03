namespace Auction.Application.Interfaces;

public interface IAuctionsRepository
{
    Task<List<Domain.Auctions.Auction>> GetAuctionsAsync(CancellationToken cancellationToken = default);
    Task<Domain.Auctions.Auction?> GetAuctionByIdAsync(Guid auctionId, CancellationToken cancellationToken = default);
    Task<Domain.Auctions.Auction?> CreateAuctionAsync(Domain.Auctions.Auction auction, CancellationToken cancellationToken = default);
}