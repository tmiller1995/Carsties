using Auction.Domain.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.Infrastructure.Items;

public sealed class ItemEntityConfiguration : IEntityTypeConfiguration<ItemEntity>
{
    public void Configure(EntityTypeBuilder<ItemEntity> builder)
    {
        builder.ToTable("items");

        builder.HasKey(i => i.Id)
            .HasName("pk_items_id");
        builder.Property(i => i.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");

        builder.Property(i => i.Make)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("make");

        builder.Property(i => i.Model)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("model");

        builder.Property(i => i.Year)
            .IsRequired()
            .HasColumnName("year");

        builder.Property(i => i.Color)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("color");

        builder.Property(i => i.Mileage)
            .IsRequired()
            .HasColumnName("mileage");

        builder.Property(i => i.ImageUrl)
            .HasMaxLength(500)
            .HasColumnName("image_url");
    }
}