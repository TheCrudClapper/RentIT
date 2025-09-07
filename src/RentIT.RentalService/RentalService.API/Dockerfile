# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 7090
EXPOSE 7091


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RentalService.API/RentalService.API.csproj", "RentalService.API/"]
COPY ["RentalService.Core/RentalService.Core.csproj", "RentalService.Core/"]
COPY ["RentalService.Infrastructure/RentalService.Infrastructure.csproj", "RentalService.Infrastructure/"]

RUN dotnet restore "./RentalService.API/RentalService.API.csproj"
COPY . .

WORKDIR "/src/RentalService.API"
RUN dotnet build "./RentalService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RentalService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV DB_USER=postgres
ENV DB_NAME=rentals
ENV USERS_MICROSERVICE_PORT=8080
ENV USERS_MICROSERVICE_NAME=users-microservice
ENV EQUIPMENT_MICROSERVICE_NAME=equipment-microservice
ENV EQUIPMENT_MICROSERVICE_PORT=8080
ENV DB_PORT=5432
ENV DB_HOST=localhost
ENV DB_PASSWORD=admin
ENTRYPOINT ["dotnet", "RentalService.API.dll"]