using Auction.Contract.Dtos;
using Auction.Domain.Auctions;
using Auction.Domain.Items;

namespace AuctionService.API.Auctions.Mapper;

public static class AuctionMapper
{
    public static AuctionDto ToAuctionDto(this AuctionEntity auctionEntity)
    {
        return new AuctionDto
        {
            Id = auctionEntity.Id,
            Seller = auctionEntity.Seller,
            Winner = auctionEntity.Winner,
            SoldAmount = auctionEntity.SoldAmount,
            CurrentHighBid = auctionEntity.CurrentHighBid,
            CreatedAt = auctionEntity.CreatedAt,
            UpdatedAt = auctionEntity.UpdatedAt,
            AuctionEnd = auctionEntity.AuctionEnd,
            Status = auctionEntity.Status.ToString(),
            Make = auctionEntity.ItemEntity.Make,
            Model = auctionEntity.ItemEntity.Model,
            Year = auctionEntity.ItemEntity.Year,
            Color = auctionEntity.ItemEntity.Color,
            Mileage = auctionEntity.ItemEntity.Mileage,
            ImageUrl = auctionEntity.ItemEntity.ImageUrl,
            ReservePrice = auctionEntity.ReservePrice
        };
    }

    public static AuctionEntity ToAuction(this AuctionDto auctionDto)
    {
        var auction = new AuctionEntity(auctionDto.ReservePrice,
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
            ItemEntity = new ItemEntity(auctionDto.Make,
                auctionDto.Model,
                auctionDto.Year,
                auctionDto.Color,
                auctionDto.Mileage,
                auctionDto.ImageUrl)
        };

        return auction;
    }

    public static AuctionEntity ToAuction(this CreateAuctionDto createAuctionDto, string seller)
    {
        return new AuctionEntity(createAuctionDto.ReservePrice,
            seller,
            winner: null,
            soldAmount: null,
            currentHighBid: null,
            createdAt: DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
            updatedAt: DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
            DateTime.SpecifyKind(createAuctionDto.AuctionEnd, DateTimeKind.Utc),
            Status.Live)
        {
            ItemEntity = new ItemEntity(createAuctionDto.Make,
                createAuctionDto.Model,
                createAuctionDto.Year,
                createAuctionDto.Color,
                createAuctionDto.Mileage,
                createAuctionDto.ImageUrl)
        };
    }
}