using Auction.Domain.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.Infrastructure.Auctions;

public sealed class AuctionEntityConfiguration : IEntityTypeConfiguration<Domain.Auctions.AuctionEntity>
{
    public void Configure(EntityTypeBuilder<Domain.Auctions.AuctionEntity> builder)
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
            .HasDefaultValue(DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc))
            .HasColumnName("created_at");

        builder.Property(a => a.UpdatedAt)
            .IsRequired()
            .HasDefaultValue(DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc))
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