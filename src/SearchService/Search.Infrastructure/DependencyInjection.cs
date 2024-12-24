using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Raven.DependencyInjection;
using Search.Application.Auctions.EventConsumers;
using Search.Application.Interfaces;
using Search.Infrastructure.Searches;

namespace Search.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication().AddJwtBearer();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddMassTransit(config =>
        {
            config.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
            config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: "search"));
            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.ReceiveEndpoint("search-auction-created", endpointConfigurator =>
                {
                    endpointConfigurator.UseMessageRetry(r => r.Interval(2, 100));
                    endpointConfigurator.ConfigureConsumer<AuctionCreatedConsumer>(context);
                });
                configurator.ConfigureEndpoints(context);
            });
        });

        builder.Services.AddRavenDbDocStore(options => options.SectionName = "RavenDbSettings");
        builder.Services.AddRavenDbAsyncSession();

        builder.Services.AddScoped<ISearchRepository, SearchRepository>();

        return builder;
    }
}