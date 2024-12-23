using System.Text.Json;
using Auction.Contract.Dtos;
using ErrorOr;
using Search.Domain.Auctions;
using Search.Domain.Items;

namespace Search.Infrastructure.AuctionServiceClient;

public sealed class AuctionService
{
    private readonly HttpClient _httpClient;

    public AuctionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ErrorOr<List<Domain.Auctions.Auction>>> GetAuctionsAsync()
    {
        var response = await _httpClient.GetAsync("api/auctions");
        if (!response.IsSuccessStatusCode)
            return Error.Failure("Auctions", $"Received status code: {response.StatusCode}");

        var auctionDtos = await JsonSerializer.DeserializeAsync<List<AuctionDto>>(await response.Content.ReadAsStreamAsync());

        var auctions = new List<Domain.Auctions.Auction>();
        auctionDtos.ForEach(adto =>
        {
            var auction = new Domain.Auctions.Auction
            {
                ExternalId = adto.Id,
                ReservePrice = adto.ReservePrice,
                Seller = adto.Seller,
                Winner = adto.Winner,
                SoldAmount = adto.SoldAmount,
                CurrentHighBid = adto.CurrentHighBid,
                CreatedAt = adto.CreatedAt,
                UpdatedAt = adto.UpdatedAt,
                AuctionEnd = adto.AuctionEnd,
                Status = Enum.Parse<Status>(adto.Status),
                Item = new Item
                {
                    Make = adto.Make,
                    Model = adto.Model,
                    Year = adto.Year,
                    Color = adto.Color,
                    Mileage = adto.Mileage,
                    ImageUrl = adto.ImageUrl
                }
            };

            auctions.Add(auction);
        });

        return auctions;
    }
}