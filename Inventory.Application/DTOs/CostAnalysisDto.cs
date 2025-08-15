namespace Inventory.Application.DTOs;

/// <summary>
/// DTO pour l'entité CostAnalysis
/// </summary>
public class CostAnalysisDto
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
    public string Trend { get; set; } = string.Empty;
}

