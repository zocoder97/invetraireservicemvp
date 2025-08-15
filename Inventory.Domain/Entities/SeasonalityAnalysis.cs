namespace Inventory.Domain.Entities;

/// <summary>
/// Entité représentant une analyse de saisonnalité
/// </summary>
public class SeasonalityAnalysis
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
    
    /// <summary>
    /// Date de création
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Date de dernière mise à jour
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}

