using Raven.Client.Documents.Session;
using Search.Infrastructure.AuctionServiceClient;

namespace Search.Infrastructure.Data;

public sealed class DataSeeder
{
    private readonly IAsyncDocumentSession _documentSession;
    private readonly AuctionService _auctionService;

    public DataSeeder(IAsyncDocumentSession documentSession, AuctionService auctionService)
    {
        _documentSession = documentSession;
        _auctionService = auctionService;
    }

    public async Task SeedAsync()
    {
        var auctionsErrorOr = await _auctionService.GetAuctionsAsync();

        if (auctionsErrorOr.IsError)
            return;

        foreach (var auction in auctionsErrorOr.Value)
        {
            await _documentSession.StoreAsync(auction);
        }

        await _documentSession.SaveChangesAsync();
    }
}