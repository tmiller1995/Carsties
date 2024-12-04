using Auction.Application.Interfaces;
using Auction.Infrastructure.Auctions;
using Auction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Auction.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication().AddJwtBearer();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddDbContext<AuctionDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("auction-database"));
        });
        builder.Services.AddScoped<IAuctionsRepository, AuctionsRepository>();

        return builder;
    }
}