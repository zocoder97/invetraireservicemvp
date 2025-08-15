using Inventory.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Domain.Entities;

/// <summary>
/// Entité représentant un produit dans l'inventaire
/// </summary>
public class Product
{
    /// <summary>
    /// Identifiant unique du produit
    /// </summary>
    public Guid ProductId { get; set; }
    
    /// <summary>
    /// Identifiant de l'entreprise/salon propriétaire
    /// </summary>
    [Required]
    public Guid EntrepriseId { get; set; }
    
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
    public TrendType Trend { get; set; }
    
    /// <summary>
    /// Coût unitaire du produit
    /// </summary>
    public decimal Cost { get; set; }
    
    /// <summary>
    /// Date de création
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Date de dernière mise à jour
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}

