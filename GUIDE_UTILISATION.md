# Guide d'Utilisation - Microservice d'Inventaire

## 🚀 Démarrage Rapide

### Option 1: Exécution Locale (Recommandée pour le développement)

1. **Prérequis**
   - .NET 8 SDK installé
   - PostgreSQL 12+ installé et en cours d'exécution

2. **Configuration de la base de données**
   ```bash
   # Créer la base de données
   createdb -U postgres InventoryDB_Dev
   ```

3. **Lancement de l'application**
   ```bash
   cd Inventory.API
   dotnet run
   ```

4. **Accès à l'API**
   - API: http://localhost:5000
   - Documentation Swagger: http://localhost:5000
   - Endpoint de santé: http://localhost:5000/health

### Option 2: Avec Docker Compose (Recommandée pour la production)

1. **Lancement complet avec base de données**
   ```bash
   docker-compose up -d
   ```

2. **Accès aux services**
   - API: http://localhost:5000
   - pgAdmin: http://localhost:8080 (admin@inventory.com / admin)

## 📋 Exemples d'Utilisation des API

### 1. Gestion des Produits

#### Créer un nouveau produit
```bash
curl -X POST "http://localhost:5000/api/products" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Shampooing Premium",
    "currentStock": 20,
    "optimalStock": 30,
    "reorderPoint": 10,
    "trend": "Up",
    "cost": 15000
  }'
```

#### Récupérer tous les produits
```bash
curl -X GET "http://localhost:5000/api/products"
```

#### Récupérer les produits en stock critique
```bash
curl -X GET "http://localhost:5000/api/products/critical-stock"
```

#### Mettre à jour un produit
```bash
curl -X PUT "http://localhost:5000/api/products/{id}" \
  -H "Content-Type: application/json" \
  -d '{
    "currentStock": 25,
    "trend": "Up"
  }'
```

### 2. Gestion des Fournisseurs

#### Créer un nouveau fournisseur
```bash
curl -X POST "http://localhost:5000/api/suppliers" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Beauty Supplies Madagascar",
    "reliability": 90,
    "priceScore": 85,
    "deliveryScore": 88,
    "qualityScore": 92,
    "overallScore": 89
  }'
```

#### Récupérer les meilleurs fournisseurs
```bash
curl -X GET "http://localhost:5000/api/suppliers/top?count=5"
```

### 3. Gestion des Alertes

#### Créer une nouvelle alerte
```bash
curl -X POST "http://localhost:5000/api/alerts" \
  -H "Content-Type: application/json" \
  -d '{
    "type": "Critical",
    "message": "Stock épuisé pour le produit X",
    "count": 1
  }'
```

#### Récupérer les alertes critiques
```bash
curl -X GET "http://localhost:5000/api/alerts/critical"
```

#### Marquer une alerte comme lue
```bash
curl -X PUT "http://localhost:5000/api/alerts/{id}/markAsRead"
```

### 4. Analyses et Données IA

#### Récupérer les prédictions de demande
```bash
curl -X GET "http://localhost:5000/api/analytics/demandpredictions"
```

#### Récupérer les analyses de coûts
```bash
curl -X GET "http://localhost:5000/api/analytics/costanalysis"
```

#### Récupérer les scores de performance IA
```bash
curl -X GET "http://localhost:5000/api/analytics/performancescores"
```

## 🔧 Configuration Avancée

### Variables d'Environnement

| Variable | Description | Valeur par défaut |
|----------|-------------|-------------------|
| `ConnectionStrings__DefaultConnection` | Chaîne de connexion PostgreSQL | `Host=localhost;Database=InventoryDB;Username=postgres;Password=password` |
| `ASPNETCORE_ENVIRONMENT` | Environnement d'exécution | `Development` |
| `ASPNETCORE_URLS` | URLs d'écoute | `http://+:5000` |

### Configuration de la Base de Données

#### Chaîne de connexion PostgreSQL
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=votre_host;Port=5432;Database=votre_db;Username=votre_user;Password=votre_password;SSL Mode=Require;"
  }
}
```

#### Migration de la base de données
```bash
# Créer une nouvelle migration
dotnet ef migrations add NomDeLaMigration --project Inventory.Infrastructure --startup-project Inventory.API

# Appliquer les migrations
dotnet ef database update --project Inventory.Infrastructure --startup-project Inventory.API
```

## 🧪 Tests et Validation

### Tests Unitaires
```bash
dotnet test
```

### Test de l'API avec curl
```bash
# Test de santé
curl http://localhost:5000/health

# Test d'authentification (si implémentée)
curl -H "Authorization: Bearer your_token" http://localhost:5000/api/products
```

### Test de Performance
```bash
# Utiliser Apache Bench pour tester la performance
ab -n 1000 -c 10 http://localhost:5000/api/products
```

## 📊 Monitoring et Logs

### Logs de l'Application
Les logs sont configurés pour s'afficher dans la console et peuvent être redirigés vers des fichiers :

```bash
# Voir les logs en temps réel
docker-compose logs -f inventory_api
```

### Métriques de Santé
L'endpoint `/health` fournit des informations sur l'état du service :

```json
{
  "status": "healthy",
  "timestamp": "2024-01-15T10:30:00Z",
  "service": "Inventory Microservice"
}
```

## 🔒 Sécurité

### CORS
Le service est configuré pour accepter les requêtes de toutes les origines en développement. Pour la production, modifiez la configuration CORS dans `Program.cs`.

### Authentification (À implémenter)
Pour ajouter l'authentification JWT :

1. Installer le package `Microsoft.AspNetCore.Authentication.JwtBearer`
2. Configurer JWT dans `Program.cs`
3. Ajouter `[Authorize]` aux contrôleurs

## 🚀 Déploiement en Production

### Avec Docker
```bash
# Build de l'image
docker build -t inventory-microservice:latest .

# Déploiement
docker run -d \
  --name inventory-api \
  -p 5000:5000 \
  -e ConnectionStrings__DefaultConnection="Host=prod_host;Database=InventoryDB;Username=prod_user;Password=prod_password" \
  -e ASPNETCORE_ENVIRONMENT=Production \
  inventory-microservice:latest
```

### Avec Kubernetes
Créer les fichiers de déploiement Kubernetes pour une orchestration complète.

## 🐛 Dépannage

### Problèmes Courants

1. **Erreur de connexion à la base de données**
   - Vérifier que PostgreSQL est en cours d'exécution
   - Vérifier la chaîne de connexion
   - Vérifier les permissions utilisateur

2. **Port déjà utilisé**
   ```bash
   # Trouver le processus utilisant le port 5000
   lsof -i :5000
   # Tuer le processus
   kill -9 PID
   ```

3. **Problèmes de migration**
   ```bash
   # Supprimer et recréer la base de données
   dotnet ef database drop --project Inventory.Infrastructure --startup-project Inventory.API
   dotnet ef database update --project Inventory.Infrastructure --startup-project Inventory.API
   ```

### Logs de Debug
Pour activer les logs détaillés, modifier `appsettings.Development.json` :

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```
