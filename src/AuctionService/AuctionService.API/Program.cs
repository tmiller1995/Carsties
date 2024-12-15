using Auction.Application;
using Auction.Infrastructure;
using Auction.Infrastructure.Data;
using Auction.Infrastructure.Middleware;
using AuctionService.API;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddPresentation()
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
        var auctionsToSeed = SeedData.GenerateAuctions();
        auctionDbContext.Auctions.AddRange(auctionsToSeed);
        await auctionDbContext.SaveChangesAsync();
    }
}

app.UseMiddleware<EventualConsistencyMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();

app.Run();
