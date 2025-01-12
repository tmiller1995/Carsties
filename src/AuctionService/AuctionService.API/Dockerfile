# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["src/AuctionService/AuctionService.API/AuctionService.API.csproj", "src/AuctionService/AuctionService.API/"]
COPY ["src/AuctionService/Auction.Application/Auction.Application.csproj", "src/AuctionService/Auction.Application/"]
COPY ["src/AuctionService/Auction.Domain/Auction.Domain.csproj", "src/AuctionService/Auction.Domain/"]
COPY ["src/Carsties.Core/Carsties.Core.csproj", "src/Carsties.Core/"]
COPY ["src/AuctionService/Auction.Contract/Auction.Contract.csproj", "src/AuctionService/Auction.Contract/"]
COPY ["src/AuctionService/Auction.Infrastructure/Auction.Infrastructure.csproj", "src/AuctionService/Auction.Infrastructure/"]
RUN dotnet restore "./src/AuctionService/AuctionService.API/AuctionService.API.csproj"
COPY . .
WORKDIR "/src/src/AuctionService/AuctionService.API"
RUN dotnet build "./AuctionService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AuctionService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuctionService.API.dll"]