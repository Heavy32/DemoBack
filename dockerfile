# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy solution file and main project files
COPY ["DemoBack.sln", "./"]
COPY ["DemoBack/FlowCycle.Api.csproj", "DemoBack/"]
COPY ["FlowCycle.Domain/FlowCycle.Domain.csproj", "FlowCycle.Domain/"]
COPY ["FlowCycle.Persistance/FlowCycle.Persistance.csproj", "FlowCycle.Persistance/"]

# Restore dependencies
RUN dotnet restore

# Copy everything else
COPY . .

# Build and publish
RUN dotnet publish "DemoBack/FlowCycle.Api.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

# Install required tools and packages
RUN apt-get update && \
    apt-get install -y --no-install-recommends icu-devtools && \
    rm -rf /var/lib/apt/lists/* && \
    dotnet tool install --global dotnet-ef

# Add dotnet tools to PATH
ENV PATH="$PATH:/root/.dotnet/tools"

# Copy the source and published files
COPY --from=build /source /app/src
COPY --from=build /app/publish /app/publish

# Environment configuration
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Expose ports
EXPOSE 80