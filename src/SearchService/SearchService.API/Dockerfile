FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SearchService/SearchService.API/SearchService.API.csproj", "src/SearchService/SearchService.API/"]
COPY ["src/SearchService/Search.Application/Search.Application.csproj", "src/SearchService/Search.Application/"]
COPY ["src/SearchService/Search.Domain/Search.Domain.csproj", "src/SearchService/Search.Domain/"]
COPY ["src/Carsties.Core/Carsties.Core.csproj", "src/Carsties.Core/"]
COPY ["src/SearchService/Search.Contract/Search.Contract.csproj", "src/SearchService/Search.Contract/"]
COPY ["src/SearchService/Search.Infrastructure/Search.Infrastructure.csproj", "src/SearchService/Search.Infrastructure/"]
RUN dotnet restore "src/SearchService/SearchService.API/SearchService.API.csproj"
COPY . .
WORKDIR "/src/src/SearchService/SearchService.API"
RUN dotnet build "SearchService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SearchService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SearchService.API.dll"]
