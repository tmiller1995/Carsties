using System.Net.Http.Headers;
using Auction.Application.Auctions;
using Auction.Application.Auctions.Create;
using Auction.Application.Bids;
using Auction.Application.Interfaces;
using Auction.Domain.Interfaces;
using Auction.Infrastructure.Auctions;
using Auction.Infrastructure.Data;
using Auction.Infrastructure.ImageGeneration;
using IdentityModel;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Auction.Infrastructure;

public static class DependencyInjection
{
    public static TBuilder AddInfrastructure<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["IdentityService:Authority"];
                options.TokenValidationParameters.ValidateAudience = false;
                options.TokenValidationParameters.NameClaimType = JwtClaimTypes.PreferredUserName;
                options.RequireHttpsMetadata = false;
            });
        builder.Services.AddAuthorization();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddHttpClient<IAiImageService, OpenAiImageService>((client) =>
        {
            client.BaseAddress = new Uri("https://api.openai.com");
        });
        builder.Services.AddSingleton<SeedData>();
        builder.AddNpgsqlDbContext<AuctionDbContext>("auction-db");
        builder.AddMassTransitRabbitMq("rabbitmq", massTransitConfiguration: configurator =>
        {
            configurator.AddEntityFrameworkOutbox<AuctionDbContext>(options =>
            {
                options.QueryDelay = TimeSpan.FromSeconds(10);
                options.UsePostgres();
                options.UseBusOutbox();
            });

            configurator.AddConsumersFromNamespaceContaining<AuctionCreatedEventFaultConsumer>();
            configurator.AddConsumersFromNamespaceContaining<AuctionFinishedEventConsumer>();
            configurator.AddConsumersFromNamespaceContaining<BidPlacedEventConsumer>();

            configurator.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: "auction"));
        });

        builder.Services.AddScoped<IAuctionsRepository, AuctionsRepository>();

        return builder;
    }
}