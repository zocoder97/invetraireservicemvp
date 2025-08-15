using System.ComponentModel.DataAnnotations;
using Inventory.Domain.Enums;

namespace Inventory.Application.DTOs;

/// <summary>
/// DTO pour la création d'un produit
/// </summary>
public class CreateProductDto
{
    /// <summary>
    /// Nom du produit
    /// </summary>
    [Required(ErrorMessage = "Le nom du produit est obligatoire")]
    [StringLength(200, ErrorMessage = "Le nom ne peut pas dépasser 200 caractères")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Stock actuel du produit
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Le stock actuel doit être positif")]
    public int CurrentStock { get; set; }
    
    /// <summary>
    /// Stock optimal recommandé
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Le stock optimal doit être positif")]
    public int OptimalStock { get; set; }
    
    /// <summary>
    /// Point de commande (seuil de réapprovisionnement)
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Le point de commande doit être positif")]
    public int ReorderPoint { get; set; }
    
    /// <summary>
    /// Tendance du stock
    /// </summary>
    public TrendType Trend { get; set; } = TrendType.Up;
    
    /// <summary>
    /// Coût unitaire du produit
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "Le coût doit être positif")]
    public decimal Cost { get; set; }
}

