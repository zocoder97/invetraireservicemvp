namespace Inventory.Application.DTOs;

/// <summary>
/// DTO pour l'entité Supplier
/// </summary>
public class SupplierDto
{
    /// <summary>
    /// Identifiant unique du fournisseur
    /// </summary>
    public Guid SupplierId { get; set; }
    
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
}

