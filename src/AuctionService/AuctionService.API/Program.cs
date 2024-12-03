using Auction.Application;
using Auction.Infrastructure;
using Auction.Infrastructure.Data;
using Auction.Infrastructure.Middleware;
using AuctionService.API;
using Carsties.ServiceDefaults;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddPresentation()
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

app.MapDefaultEndpoints();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<EventualConsistencyMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();

app.Run();
