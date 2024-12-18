using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Raven.DependencyInjection;

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

        return builder;
    }
}