using Microsoft.Extensions.Configuration;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);
if (builder.ExecutionContext.IsRunMode)
    postgres.WithDataVolume();

var auctionDb = postgres.AddDatabase("auction-db");
var identityDb = postgres.AddDatabase("identity-db");

var redis = builder.AddRedis("redis")
    .WithDataVolume()
    .WithRedisCommander();

var rabbitMq = builder.AddRabbitMQ("rabbitmq")
    .WithLifetime(ContainerLifetime.Persistent);

var ravenDb = builder.AddRavenDB("ravendb")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume();

var auctionSearchDb = ravenDb.AddDatabase("auction-search-db");

var auctionServiceApi = builder.AddProject<AuctionService_API>("auction-service")
    .WithReference(auctionDb)
    .WaitFor(auctionDb)
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq);

var searchServiceApi = builder.AddProject<SearchService_API>("search-service")
    .WithReference(auctionSearchDb)
    .WaitFor(auctionSearchDb)
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq);

var identityService = builder.AddProject<IdentityService>("identity-service")
    .WithReference(identityDb)
    .WaitFor(identityDb);

var gatewayService = builder.AddProject<GatewayService>("gateway-service")
    .WithReference(identityService)
    .WaitFor(identityService)
    .WithReference(auctionServiceApi)
    .WaitFor(auctionServiceApi)
    .WithReference(searchServiceApi)
    .WaitFor(searchServiceApi);

builder.AddDockerfile("frontend", @"..\react-frontend",@"..\react-frontend\Dockerfile")
    .WithReference(gatewayService)
    .WaitFor(gatewayService)
    .WithExternalHttpEndpoints()
    .WithEndpoint(port: 3000, targetPort: 3000, name: "http")
    .WithBuildArg("FONTAWESOME_PACKAGE_TOKEN", builder.Configuration["FONTAWESOME_PACKAGE_TOKEN"]);

builder.Build().Run();