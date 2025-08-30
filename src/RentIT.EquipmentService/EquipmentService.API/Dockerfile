# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EquipmentService.API/EquipmentService.API.csproj", "EquipmentService.API/"]
COPY ["EquipmentService.Core/EquipmentService.Core.csproj", "EquipmentService.Core/"]
COPY ["EquipmentService.Infrastructure/EquipmentService.Infrastructure.csproj", "EquipmentService.Infrastructure/"]
RUN dotnet restore "./EquipmentService.API/EquipmentService.API.csproj"
COPY . .
WORKDIR "/src/EquipmentService.API"
RUN dotnet build "./EquipmentService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EquipmentService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV DB_USER=postgres
ENV DB_NAME=equipment
ENV DB_PORT=5432
ENV DB_HOST=localhost
ENV DB_PASSWORD=admin
ENTRYPOINT ["dotnet", "EquipmentService.API.dll"]