using FastEndpoints;
using Search.Application;
using Search.Infrastructure;
using Search.Infrastructure.Data;
using SearchService.API;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation()
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // using var serviceScope = app.Services.CreateScope();
    // var seeder = serviceScope.ServiceProvider.GetRequiredService<DataSeeder>();
    // await seeder.SeedAsync();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();

app.Run();