# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies from backend folder
COPY backend/*.csproj ./
RUN dotnet restore

# Copy everything else from backend folder and build
COPY backend/. ./
RUN dotnet publish -c Release -o out

# Use the runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Expose port (Render will set PORT env variable)
EXPOSE 5000

# Run the application
# Render sets PORT env variable, we use it in Program.cs
ENTRYPOINT ["dotnet", "backend.dll"]
