# Guide Multi-Tenant - Microservice d'Inventaire

## 🏢 Vue d'ensemble

Ce microservice supporte maintenant le **multi-tenancy** (multi-locataire), permettant à plusieurs entreprises/salons de beauté d'utiliser la même instance de l'API tout en gardant leurs données complètement isolées.

## 🔑 Principe de Fonctionnement

### Identification des Entreprises
Chaque entreprise/salon est identifié par un `EntrepriseId` unique (GUID). Toutes les données sont automatiquement filtrées par cet identifiant.

### Méthodes d'Authentification d'Entreprise

#### 1. Header HTTP (Recommandé)
```http
X-Entreprise-Id: 12345678-1234-1234-1234-123456789012
```

#### 2. JWT Claims (Pour l'authentification)
```json
{
  "entreprise_id": "12345678-1234-1234-1234-123456789012",
  "user_id": "user123",
  "role": "admin"
}
```

#### 3. Query Parameter (Pour les tests uniquement)
```http
GET /api/products?entrepriseId=12345678-1234-1234-1234-123456789012
```

## 📊 Entités Modifiées

Toutes les entités principales incluent maintenant `EntrepriseId` :

### Product
```csharp
public class Product
{
    public Guid ProductId { get; set; }
    [Required]
    public Guid EntrepriseId { get; set; } // 🆕 Nouveau champ
    public string Name { get; set; }
    // ... autres propriétés
}
```

### Supplier
```csharp
public class Supplier
{
    public Guid SupplierId { get; set; }
    [Required]
    public Guid EntrepriseId { get; set; } // 🆕 Nouveau champ
    public string Name { get; set; }
    // ... autres propriétés
}
```

### SmartAlert
```csharp
public class SmartAlert
{
    public Guid AlertId { get; set; }
    [Required]
    public Guid EntrepriseId { get; set; } // 🆕 Nouveau champ
    public AlertType Type { get; set; }
    // ... autres propriétés
}
```

## 🔧 Architecture Technique

### TenantRepository
Nouveau repository générique qui gère automatiquement le filtrage par entreprise :

```csharp
public interface ITenantRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllByEntrepriseAsync(Guid entrepriseId);
    Task<T?> GetByIdAndEntrepriseAsync(Guid id, Guid entrepriseId);
    Task<T> AddAsync(T entity, Guid entrepriseId);
    // ... autres méthodes
}
```

### TenantService
Service qui récupère automatiquement l'`EntrepriseId` depuis le contexte HTTP :

```csharp
public interface ITenantService
{
    Guid? GetCurrentEntrepriseId();
}
```

## 🚀 Utilisation des APIs

### Exemple avec curl

#### Créer un produit pour l'entreprise A
```bash
curl -X POST "http://localhost:5000/api/products" \
  -H "Content-Type: application/json" \
  -H "X-Entreprise-Id: 11111111-1111-1111-1111-111111111111" \
  -d '{
    "name": "Shampooing Premium",
    "currentStock": 20,
    "optimalStock": 30,
    "reorderPoint": 10,
    "trend": "Up",
    "cost": 15000
  }'
```

#### Récupérer les produits de l'entreprise A
```bash
curl -X GET "http://localhost:5000/api/products" \
  -H "X-Entreprise-Id: 11111111-1111-1111-1111-111111111111"
```

#### Récupérer les produits de l'entreprise B (données différentes)
```bash
curl -X GET "http://localhost:5000/api/products" \
  -H "X-Entreprise-Id: 22222222-2222-2222-2222-222222222222"
```

### Exemple avec JavaScript/Fetch

```javascript
// Configuration de l'entreprise
const ENTREPRISE_ID = '11111111-1111-1111-1111-111111111111';

// Fonction helper pour les requêtes
async function apiCall(endpoint, options = {}) {
  const defaultHeaders = {
    'Content-Type': 'application/json',
    'X-Entreprise-Id': ENTREPRISE_ID
  };
  
  return fetch(`http://localhost:5000/api${endpoint}`, {
    ...options,
    headers: {
      ...defaultHeaders,
      ...options.headers
    }
  });
}

// Récupérer les produits
const products = await apiCall('/products').then(r => r.json());

