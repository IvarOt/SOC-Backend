# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Set up build arguments globally
ARG BUILD_CONFIGURATION=Release
ARG ASPNETCORE_ENVIRONMENT=Testing

# Use the SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY SOC-backend/ ./SOC-backend/
RUN dotnet restore SOC-backend/SOC-backend.sln
RUN dotnet build SOC-backend/SOC-backend.sln \
    -c "$BUILD_CONFIGURATION" \
    --output /app/build \
    --no-restore \
    /p:ASPNETCORE_ENVIRONMENT="$ASPNETCORE_ENVIRONMENT"

# Publish the app
FROM build AS publish
RUN dotnet publish SOC-backend/SOC-backend.api/SOC-backend.api.csproj \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT \
    /p:UseAppHost=false

# Final stage - run the app in production
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Testing
ENTRYPOINT ["dotnet", "SOC-backend.api.dll"]
