namespace Inventory.Application.DTOs;

/// <summary>
/// DTO pour l'entité DemandPrediction
/// </summary>
public class DemandPredictionDto
{
    /// <summary>
    /// Identifiant unique de la prédiction
    /// </summary>
    public Guid PredictionId { get; set; }
    
    /// <summary>
    /// Date de la prédiction (format string pour le frontend)
    /// </summary>
    public string Date { get; set; } = string.Empty;
    
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
}

