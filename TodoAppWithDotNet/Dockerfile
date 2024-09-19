# Stage 1: Base image with runtime dependencies
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Stage 2: Build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file into the container (since you're already in the folder)
COPY ["TodoAppWithDotNet.csproj", "./"]

# Restore dependencies
RUN dotnet restore "./TodoAppWithDotNet.csproj"

# Copy the remaining source code
COPY . .

# Build the app
RUN dotnet build "./TodoAppWithDotNet.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish the app
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TodoAppWithDotNet.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final image with the published app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoAppWithDotNet.dll"]
