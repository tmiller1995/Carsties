using Auction.Contract.Dtos;
using Auction.Domain.Auctions;
using Auction.Domain.Items;

namespace AuctionService.API.Auctions.Mapper;

public static class AuctionMapper
{
    public static AuctionDto ToAuctionDto(this Auction.Domain.Auctions.Auction auction)
    {
        return new AuctionDto
        {
            Id = auction.Id,
            Seller = auction.Seller,
            Winner = auction.Winner,
            SoldAmount = auction.SoldAmount,
            CurrentHighBid = auction.CurrentHighBid,
            CreatedAt = auction.CreatedAt,
            UpdatedAt = auction.UpdatedAt,
            AuctionEnd = auction.AuctionEnd,
            Status = auction.Status.ToString(),
            Make = auction.Item.Make,
            Model = auction.Item.Model,
            Year = auction.Item.Year
        };
    }

    public static Auction.Domain.Auctions.Auction ToAuction(this AuctionDto auctionDto)
    {
        var auction = new Auction.Domain.Auctions.Auction(auctionDto.ReservePrice,
            auctionDto.Seller,
            auctionDto.Winner,
            auctionDto.SoldAmount,
            auctionDto.CurrentHighBid,
            auctionDto.CreatedAt,
            auctionDto.UpdatedAt,
            auctionDto.AuctionEnd,
            Enum.Parse<Status>(auctionDto.Status),
            auctionDto.Id)
        {
            Item = new Item(auctionDto.Make,
                auctionDto.Model,
                auctionDto.Year,
                auctionDto.Color,
                auctionDto.Mileage,
                auctionDto.ImageUrl)
        };

        return auction;
    }

    public static Auction.Domain.Auctions.Auction ToAuction(this CreateAuctionDto createAuctionDto, string seller)
    {
        return new Auction.Domain.Auctions.Auction(createAuctionDto.ReservePrice,
            seller,
            winner: null,
            soldAmount: null,
            currentHighBid: null,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow,
            createAuctionDto.AuctionEnd,
            Status.Live)
        {
            Item = new Item(createAuctionDto.Make,
                createAuctionDto.Model,
                createAuctionDto.Year,
                createAuctionDto.Color,
                createAuctionDto.Mileage,
                createAuctionDto.ImageUrl)
        };
    }
}