# Build context is the repo root (Render's default for a manual Web Service),
# so all paths below are relative to the repo root, not this folder.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY BG/MyBackendAPI/*.csproj ./BG/MyBackendAPI/
RUN dotnet restore ./BG/MyBackendAPI/MyBackendAPI.csproj
COPY BG/MyBackendAPI/. ./BG/MyBackendAPI/
RUN dotnet publish ./BG/MyBackendAPI/MyBackendAPI.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MyBackendAPI.dll"]
