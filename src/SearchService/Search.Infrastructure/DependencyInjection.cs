using MassTransit;
using Microsoft.Extensions.Configuration;
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
            config.AddConsumersFromNamespaceContaining<AuctionCreatedEventConsumer>();

            config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: "search"));

            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host("rabbitmq", "/", hostConfig =>
                {
                });

                configurator.ReceiveEndpoint("search-auction-created", endpointConfigurator =>
                {
                    endpointConfigurator.UseMessageRetry(r => r.Interval(10, 100));
                    endpointConfigurator.ConfigureConsumer<AuctionCreatedEventConsumer>(context);
                });

                configurator.ReceiveEndpoint("search-auction-updated", endpointConfigurator =>
                {
                    endpointConfigurator.UseMessageRetry(r => r.Interval(10, 100));
                    endpointConfigurator.ConfigureConsumer<AuctionUpdatedEventConsumer>(context);
                });

                configurator.ReceiveEndpoint("search-auction-deleted", endpointConfigurator =>
                {
                    endpointConfigurator.UseMessageRetry(r => r.Interval(10, 100));
                    endpointConfigurator.ConfigureConsumer<AuctionDeletedEventConsumer>(context);
                });

                configurator.ConfigureEndpoints(context);
            });
        });

        builder.Services.AddRavenDbDocStore(options => options.SectionName = "auction-search-db");
        builder.Services.AddRavenDbAsyncSession();

        builder.Services.AddScoped<ISearchRepository, SearchRepository>();

        return builder;
    }
}