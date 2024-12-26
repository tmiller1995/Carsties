using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Data.IdentityConfigurations;

public sealed class IdentityUserLoginEntityConfiguration : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder)
    {
        builder.ToTable("user_logins");

        builder.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });

        builder.Property(ul => ul.LoginProvider)
            .IsRequired()
            .HasColumnName("login_provider");

        builder.Property(ul => ul.ProviderKey)
            .IsRequired()
            .HasColumnName("provider_key");

        builder.Property(ul => ul.ProviderDisplayName)
            .HasColumnName("provider_display_name");

        builder.Property(ul => ul.UserId)
            .IsRequired()
            .HasColumnName("user_id");

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(ul => ul.UserId)
            .HasPrincipalKey(u => u.Id)
            .HasConstraintName("fk_user_logins_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ul => ul.UserId)
            .HasDatabaseName("ix_user_logins_user_id");
    }
}