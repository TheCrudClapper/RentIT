# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)

# specyfying base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app

# app listens on this ports
EXPOSE 9090
EXPOSE 9091


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UserService.API/UserService.API.csproj", "UserService.API/"]
COPY ["UserService.Core/UserService.Core.csproj", "UserService.Core/"]
COPY ["UserService.Infrastructure/UserService.Infrastructure.csproj", "UserService.Infrastructure/"]

# Restore nuget packages on
RUN dotnet restore "./UserService.API/./UserService.API.csproj"

# Copy from (first dot) - source to (second dot) - destination
COPY . .
WORKDIR "/src/UserService.API"

#Build application
RUN dotnet build "./UserService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV DB_USER=postgres
ENV DB_NAME=users
ENV DB_PORT=5432
ENV DB_HOST=localhost
ENV DB_PASSWORD=admin
ENTRYPOINT ["dotnet", "UserService.API.dll"]