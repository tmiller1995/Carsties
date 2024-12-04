var builder = DistributedApplication.CreateBuilder(args);
var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);

var auctionDatabase = postgres.AddDatabase("auction-database");

var auctionApiService = builder.AddProject<Projects.AuctionService_API>("auction-service-api")
    .WithReference(auctionDatabase).WaitFor(auctionDatabase);

var frontend = builder.AddNpmApp("frontend", "../carsties-react-web")
    .WithReference(auctionApiService).WaitFor(auctionApiService)
    .WithHttpEndpoint(port: 3_000)
    .PublishAsDockerFile();

builder.Build().Run();
