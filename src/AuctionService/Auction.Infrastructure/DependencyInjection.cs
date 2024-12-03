using Auction.Application.Interfaces;
using Auction.Infrastructure.Auctions;
using Auction.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Auction.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<AuctionDbContext>(connectionName: "auction-database");
        builder.Services.AddScoped<IAuctionsRepository, AuctionsRepository>();

        return builder;
    }
}