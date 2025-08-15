namespace Inventory.Domain.Entities;

/// <summary>
/// Entité représentant une analyse de prix du marché
/// </summary>
public class MarketPriceAnalysis
{
    /// <summary>
    /// Identifiant unique de l'analyse de prix
    /// </summary>
    public Guid MarketPriceId { get; set; }
    
    /// <summary>
    /// Nom du produit
    /// </summary>
    public string Product { get; set; } = string.Empty;
    
    /// <summary>
    /// Prix actuel
    /// </summary>
    public decimal CurrentPrice { get; set; }
    
    /// <summary>
    /// Prix moyen du marché
    /// </summary>
    public decimal MarketAvg { get; set; }
    
    /// <summary>
    /// Meilleur prix détecté
    /// </summary>
    public decimal BestPrice { get; set; }
    
    /// <summary>
    /// Économies potentielles
    /// </summary>
    public decimal Savings { get; set; }
    
    /// <summary>
    /// Date de création
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Date de dernière mise à jour
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}

