using IdentityService.Data.ValueGenerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Data.IdentityConfigurations;

public sealed class IdentityRoleClaimEntityConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
    {
        builder.ToTable("role_claims");

        builder.HasKey(rc => rc.Id)
            .HasName("pk_role_claims");

        builder.Property(rc => rc.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(rc => rc.RoleId)
            .IsRequired()
            .HasColumnName("role_id");

        builder.Property(rc => rc.ClaimType)
            .HasColumnName("claim_type");

        builder.Property(rc => rc.ClaimValue)
            .HasColumnName("claim_value");

        builder.HasOne<IdentityRole<Guid>>()
            .WithMany()
            .HasForeignKey(rc => rc.RoleId)
            .HasPrincipalKey(r => r.Id)
            .HasConstraintName("fk_role_claims_roles_role_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(rc => rc.RoleId)
            .HasDatabaseName("ix_role_claims_role_id");
    }
}