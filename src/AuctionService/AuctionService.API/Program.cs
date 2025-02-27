using Auction.Application;
using Auction.Application.Interfaces;
using Auction.Infrastructure;
using Auction.Infrastructure.Data;
using Auction.Infrastructure.Middleware;
using AuctionService.API;
using Carsties.ServiceDefaults;
using FastEndpoints;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(
            outputTemplate:
            "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    builder.AddServiceDefaults()
        .AddDefaultHealthChecks()
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure();

    var app = builder.Build();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();

        using var serviceScope = app.Services.CreateScope();
        var auctionDbContext = serviceScope.ServiceProvider.GetRequiredService<AuctionDbContext>();
        if (!auctionDbContext.Auctions.Any())
        {
            Log.Information("Seeding database");
            var auctionsRepository = serviceScope.ServiceProvider.GetRequiredService<IAuctionsRepository>();
            var auctionsToSeed = SeedData.GenerateAuctions();
            foreach (var auctionBatch in auctionsToSeed.Chunk(5_000))
            {
                await auctionsRepository.CreateAuctionsAsync(auctionBatch);
            }

            Log.Information("Seeding complete");
        }
    }

    app.UseSerilogRequestLogging();
    app.UseMiddleware<EventualConsistencyMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseFastEndpoints();

    app.Run();
}
catch (Exception e) when (e is not HostAbortedException)
{
    Log.Fatal(e, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}