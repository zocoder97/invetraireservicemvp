using Inventory.Domain.Enums;

namespace Inventory.Application.DTOs;

/// <summary>
/// DTO pour l'entité Product
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Identifiant unique du produit
    /// </summary>
    public Guid ProductId { get; set; }
    
    /// <summary>
    /// Nom du produit
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Stock actuel du produit
    /// </summary>
    public int CurrentStock { get; set; }
    
    /// <summary>
    /// Stock optimal recommandé
    /// </summary>
    public int OptimalStock { get; set; }
    
    /// <summary>
    /// Point de commande (seuil de réapprovisionnement)
    /// </summary>
    public int ReorderPoint { get; set; }
    
    /// <summary>
    /// Tendance du stock
    /// </summary>
    public string Trend { get; set; } = string.Empty;
    
    /// <summary>
    /// Coût unitaire du produit
    /// </summary>
    public decimal Cost { get; set; }
}

