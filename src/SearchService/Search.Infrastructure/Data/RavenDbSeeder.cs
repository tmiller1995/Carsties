using System.Text.Json;
using Raven.Client.Documents.Session;
using Search.Domain.Auctions;
using Search.Domain.Items;

namespace Search.Infrastructure.Data;

public class RavenDbSeeder
{
    private readonly IDocumentSession _documentSession;

    #region JsonData

    private const string JsonData = """
                                    [
                                      {
                                        "id": "0193c859-d010-7145-a6de-382fde77b4f8",
                                        "createdAt": "2024-12-15T03:25:06.95597Z",
                                        "updatedAt": "2024-12-15T03:25:06.956933Z",
                                        "auctionEnd": "2028-10-19T06:34:44.932502Z",
                                        "seller": "Elenor.Grady",
                                        "winner": null,
                                        "make": "Audi",
                                        "model": "R8",
                                        "year": 2022,
                                        "color": "white",
                                        "mileage": 75856,
                                        "imageUrl": "https://via.placeholder.com/150x150/cccccc/9c9c9c.png",
                                        "status": "Live",
                                        "reservePrice": 212494.2812055791,
                                        "soldAmount": null,
                                        "currentHighBid": null
                                      },
                                      {
                                        "id": "0193c859-d010-796a-b7f6-07313f382819",
                                        "createdAt": "2024-12-15T03:25:06.95597Z",
                                        "updatedAt": "2024-12-15T03:25:06.956933Z",
                                        "auctionEnd": "2026-03-05T11:57:09.503843Z",
                                        "seller": "Alda_Kling",
                                        "winner": null,
                                        "make": "Chevrolet",
                                        "model": "Silverado",
                                        "year": 1984,
                                        "color": "plum",
                                        "mileage": 70605,
                                        "imageUrl": "https://via.placeholder.com/150x150/cccccc/9c9c9c.png",
                                        "status": "Live",
                                        "reservePrice": 359020.73594695935,
                                        "soldAmount": null,
                                        "currentHighBid": null
                                      },
                                      {
                                        "id": "0193c859-d010-7322-af85-8600764a231d",
                                        "createdAt": "2024-12-15T03:25:06.95597Z",
                                        "updatedAt": "2024-12-15T03:25:06.956933Z",
                                        "auctionEnd": "2028-03-07T20:49:13.726358Z",
                                        "seller": "Leonor_Quigley",
                                        "winner": null,
                                        "make": "Ford",
                                        "model": "Mustang",
                                        "year": 1984,
                                        "color": "tan",
                                        "mileage": 190575,
                                        "imageUrl": "https://via.placeholder.com/150x150/cccccc/9c9c9c.png",
                                        "status": "Live",
                                        "reservePrice": 156907.09157237172,
                                        "soldAmount": null,
                                        "currentHighBid": null
                                      },
                                      {
                                        "id": "0193c859-d010-7be8-9d68-55ea55a323b0",
                                        "createdAt": "2024-12-15T03:25:06.95597Z",
                                        "updatedAt": "2024-12-15T03:25:06.956933Z",
                                        "auctionEnd": "2026-10-06T09:12:10.238182Z",
                                        "seller": "Warren_Lind47",
                                        "winner": null,
                                        "make": "Lamborghini",
                                        "model": "Countach",
                                        "year": 1969,
                                        "color": "black",
                                        "mileage": 269949,
                                        "imageUrl": "https://via.placeholder.com/150x150/cccccc/9c9c9c.png",
                                        "status": "Live",
                                        "reservePrice": 360981.2685386188,
                                        "soldAmount": null,
                                        "currentHighBid": null
                                      },
                                      {
                                        "id": "0193c859-d010-7b97-9679-68c11a0398b2",
                                        "createdAt": "2024-12-15T03:25:06.95597Z",
                                        "updatedAt": "2024-12-15T03:25:06.956933Z",
                                        "auctionEnd": "2026-08-30T04:01:14.375021Z",
                                        "seller": "Philip20",
                                        "winner": null,
                                        "make": "Chrysler",
                                        "model": "LeBaron",
                                        "year": 1978,
                                        "color": "grey",
                                        "mileage": 342879,
                                        "imageUrl": "https://via.placeholder.com/150x150/cccccc/9c9c9c.png",
                                        "status": "Live",
                                        "reservePrice": 246837.26025132305,
                                        "soldAmount": null,
                                        "currentHighBid": null
                                      },
                                      {
                                        "id": "0193c859-cfef-7ef0-815e-b27960925ff0",
                                        "createdAt": "2024-12-15T03:25:06.95597Z",
                                        "updatedAt": "2024-12-15T03:25:06.956933Z",
                                        "auctionEnd": "2025-08-27T10:15:26.922057Z",
                                        "seller": "Elliot69",
                                        "winner": null,
                                        "make": "Cadillac",
                                        "model": "Escalade",
                                        "year": 1977,
                                        "color": "olive",
                                        "mileage": 145999,
                                        "imageUrl": "https://via.placeholder.com/150x150/cccccc/9c9c9c.png",
                                        "status": "Live",
                                        "reservePrice": 225238.38896548303,
                                        "soldAmount": null,
                                        "currentHighBid": null
                                      },
                                      {
                                        "id": "0193c859-d010-7d6d-9c1f-743bbeba8084",
                                        "createdAt": "2024-12-15T03:25:06.95597Z",
                                        "updatedAt": "2024-12-15T03:25:06.956933Z",
                                        "auctionEnd": "2028-04-10T05:09:01.881622Z",
                                        "seller": "Vesta50",
                                        "winner": null,
                                        "make": "Ford",
                                        "model": "Ranchero",
                                        "year": 1990,
                                        "color": "orange",
                                        "mileage": 107989,
                                        "imageUrl": "https://via.placeholder.com/150x150/cccccc/9c9c9c.png",
                                        "status": "Live",
                                        "reservePrice": 446757.0486789369,
                                        "soldAmount": null,
                                        "currentHighBid": null
                                      },
                                      {
                                        "id": "0193c859-d010-75a9-9406-e54c1c6b2635",
                                        "createdAt": "2024-12-15T03:25:06.95597Z",
                                        "updatedAt": "2024-12-15T03:25:06.956933Z",
                                        "auctionEnd": "2026-01-26T20:38:03.150671Z",
                                        "seller": "Tom_Braun",
                                        "winner": null,
                                        "make": "Chrysler",
                                        "model": "PT Cruiser",
                                        "year": 1968,
                                        "color": "gold",
                                        "mileage": 27301,
                                        "imageUrl": "https://via.placeholder.com/150x150/cccccc/9c9c9c.png",
                                        "status": "Live",
                                        "reservePrice": 418849.87490663753,
                                        "soldAmount": null,
                                        "currentHighBid": null
                                      },
                                      {
                                        "id": "0193c859-d010-7489-b3d7-279849ad29ce",
                                        "createdAt": "2024-12-15T03:25:06.95597Z",
                                        "updatedAt": "2024-12-15T03:25:06.956933Z",
                                        "auctionEnd": "2029-06-10T05:21:20.233416Z",
                                        "seller": "Aurelio20",
                                        "winner": null,
                                        "make": "Toyota",
                                        "model": "Prius",
                                        "year": 1931,
                                        "color": "gold",
                                        "mileage": 7885,
                                        "imageUrl": "https://via.placeholder.com/150x150/cccccc/9c9c9c.png",
                                        "status": "Live",
                                        "reservePrice": 104560.03929700726,
                                        "soldAmount": null,
                                        "currentHighBid": null
                                      },
                                      {
                                        "id": "0193c859-d011-7ec3-8871-6bd38d5266e2",
                                        "createdAt": "2024-12-15T03:25:06.95597Z",
                                        "updatedAt": "2024-12-15T03:25:06.956933Z",
                                        "auctionEnd": "2027-04-02T09:52:01.509363Z",
                                        "seller": "Bernhard.Durgan47",
                                        "winner": null,
                                        "make": "Jeep",
                                        "model": "Grand Cherokee",
                                        "year": 2018,
                                        "color": "ivory",
                                        "mileage": 111905,
                                        "imageUrl": "https://via.placeholder.com/150x150/cccccc/9c9c9c.png",
                                        "status": "Live",
                                        "reservePrice": 141101.72427776328,
                                        "soldAmount": null,
                                        "currentHighBid": null
                                      }
                                    ]
                                    """;

