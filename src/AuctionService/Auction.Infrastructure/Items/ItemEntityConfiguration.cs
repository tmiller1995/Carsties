using Auction.Domain.Auctions;
using Auction.Domain.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.Infrastructure.Items;

public sealed class ItemEntityConfiguration : IEntityTypeConfiguration<ItemEntity>
{
    public void Configure(EntityTypeBuilder<ItemEntity> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .ValueGeneratedNever();

        builder.Property(i => i.Make)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Model)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Year)
            .IsRequired();

        builder.Property(i => i.Color)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Mileage)
            .IsRequired();

        builder.Property(i => i.ImageUrl)
            .HasMaxLength(500);

        builder.HasOne<AuctionEntity>(i => i.AuctionEntity)
            .WithOne(a => a.ItemEntity);
    }
}