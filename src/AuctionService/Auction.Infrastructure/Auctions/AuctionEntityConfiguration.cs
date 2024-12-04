using Auction.Domain.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.Infrastructure.Auctions;

public sealed class AuctionEntityConfiguration : IEntityTypeConfiguration<Domain.Auctions.AuctionEntity>
{
    public void Configure(EntityTypeBuilder<Domain.Auctions.AuctionEntity> builder)
    {
        builder.ToTable("auctions");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .ValueGeneratedNever();

        builder.Property(a => a.Seller)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.CreatedAt)
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(a => a.UpdatedAt)
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(a => a.Status)
            .IsRequired();

        builder.HasOne<ItemEntity>(a => a.ItemEntity)
            .WithOne(i => i.AuctionEntity);
    }
}