using ErrorOr;

namespace Auction.Domain.Interfaces;

public interface IAiImageService
{
    Task<ErrorOr<string>> GetOrCreateAsync(int year, string make, string model, string color,
        CancellationToken ct = default);
}