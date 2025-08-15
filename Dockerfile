# Utiliser l'image de base .NET 8 SDK pour la construction
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier les fichiers de projet et restaurer les dépendances
COPY ["Inventory.API/Inventory.API.csproj", "Inventory.API/"]
COPY ["Inventory.Application/Inventory.Application.csproj", "Inventory.Application/"]
COPY ["Inventory.Domain/Inventory.Domain.csproj", "Inventory.Domain/"]
COPY ["Inventory.Infrastructure/Inventory.Infrastructure.csproj", "Inventory.Infrastructure/"]

RUN dotnet restore "Inventory.API/Inventory.API.csproj"

# Copier tout le code source
COPY . .

# Construire l'application
WORKDIR "/src/Inventory.API"
RUN dotnet build "Inventory.API.csproj" -c Release -o /app/build

# Publier l'application
FROM build AS publish
RUN dotnet publish "Inventory.API.csproj" -c Release -o /app/publish

# Utiliser l'image de runtime .NET 8 pour l'exécution
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copier les fichiers publiés
COPY --from=publish /app/publish .

# Exposer le port 5000
EXPOSE 5000

# Variables d'environnement
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

# Point d'entrée
ENTRYPOINT ["dotnet", "Inventory.API.dll"]

