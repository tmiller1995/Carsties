using IdentityService.Data.ValueGenerators;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Data.IdentityConfigurations;

public sealed class IdentityUserClaimEntityConfiguration : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> builder)
    {
        builder.ToTable("user_claims");

        builder.HasKey(uc => uc.Id)
            .HasName("pk_user_claims");

        builder.Property(uc => uc.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(uc => uc.UserId)
            .IsRequired()
            .HasColumnName("user_id");

        builder.Property(uc => uc.ClaimType)
            .HasColumnName("claim_type");

        builder.Property(uc => uc.ClaimValue)
            .HasColumnName("claim_value");

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(uc => uc.UserId)
            .HasPrincipalKey(u => u.Id)
            .HasConstraintName("fk_user_claims_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(uc => uc.UserId)
            .HasDatabaseName("ix_user_claims_user_id");
    }
}