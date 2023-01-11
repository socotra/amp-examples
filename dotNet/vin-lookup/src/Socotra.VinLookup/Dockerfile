# Dockerfile
ARG arch=arm64
FROM --platform=linux/${arch} mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

# Copy csproj and restore as distinct layers
FROM --platform=linux/${arch} mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
WORKDIR "/app"
RUN dotnet build "Socotra.VinLookup.csproj" -c Release -o /app/build

# Build runtime image
FROM --platform=linux/${arch} build AS publish
RUN dotnet publish "Socotra.VinLookup.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM --platform=linux/${arch} base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Socotra.VinLookup.dll"]
