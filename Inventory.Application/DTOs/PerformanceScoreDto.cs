namespace Inventory.Application.DTOs;

/// <summary>
/// DTO pour l'entité PerformanceScore
/// </summary>
public class PerformanceScoreDto
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
    public string Status { get; set; } = string.Empty;
}

