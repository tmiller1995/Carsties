using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Raven.DependencyInjection;
using Search.Application.Interfaces;
using Search.Infrastructure.Auctions;

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

        builder.Services.AddScoped<ISearchRepository, SearchRepository>();

        return builder;
    }
}