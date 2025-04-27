# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file and main project files
COPY DemoBack.sln .
COPY DemoBack/FlowCycle.Api.csproj ./DemoBack/
COPY FlowCycle.Domain/FlowCycle.Domain.csproj ./FlowCycle.Domain/
COPY FlowCycle.Persistance/FlowCycle.Persistance.csproj ./FlowCycle.Persistance/

# Restore dependencies
RUN dotnet restore "DemoBack/FlowCycle.Api.csproj"

# Copy everything else
COPY . .

# Build and publish
WORKDIR "/src/DemoBack"
RUN dotnet publish "FlowCycle.Api.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Install ICU for localization (needed for your many language resources)
RUN apt-get update && \
    apt-get install -y --no-install-recommends icu-devtools && \
    rm -rf /var/lib/apt/lists/*

# Copy from build stage
COPY --from=build /app/publish .

# Environment configuration
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Expose ports
EXPOSE 80
EXPOSE 443

# Health check
HEALTHCHECK --interval=30s --timeout=3s \
    CMD curl -f http://localhost/health || exit 1

ENTRYPOINT ["dotnet", "FlowCycle.Api.dll"]