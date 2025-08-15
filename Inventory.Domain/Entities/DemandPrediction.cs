namespace Inventory.Domain.Entities;

/// <summary>
/// Entité représentant une prédiction de demande
/// </summary>
public class DemandPrediction
{
    /// <summary>
    /// Identifiant unique de la prédiction
    /// </summary>
    public Guid PredictionId { get; set; }
    
    /// <summary>
    /// Date de la prédiction
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Demande réelle (peut être nulle pour les prédictions futures)
    /// </summary>
    public int? Actual { get; set; }
    
    /// <summary>
    /// Demande prédite
    /// </summary>
    public int Predicted { get; set; }
    
    /// <summary>
    /// Niveau de confiance de la prédiction (0-100)
    /// </summary>
    public int Confidence { get; set; }
    
    /// <summary>
    /// Date de création de la prédiction
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

