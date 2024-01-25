#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/trucks/trucks.api/trucks.api.csproj", "services/trucks/trucks.api/"]
COPY ["services/trucks/trucks.application/trucks.application.csproj", "services/trucks/trucks.application/"]
COPY ["services/trucks/trucks.domain/trucks.domain.csproj", "services/trucks/trucks.domain/"]
RUN dotnet restore "./services/trucks/trucks.api/./trucks.api.csproj"
COPY . .
WORKDIR "/src/services/trucks/trucks.api"
RUN dotnet build "./trucks.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./trucks.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "trucks.api.dll"]