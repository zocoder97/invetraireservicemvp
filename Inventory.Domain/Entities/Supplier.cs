using System.ComponentModel.DataAnnotations;

namespace Inventory.Domain.Entities;

/// <summary>
/// Entité représentant un fournisseur
/// </summary>
public class Supplier
{
    /// <summary>
    /// Identifiant unique du fournisseur
    /// </summary>
    public Guid SupplierId { get; set; }
    
    /// <summary>
    /// Identifiant de l'entreprise/salon propriétaire
    /// </summary>
    [Required]
    public Guid EntrepriseId { get; set; }
    
    /// <summary>
    /// Nom du fournisseur
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Score de fiabilité (0-100)
    /// </summary>
    public int Reliability { get; set; }
    
    /// <summary>
    /// Score de prix (0-100)
    /// </summary>
    public int PriceScore { get; set; }
    
    /// <summary>
    /// Score de livraison (0-100)
    /// </summary>
    public int DeliveryScore { get; set; }
    
    /// <summary>
    /// Score de qualité (0-100)
    /// </summary>
    public int QualityScore { get; set; }
    
    /// <summary>
    /// Score global du fournisseur (0-100)
    /// </summary>
    public int OverallScore { get; set; }
    
    /// <summary>
    /// Date de création
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Date de dernière mise à jour
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}

