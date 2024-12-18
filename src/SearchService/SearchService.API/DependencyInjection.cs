using FastEndpoints;

namespace SearchService.API;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddPresentation(this IHostApplicationBuilder builder)
    {
        builder.Services.AddFastEndpoints();
        builder.Services.AddOpenApi();

        return builder;
    }
}