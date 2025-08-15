# Microservice d'Inventaire - Plateforme SaaS Salons de Beauté

Ce microservice gère l'inventaire pour une plateforme SaaS dédiée aux salons de beauté. Il est développé avec ASP.NET Core 8, utilise PostgreSQL comme base de données et suit une architecture Clean Code.

## 🏗️ Architecture

Le projet suit une architecture en couches (Clean Architecture) :

- **Inventory.API** : Couche de présentation (contrôleurs API REST)
- **Inventory.Application** : Couche application (services métier, DTOs, interfaces)
- **Inventory.Domain** : Couche domaine (entités, enums, interfaces de dépôt)
- **Inventory.Infrastructure** : Couche infrastructure (implémentations, Entity Framework, PostgreSQL)
- **Inventory.Tests** : Tests unitaires et d'intégration

## 🚀 Fonctionnalités

### Gestion des Produits
- CRUD complet des produits
- Gestion des stocks (actuel, optimal, point de commande)
- Suivi des tendances (hausse, baisse, critique)
- Identification des produits en stock critique
- Liste des produits à réapprovisionner

### Gestion des Fournisseurs
- CRUD complet des fournisseurs
- Scores de performance (fiabilité, prix, livraison, qualité)
- Classement des meilleurs fournisseurs

### Alertes Intelligentes
- Alertes par type (critique, avertissement, info, succès)
- Gestion des alertes non lues
- Marquage des alertes comme lues

### Analyses et IA
- Prédictions de demande
- Analyses de coûts par catégorie
- Analyses de prix du marché
- Données de saisonnalité
- Scores de performance IA

## 🛠️ Technologies Utilisées

- **Framework** : ASP.NET Core 8
- **Base de données** : PostgreSQL
- **ORM** : Entity Framework Core
- **Mapping** : AutoMapper
- **Documentation** : Swagger/OpenAPI
- **Architecture** : Clean Architecture
- **Conteneurisation** : Docker (optionnel)

## 📋 Prérequis

- .NET 8 SDK
- PostgreSQL 12+
- Visual Studio 2022 ou VS Code

## 🔧 Installation et Configuration

### 1. Cloner le projet
```bash
git clone <repository-url>
cd InventoryMicroservice
```

### 2. Configurer la base de données
Modifiez la chaîne de connexion dans `appsettings.json` :
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=InventoryDB;Username=votre_utilisateur;Password=votre_mot_de_passe"
  }
}
```

### 3. Installer les dépendances
```bash
dotnet restore
```

### 4. Créer la base de données
```bash
cd Inventory.API
dotnet ef database update
```

### 5. Lancer l'application
```bash
dotnet run
```

L'API sera accessible sur `http://localhost:5000` et la documentation Swagger sur `http://localhost:5000`

## 📚 Endpoints API

### Produits
- `GET /api/products` - Récupérer tous les produits
- `GET /api/products/{id}` - Récupérer un produit par ID
- `POST /api/products` - Créer un nouveau produit
- `PUT /api/products/{id}` - Mettre à jour un produit
- `DELETE /api/products/{id}` - Supprimer un produit
- `GET /api/products/critical-stock` - Produits en stock critique
- `GET /api/products/reorder` - Produits à réapprovisionner

### Fournisseurs
- `GET /api/suppliers` - Récupérer tous les fournisseurs
- `GET /api/suppliers/{id}` - Récupérer un fournisseur par ID
- `POST /api/suppliers` - Créer un nouveau fournisseur
- `PUT /api/suppliers/{id}` - Mettre à jour un fournisseur
- `DELETE /api/suppliers/{id}` - Supprimer un fournisseur
- `GET /api/suppliers/top` - Meilleurs fournisseurs

### Alertes
- `GET /api/alerts` - Récupérer toutes les alertes
- `GET /api/alerts/critical` - Alertes critiques
- `GET /api/alerts/type/{type}` - Alertes par type
- `GET /api/alerts/unread` - Alertes non lues
- `POST /api/alerts` - Créer une alerte
- `PUT /api/alerts/{id}/markAsRead` - Marquer comme lue
- `DELETE /api/alerts/{id}` - Supprimer une alerte

### Analyses
- `GET /api/analytics/demandpredictions` - Prédictions de demande
- `GET /api/analytics/costanalysis` - Analyses de coûts
- `GET /api/analytics/marketprices` - Analyses de prix du marché
- `GET /api/analytics/seasonality` - Données de saisonnalité
- `GET /api/analytics/performancescores` - Scores de performance IA

## 🔍 Santé de l'API
- `GET /health` - Endpoint de santé du service

## 🧪 Tests

Pour exécuter les tests :
```bash
dotnet test
```

## 📦 Déploiement

### Avec Docker
```bash
docker build -t inventory-microservice .
docker run -p 5000:5000 inventory-microservice
```

### Variables d'environnement
- `ConnectionStrings__DefaultConnection` : Chaîne de connexion PostgreSQL
- `ASPNETCORE_ENVIRONMENT` : Environnement (Development, Production)

## 🤝 Contribution

1. Fork le projet
2. Créer une branche feature (`git checkout -b feature/AmazingFeature`)
3. Commit les changements (`git commit -m 'Add some AmazingFeature'`)
4. Push vers la branche (`git push origin feature/AmazingFeature`)
5. Ouvrir une Pull Request

## 📄 Licence

Ce projet est sous licence MIT. Voir le fichier `LICENSE` pour plus de détails.

## 📞 Support

Pour toute question ou support, contactez l'équipe de développement à dev@beautyplatform.com