// Créer un produit
const newProduct = await apiCall('/products', {
  method: 'POST',
  body: JSON.stringify({
    name: 'Nouveau produit',
    currentStock: 15,
    optimalStock: 25,
    reorderPoint: 8,
    trend: 'Stable',
    cost: 12000
  })
}).then(r => r.json());
```

## 🔒 Sécurité et Isolation

### Isolation des Données
- ✅ **Lecture** : Seules les données de l'entreprise connectée sont retournées
- ✅ **Écriture** : Les nouvelles données sont automatiquement associées à l'entreprise
- ✅ **Modification** : Impossible de modifier les données d'une autre entreprise
- ✅ **Suppression** : Impossible de supprimer les données d'une autre entreprise

### Validation
```csharp
// Exemple de validation automatique dans le contrôleur
var entrepriseId = _tenantService.GetCurrentEntrepriseId();
if (entrepriseId == null)
    return BadRequest("EntrepriseId manquant. Veuillez fournir l'header 'X-Entreprise-Id'.");
```

## 📝 Documentation Swagger

L'interface Swagger a été mise à jour pour inclure le support de l'header `X-Entreprise-Id` :

1. Ouvrez http://localhost:5000
2. Cliquez sur "Authorize" 
3. Entrez votre `EntrepriseId` dans le champ "EntrepriseId"
4. Toutes vos requêtes incluront automatiquement l'header

## 🧪 Tests Multi-Tenant

### Scénario de Test Complet

```bash
# 1. Créer des produits pour l'entreprise A
curl -X POST "http://localhost:5000/api/products" \
  -H "Content-Type: application/json" \
  -H "X-Entreprise-Id: aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa" \
  -d '{"name": "Produit A1", "currentStock": 10, "optimalStock": 20, "reorderPoint": 5, "cost": 1000}'

# 2. Créer des produits pour l'entreprise B
curl -X POST "http://localhost:5000/api/products" \
  -H "Content-Type: application/json" \
  -H "X-Entreprise-Id: bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" \
  -d '{"name": "Produit B1", "currentStock": 15, "optimalStock": 25, "reorderPoint": 8, "cost": 1500}'

# 3. Vérifier l'isolation : l'entreprise A ne voit que ses produits
curl -X GET "http://localhost:5000/api/products" \
  -H "X-Entreprise-Id: aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"

# 4. Vérifier l'isolation : l'entreprise B ne voit que ses produits
curl -X GET "http://localhost:5000/api/products" \
  -H "X-Entreprise-Id: bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"
```

## 🔄 Migration des Données Existantes

Si vous avez des données existantes sans `EntrepriseId`, voici un script de migration :

```sql
-- Ajouter une entreprise par défaut pour les données existantes
UPDATE Products 
SET EntrepriseId = '00000000-0000-0000-0000-000000000000' 
WHERE EntrepriseId IS NULL;

UPDATE Suppliers 
SET EntrepriseId = '00000000-0000-0000-0000-000000000000' 
WHERE EntrepriseId IS NULL;

UPDATE SmartAlerts 
SET EntrepriseId = '00000000-0000-0000-0000-000000000000' 
WHERE EntrepriseId IS NULL;
```

## 🚨 Points d'Attention

### Obligatoire
- ⚠️ **Header requis** : Toutes les requêtes doivent inclure `X-Entreprise-Id`
- ⚠️ **Format GUID** : L'EntrepriseId doit être un GUID valide
- ⚠️ **Cohérence** : Utilisez toujours le même EntrepriseId pour une session

### Recommandations
- 🔐 **Authentification** : Implémentez JWT avec `entreprise_id` dans les claims
- 📊 **Monitoring** : Surveillez les tentatives d'accès cross-tenant
- 🔄 **Cache** : Considérez un cache par entreprise pour les performances
- 📝 **Logs** : Incluez l'EntrepriseId dans tous les logs

## 🔮 Évolutions Futures

### Fonctionnalités Prévues
- 🔐 **Authentification JWT** complète avec rôles par entreprise
- 📊 **Métriques** par entreprise (usage, performance)
- 🔄 **Sauvegarde** et restauration par entreprise
- 🌐 **Configuration** personnalisée par entreprise
- 📈 **Facturation** basée sur l'usage par entreprise

### Optimisations Possibles
- 🚀 **Index** de base de données sur EntrepriseId
- 💾 **Partitioning** des tables par entreprise
- 🔄 **Cache Redis** avec clés préfixées par entreprise
- 📊 **Monitoring** des performances par tenant



