using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Raven.DependencyInjection;
using Search.Application.Interfaces;
using Search.Infrastructure.AuctionServiceClient;
using Search.Infrastructure.Data;
using Search.Infrastructure.Searches;

namespace Search.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication().AddJwtBearer();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddRavenDbDocStore(options => options.SectionName = "RavenDbSettings");
        builder.Services.AddRavenDbAsyncSession();

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddHttpClient<AuctionService>(client =>
            {
                var auctionServiceUrl = builder.Configuration["AuctionServiceUrl"]!;
                client.BaseAddress = new Uri(auctionServiceUrl);
            });

            builder.Services.AddScoped<DataSeeder>();
        }

        builder.Services.AddScoped<ISearchRepository, SearchRepository>();

        return builder;
    }
}