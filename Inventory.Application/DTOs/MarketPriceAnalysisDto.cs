namespace Inventory.Application.DTOs;

/// <summary>
/// DTO pour l'entité MarketPriceAnalysis
/// </summary>
public class MarketPriceAnalysisDto
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
}

