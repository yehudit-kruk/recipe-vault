# syntax=docker/dockerfile:1

# ===== Build stage: full SDK, used only to compile =====
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Restore first, so the dependency layer is cached until a .csproj changes
COPY src/RecipeVault.Api/RecipeVault.Api.csproj src/RecipeVault.Api/
RUN dotnet restore src/RecipeVault.Api/RecipeVault.Api.csproj

COPY src/ src/
RUN dotnet publish src/RecipeVault.Api/RecipeVault.Api.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

# ===== Runtime stage: ASP.NET runtime only, no SDK =====
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Built-in non-root user of the official .NET images
USER $APP_UID

# The official images listen on port 8080 (ASPNETCORE_HTTP_PORTS)
EXPOSE 8080
ENTRYPOINT ["dotnet", "RecipeVault.Api.dll"]
