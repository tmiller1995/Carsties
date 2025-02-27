using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var rabbitMq = builder.AddRabbitMQ("rabbitmq");
var ravenDb = builder.AddRavenDB("ravendb");

var auctionServiceApi = builder.AddProject<AuctionService_API>("auction-service")
    .WithReference(postgres)
    .WaitFor(postgres)
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq);

var searchServiceApi = builder.AddProject<SearchService_API>("search-service")
    .WithReference(ravenDb)
    .WaitFor(ravenDb)
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq);

var identityService = builder.AddProject<IdentityService>("identity-service")
    .WithReference(postgres)
    .WaitFor(postgres);

var gatewayService = builder.AddProject<GatewayService>("gateway-service")
    .WithReference(identityService)
    .WaitFor(identityService)
    .WithReference(auctionServiceApi)
    .WaitFor(auctionServiceApi)
    .WithReference(searchServiceApi)
    .WaitFor(searchServiceApi);

builder.AddNpmApp("frontend", @"..\react-frontend")
    .WithReference(gatewayService)
    .WaitFor(gatewayService)
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();