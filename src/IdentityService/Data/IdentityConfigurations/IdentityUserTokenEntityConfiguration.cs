using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Data.IdentityConfigurations;

public sealed class IdentityUserTokenEntityConfiguration : IEntityTypeConfiguration<IdentityUserToken<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> builder)
    {
        builder.ToTable("user_tokens");

        builder.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

        builder.Property(ut => ut.UserId)
            .IsRequired()
            .HasColumnName("user_id");

        builder.Property(ut => ut.LoginProvider)
            .IsRequired()
            .HasColumnName("login_provider");

        builder.Property(ut => ut.Name)
            .IsRequired()
            .HasColumnName("name");

        builder.Property(ut => ut.Value)
            .HasColumnName("value");

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(ut => ut.UserId)
            .HasPrincipalKey(u => u.Id)
            .HasConstraintName("fk_user_tokens_users_user_id")
            .OnDelete(DeleteBehavior.NoAction);
    }
}