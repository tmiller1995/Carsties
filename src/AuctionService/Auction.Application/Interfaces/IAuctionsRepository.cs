namespace Auction.Application.Interfaces;

public interface IAuctionsRepository
{
    Task<List<Domain.Auctions.AuctionEntity>> GetAuctionsAsync(CancellationToken cancellationToken = default);
    Task<Domain.Auctions.AuctionEntity?> GetAuctionByIdAsync(Guid auctionId, CancellationToken cancellationToken = default);
    Task<Domain.Auctions.AuctionEntity?> CreateAuctionAsync(Domain.Auctions.AuctionEntity auctionToCreate, CancellationToken cancellationToken = default);
}