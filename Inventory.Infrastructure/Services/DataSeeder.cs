using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Infrastructure.Persistence;

namespace Inventory.Infrastructure.Services;

/// <summary>
/// Service pour initialiser la base de données avec des données de test
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Initialise la base de données avec des données de test
    /// </summary>
    /// <param name="context">Contexte de base de données</param>
    public static async Task SeedDataAsync(AppDbContext context)
    {
        // Vérifier si des données existent déjà
        if (context.Products.Any())
            return;

        // Données de test pour les produits
        var products = new List<Product>
        {
            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Shampooing Pro",
                CurrentStock = 15,
                OptimalStock = 25,
                ReorderPoint = 8,
                Trend = TrendType.Up,
                Cost = 12000,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Crème hydratante",
                CurrentStock = 8,
                OptimalStock = 12,
                ReorderPoint = 5,
                Trend = TrendType.Up,
                Cost = 8500,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Vernis à ongles",
                CurrentStock = 45,
                OptimalStock = 35,
                ReorderPoint = 15,
                Trend = TrendType.Down,
                Cost = 3500,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Produits coiffage",
                CurrentStock = 22,
                OptimalStock = 28,
                ReorderPoint = 12,
                Trend = TrendType.Up,
                Cost = 15000,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Masque facial",
                CurrentStock = 6,
                OptimalStock = 18,
                ReorderPoint = 6,
                Trend = TrendType.Critical,
                Cost = 25000,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        // Données de test pour les fournisseurs
        var suppliers = new List<Supplier>
        {
            new Supplier
            {
                SupplierId = Guid.NewGuid(),
                Name = "Beauty Supply Pro",
                Reliability = 95,
                PriceScore = 78,
                DeliveryScore = 92,
                QualityScore = 88,
                OverallScore = 88,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Supplier
            {
                SupplierId = Guid.NewGuid(),
                Name = "Cosmo Distributeur",
                Reliability = 88,
                PriceScore = 85,
                DeliveryScore = 85,
                QualityScore = 90,
                OverallScore = 87,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Supplier
            {
                SupplierId = Guid.NewGuid(),
                Name = "Madagascar Beauty",
                Reliability = 82,
                PriceScore = 92,
                DeliveryScore = 78,
                QualityScore = 85,
                OverallScore = 84,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        // Données de test pour les alertes
        var alerts = new List<SmartAlert>
        {
            new SmartAlert
            {
                AlertId = Guid.NewGuid(),
                Type = AlertType.Critical,
                Message = "3 produits en rupture critique",
                Count = 3,
                CreatedAt = DateTime.UtcNow.AddHours(-2),
                IsRead = false
            },
            new SmartAlert
            {
                AlertId = Guid.NewGuid(),
                Type = AlertType.Warning,
                Message = "Livraison retardée Beauty Supply Pro",
                Count = 1,
                CreatedAt = DateTime.UtcNow.AddHours(-4),
                IsRead = false
            },
            new SmartAlert
            {
                AlertId = Guid.NewGuid(),
                Type = AlertType.Info,
                Message = "Nouveau prix avantageux détecté",
                Count = 2,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                IsRead = false
            }
        };

        // Ajout des données à la base
        await context.Products.AddRangeAsync(products);
        await context.Suppliers.AddRangeAsync(suppliers);
        await context.SmartAlerts.AddRangeAsync(alerts);

        await context.SaveChangesAsync();
    }
}

