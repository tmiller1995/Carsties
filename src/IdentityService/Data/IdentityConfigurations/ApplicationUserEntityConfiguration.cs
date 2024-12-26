using IdentityService.Data.ValueGenerators;
using IdentityService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Data.IdentityConfigurations;

public sealed class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id)
            .HasName("pk_users");

        builder.Property(u => u.Id)
            .IsRequired()
            .HasValueGenerator<GuidVersion7Generator>()
            .HasColumnName("id");

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName("username");

        builder.Property(u => u.NormalizedUserName)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName("normalized_username");

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName("email");

        builder.Property(u => u.NormalizedEmail)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName("normalized_email");

        builder.Property(u => u.EmailConfirmed)
            .IsRequired()
            .HasColumnName("email_confirmed");

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasColumnName("password_hash");

        builder.Property(u => u.SecurityStamp)
            .HasColumnName("security_stamp");

        builder.Property(u => u.ConcurrencyStamp)
            .HasColumnName("concurrency_stamp");

        builder.Property(u => u.PhoneNumber)
            .HasColumnName("phone_number");

        builder.Property(u => u.PhoneNumberConfirmed)
            .IsRequired()
            .HasColumnName("phone_number_confirmed");

        builder.Property(u => u.TwoFactorEnabled)
            .IsRequired()
            .HasColumnName("two_factor_enabled");

        builder.Property(u => u.LockoutEnd)
            .HasColumnName("lockout_end");

        builder.Property(u => u.LockoutEnabled)
            .IsRequired()
            .HasColumnName("lockout_enabled");

        builder.Property(u => u.AccessFailedCount)
            .IsRequired()
            .HasColumnName("access_failed_count");

        builder.HasIndex(u => u.NormalizedEmail)
            .HasDatabaseName("ix_users_normalized_email");

        builder.HasIndex(u => u.NormalizedUserName)
            .IsUnique()
            .HasDatabaseName("ix_users_normalized_username");
    }
}