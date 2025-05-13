using FastEndpoints;

namespace AuctionService.API;

public static class DependencyInjection
{
    public static TBuilder AddPresentation<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddFastEndpoints();
        builder.Services.AddOpenApi();

        return builder;
    }
}