using Auction.Domain.Auctions;
using Auction.Domain.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.Infrastructure.Auctions;

public sealed class AuctionEntityConfiguration : IEntityTypeConfiguration<AuctionEntity>
{
    public void Configure(EntityTypeBuilder<AuctionEntity> builder)
    {
        builder.ToTable("auctions");

        builder.HasKey(a => a.Id)
            .HasName("pk_auctions_id");
        builder.Property(a => a.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");

        builder.Property(a => a.ReservePrice)
            .HasColumnName("reserve_price")
            .IsRequired();

        builder.Property(a => a.Seller)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("seller");

        builder.Property(a => a.Winner)
            .HasMaxLength(200)
            .HasColumnName("winner");

        builder.Property(a => a.SoldAmount)
            .HasColumnName("sold_amount");

        builder.Property(a => a.CurrentHighBid)
            .HasColumnName("current_high_bid");

        builder.Property(a => a.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(a => a.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");

        builder.Property(a => a.AuctionEnd)
            .IsRequired()
            .HasColumnName("auction_end");

        builder.Property(a => a.Status)
            .IsRequired()
            .HasColumnName("status");

        builder.HasOne<ItemEntity>(a => a.ItemEntity)
            .WithOne(i => i.AuctionEntity)
            .HasForeignKey<ItemEntity>(i => i.Id)
            .HasConstraintName("fk_auctions_items_item_id");
    }
}