    #endregion

    public RavenDbSeeder(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public void SeedRavenDb()
    {
        var auctionDtos = JsonSerializer.Deserialize<AuctionDto[]>(JsonData);
        if (auctionDtos is null)
          return;

        if (_documentSession.Query<Auction>().Any())
          return;

        foreach (var auctionDto in auctionDtos)
        {
            var item = new Item
            {
                Id = auctionDto.Id.ToString(),
                Color = auctionDto.Color,
                Make = auctionDto.Make,
                Model = auctionDto.Model,
                Year = auctionDto.Year,
                Mileage = auctionDto.Mileage,
                ImageUrl = auctionDto.ImageUrl
            };

            var auction = new Auction
            {
                Id = auctionDto.Id.ToString(),
                ReservePrice = auctionDto.ReservePrice,
                Seller = auctionDto.Seller,
                Winner = auctionDto.Winner,
                SoldAmount = auctionDto.SoldAmount,
                CurrentHighBid = auctionDto.CurrentHighBid,
                CreatedAt = auctionDto.CreatedAt,
                UpdatedAt = auctionDto.UpdatedAt,
                AuctionEnd = auctionDto.AuctionEnd,
                Status = Enum.Parse<Status>(auctionDto.Status),
                Item = item
            };

            _documentSession.Store(auction);
        }

        _documentSession.SaveChanges();
    }
}