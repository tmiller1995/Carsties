using IdentityService.Data.ValueGenerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Data.IdentityConfigurations;

public sealed class IdentityRoleEntityConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(r => r.Id)
            .HasName("pk_roles");

        builder.Property(r => r.Id)
            .IsRequired()
            .HasValueGenerator<GuidVersion7Generator>()
            .HasColumnName("id");

        builder.Property(r => r.Name)
            .HasMaxLength(256)
            .HasColumnName("name");

        builder.Property(r => r.NormalizedName)
            .HasMaxLength(256)
            .HasColumnName("normalized_name");

        builder.Property(r => r.ConcurrencyStamp)
            .HasColumnName("concurrency_stamp");

        builder.HasIndex(r => r.NormalizedName)
            .IsUnique()
            .HasDatabaseName("ix_roles_normalized_name");
    }
}