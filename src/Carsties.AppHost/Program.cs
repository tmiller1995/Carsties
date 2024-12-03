using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache")
    .WithLifetime(ContainerLifetime.Persistent);
var rabbitMq = builder.AddRabbitMQ("rabbitmq")
    .WithLifetime(ContainerLifetime.Persistent);
var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);

var auctionDatabase = postgres.AddDatabase("auction-database");
var auctionApiService = builder.AddProject<Projects.AuctionService_API>("auction-service-api")
    .WithReference(auctionDatabase)
    .WaitFor(auctionDatabase)
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithReference(cache)
    .WaitFor(cache);

var frontend = builder.AddNpmApp("frontend", @"..\carsties-react-web")
    .WithReference(auctionApiService)
    .WaitFor(auctionApiService)
    .WithReference(cache)
    .WaitFor(cache)
    .WithHttpEndpoint(env: "development", port: 3_000)
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
