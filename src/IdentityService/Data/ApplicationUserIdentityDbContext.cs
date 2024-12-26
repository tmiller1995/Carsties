using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Data;

public class ApplicationUserIdentityDbContext : IdentityDbContext<ApplicationUser,
    IdentityRole<Guid>,
    Guid,
    IdentityUserClaim<Guid>,
    IdentityUserRole<Guid>,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>>
{
    public ApplicationUserIdentityDbContext(DbContextOptions<ApplicationUserIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationUserIdentityDbContext).Assembly);
    }
}