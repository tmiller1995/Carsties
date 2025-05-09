using Microsoft.Extensions.Configuration;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

var postgres = builder.AddPostgres("postgres")
    .WithImageTag("latest")
    .WithPgAdmin(configureContainer: config => config.WithImageTag("latest"))
    .WithLifetime(ContainerLifetime.Persistent);
if (builder.ExecutionContext.IsRunMode)
    postgres.WithDataVolume();

var auctionDb = postgres.AddDatabase("auction-db");
var identityDb = postgres.AddDatabase("identity-db");

var redis = builder.AddRedis("redis")
    .WithImageTag("latest")
    .WithDataVolume()
    .WithRedisCommander();

var rabbitMq = builder.AddRabbitMQ("rabbitmq")
    .WithImageTag("latest")
    .WithLifetime(ContainerLifetime.Persistent);

var ravenDb = builder.AddRavenDB("ravendb")
    .WithImageTag("latest")
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

builder.AddNpmApp("fontend", @"..\react-frontend", "dev")
    .WithReference(gatewayService)
    .WaitFor(gatewayService)
    .WithEndpoint(targetPort: 3000, env: "PORT")
    .WithExternalHttpEndpoints()
    .WithEnvironment("GATEWAY_BASE_URL", gatewayService.GetEndpoint("http"))
    .WaitFor(gatewayService)
    .WithOtlpExporter();

// builder.AddDockerfile("frontend", @"..\react-frontend",@"..\react-frontend\Dockerfile")
//     .WithReference(gatewayService)
//     .WaitFor(gatewayService)
//     .WithExternalHttpEndpoints()
//     .WithEndpoint(port: 3000, targetPort: 3000, name: "http")
//     .WithBuildArg("FONTAWESOME_PACKAGE_TOKEN", builder.Configuration["FONTAWESOME_PACKAGE_TOKEN"]);

builder.Build().Run();