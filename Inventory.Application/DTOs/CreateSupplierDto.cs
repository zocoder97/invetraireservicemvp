using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.DTOs;

/// <summary>
/// DTO pour la création d'un fournisseur
/// </summary>
public class CreateSupplierDto
{
    /// <summary>
    /// Nom du fournisseur
    /// </summary>
    [Required(ErrorMessage = "Le nom du fournisseur est obligatoire")]
    [StringLength(200, ErrorMessage = "Le nom ne peut pas dépasser 200 caractères")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Score de fiabilité (0-100)
    /// </summary>
    [Range(0, 100, ErrorMessage = "Le score de fiabilité doit être entre 0 et 100")]
    public int Reliability { get; set; }
    
    /// <summary>
    /// Score de prix (0-100)
    /// </summary>
    [Range(0, 100, ErrorMessage = "Le score de prix doit être entre 0 et 100")]
    public int PriceScore { get; set; }
    
    /// <summary>
    /// Score de livraison (0-100)
    /// </summary>
    [Range(0, 100, ErrorMessage = "Le score de livraison doit être entre 0 et 100")]
    public int DeliveryScore { get; set; }
    
    /// <summary>
    /// Score de qualité (0-100)
    /// </summary>
    [Range(0, 100, ErrorMessage = "Le score de qualité doit être entre 0 et 100")]
    public int QualityScore { get; set; }
    
    /// <summary>
    /// Score global du fournisseur (0-100)
    /// </summary>
    [Range(0, 100, ErrorMessage = "Le score global doit être entre 0 et 100")]
    public int OverallScore { get; set; }
}

