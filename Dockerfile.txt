# 1. Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Proje dosyasını kopyala ve restore et
COPY UserManagementApp.csproj ./
RUN dotnet restore

# Diğer tüm dosyaları kopyala ve yayınla (publish)
COPY . ./
RUN dotnet publish -c Release -o /app/out

# 2. Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

# Uygulamanın çalıştırılması
ENTRYPOINT ["dotnet", "UserManagementApp.dll"]
