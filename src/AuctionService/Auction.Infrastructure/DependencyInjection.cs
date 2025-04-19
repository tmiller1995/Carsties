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
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
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
        builder.Services.AddMassTransit(config =>
        {
            config.AddEntityFrameworkOutbox<AuctionDbContext>(options =>
            {
                options.QueryDelay = TimeSpan.FromSeconds(10);
                options.UsePostgres();
                options.UseBusOutbox();
            });

            config.AddConsumersFromNamespaceContaining<AuctionCreatedEventFaultConsumer>();
            config.AddConsumersFromNamespaceContaining<AuctionFinishedEventConsumer>();
            config.AddConsumersFromNamespaceContaining<BidPlacedEventConsumer>();

            config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: "auction"));

            config.UsingRabbitMq((context, configurator) =>
            {
                var configuration = context.GetRequiredService<IConfiguration>();
                configurator.Host(configuration.GetConnectionString("rabbitmq"));

                configurator.ConfigureEndpoints(context);
            });
        });

        builder.Services.AddScoped<IAuctionsRepository, AuctionsRepository>();

        return builder;
    }
}