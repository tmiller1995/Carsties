using FastEndpoints;
using Search.Application;
using Search.Infrastructure;
using SearchService.API;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    builder.AddPresentation()
        .AddApplication()
        .AddInfrastructure();

    var app = builder.Build();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseSerilogRequestLogging();
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

