FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AuctionService/AuctionService.API/AuctionService.API.csproj", "src/AuctionService/AuctionService.API/"]
COPY ["src/AuctionService/Auction.Application/Auction.Application.csproj", "src/AuctionService/Auction.Application/"]
COPY ["src/AuctionService/Auction.Domain/Auction.Domain.csproj", "src/AuctionService/Auction.Domain/"]
COPY ["src/Carsties.Core/Carsties.Core.csproj", "src/Carsties.Core/"]
COPY ["src/AuctionService/Auction.Contract/Auction.Contract.csproj", "src/AuctionService/Auction.Contract/"]
COPY ["src/AuctionService/Auction.Infrastructure/Auction.Infrastructure.csproj", "src/AuctionService/Auction.Infrastructure/"]
RUN dotnet restore "src/AuctionService/AuctionService.API/AuctionService.API.csproj"
COPY . .
WORKDIR "/src/src/AuctionService/AuctionService.API"
RUN dotnet build "AuctionService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AuctionService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuctionService.API.dll"]
