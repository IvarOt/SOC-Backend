FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base 
WORKDIR /app 
EXPOSE 8080 
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ARG BUILD_CONFIGURATION=RELEASE
ARG ASPNETCORE_ENVIRONMENT=Testing

COPY SOC-backend/ ./SOC-backend/
RUN dotnet restore SOC-backend/SOC-backend.sln
RUN ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT dotnet build SOC-backend/SOC-backend.sln -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG ASPNETCORE_ENVIRONMENT=Testing
RUN dotnet publish SOC-backend/SOC-backend.api/SOC-backend.api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production
FROM base AS final
ARG ASPNETCORE_ENVIRONMENT=Testing
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
ENTRYPOINT ["dotnet", "SOC-backend.api.dll"]