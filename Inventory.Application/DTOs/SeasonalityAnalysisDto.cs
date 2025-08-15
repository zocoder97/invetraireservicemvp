namespace Inventory.Application.DTOs;

/// <summary>
/// DTO pour l'entité SeasonalityAnalysis
/// </summary>
public class SeasonalityAnalysisDto
{
    /// <summary>
    /// Identifiant unique de l'analyse de saisonnalité
    /// </summary>
    public Guid SeasonalityId { get; set; }
    
    /// <summary>
    /// Mois
    /// </summary>
    public string Month { get; set; } = string.Empty;
    
    /// <summary>
    /// Donnée pour les soins capillaires
    /// </summary>
    public int HairCare { get; set; }
    
    /// <summary>
    /// Donnée pour les soins de la peau
    /// </summary>
    public int SkinCare { get; set; }
    
    /// <summary>
    /// Donnée pour les ongles
    /// </summary>
    public int Nails { get; set; }
    
    /// <summary>
    /// Donnée pour l'équipement
    /// </summary>
    public int Equipment { get; set; }
}

