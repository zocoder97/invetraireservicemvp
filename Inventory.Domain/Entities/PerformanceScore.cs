using Inventory.Domain.Enums;

namespace Inventory.Domain.Entities;

/// <summary>
/// Entité représentant un score de performance IA
/// </summary>
public class PerformanceScore
{
    /// <summary>
    /// Identifiant unique du score
    /// </summary>
    public Guid ScoreId { get; set; }
    
    /// <summary>
    /// Métrique de performance
    /// </summary>
    public string Metric { get; set; } = string.Empty;
    
    /// <summary>
    /// Score obtenu (0-100)
    /// </summary>
    public int Score { get; set; }
    
    /// <summary>
    /// Cible (0-100)
    /// </summary>
    public int Target { get; set; }
    
    /// <summary>
    /// Statut de la performance
    /// </summary>
    public PerformanceStatus Status { get; set; }
    
    /// <summary>
    /// Date de création
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Date de dernière mise à jour
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}

