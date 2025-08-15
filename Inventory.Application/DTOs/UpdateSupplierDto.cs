using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.DTOs;

/// <summary>
/// DTO pour la mise à jour d'un fournisseur
/// </summary>
public class UpdateSupplierDto
{
    /// <summary>
    /// Nom du fournisseur (optionnel)
    /// </summary>
    [StringLength(200, ErrorMessage = "Le nom ne peut pas dépasser 200 caractères")]
    public string? Name { get; set; }
    
    /// <summary>
    /// Score de fiabilité (optionnel, 0-100)
    /// </summary>
    [Range(0, 100, ErrorMessage = "Le score de fiabilité doit être entre 0 et 100")]
    public int? Reliability { get; set; }
    
    /// <summary>
    /// Score de prix (optionnel, 0-100)
    /// </summary>
    [Range(0, 100, ErrorMessage = "Le score de prix doit être entre 0 et 100")]
    public int? PriceScore { get; set; }
    
    /// <summary>
    /// Score de livraison (optionnel, 0-100)
    /// </summary>
    [Range(0, 100, ErrorMessage = "Le score de livraison doit être entre 0 et 100")]
    public int? DeliveryScore { get; set; }
    
    /// <summary>
    /// Score de qualité (optionnel, 0-100)
    /// </summary>
    [Range(0, 100, ErrorMessage = "Le score de qualité doit être entre 0 et 100")]
    public int? QualityScore { get; set; }
    
    /// <summary>
    /// Score global du fournisseur (optionnel, 0-100)
    /// </summary>
    [Range(0, 100, ErrorMessage = "Le score global doit être entre 0 et 100")]
    public int? OverallScore { get; set; }
}

