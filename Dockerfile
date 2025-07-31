# Uygulama için base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5199

# Build için SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# backend klasörünü içeri kopyala
COPY Backend/ ./Backend/

# build klasörüne geç
WORKDIR /src/Backend

# projeyi restore et
RUN dotnet restore UserManagementApp.csproj

# publish işlemi
RUN dotnet publish UserManagementApp.csproj -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Uygulama çalıştırma
ENTRYPOINT ["dotnet", "UserManagementApp.dll"]
