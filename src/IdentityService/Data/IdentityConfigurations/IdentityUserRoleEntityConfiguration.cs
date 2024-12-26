using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Data.IdentityConfigurations;

public sealed class IdentityUserRoleEntityConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.ToTable("user_roles");

        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.Property(ur => ur.UserId)
            .IsRequired()
            .HasColumnName("user_id");

        builder.Property(ur => ur.RoleId)
            .IsRequired()
            .HasColumnName("role_id");

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(ur => ur.UserId)
            .HasPrincipalKey(u => u.Id)
            .HasConstraintName("fk_user_roles_users_user_id")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<IdentityRole<Guid>>()
            .WithMany()
            .HasForeignKey(ur => ur.RoleId)
            .HasPrincipalKey(r => r.Id)
            .HasConstraintName("fk_user_roles_roles_role_id")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(ur => ur.RoleId)
            .HasDatabaseName("ix_user_roles_role_id");
    }
}