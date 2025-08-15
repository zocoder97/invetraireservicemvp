using Inventory.Domain.Enums;

namespace Inventory.Domain.Entities;

/// <summary>
/// Entité représentant une analyse de coût
/// </summary>
public class CostAnalysis
{
    /// <summary>
    /// Identifiant unique de l'analyse de coût
    /// </summary>
    public Guid CostAnalysisId { get; set; }
    
    /// <summary>
    /// Catégorie de coût
    /// </summary>
    public string Category { get; set; } = string.Empty;
    
    /// <summary>
    /// Budget alloué
    /// </summary>
    public decimal Budget { get; set; }
    
    /// <summary>
    /// Montant dépensé
    /// </summary>
    public decimal Spent { get; set; }
    
    /// <summary>
    /// Variance (%) par rapport au budget
    /// </summary>
    public decimal Variance { get; set; }
    
    /// <summary>
    /// Tendance du coût
    /// </summary>
    public CostTrend Trend { get; set; }
    
    /// <summary>
    /// Date de création
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Date de dernière mise à jour
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}

