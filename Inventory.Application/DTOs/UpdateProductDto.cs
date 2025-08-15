using System.ComponentModel.DataAnnotations;
using Inventory.Domain.Enums;

namespace Inventory.Application.DTOs;

/// <summary>
/// DTO pour la mise à jour d'un produit
/// </summary>
public class UpdateProductDto
{
    /// <summary>
    /// Nom du produit (optionnel)
    /// </summary>
    [StringLength(200, ErrorMessage = "Le nom ne peut pas dépasser 200 caractères")]
    public string? Name { get; set; }
    
    /// <summary>
    /// Stock actuel du produit (optionnel)
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Le stock actuel doit être positif")]
    public int? CurrentStock { get; set; }
    
    /// <summary>
    /// Stock optimal recommandé (optionnel)
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Le stock optimal doit être positif")]
    public int? OptimalStock { get; set; }
    
    /// <summary>
    /// Point de commande (optionnel)
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Le point de commande doit être positif")]
    public int? ReorderPoint { get; set; }
    
    /// <summary>
    /// Tendance du stock (optionnel)
    /// </summary>
    public TrendType? Trend { get; set; }
    
    /// <summary>
    /// Coût unitaire du produit (optionnel)
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "Le coût doit être positif")]
    public decimal? Cost { get; set; }
}

