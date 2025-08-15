using Inventory.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Domain.Entities;

/// <summary>
/// Entité représentant une alerte intelligente
/// </summary>
public class SmartAlert
{
    /// <summary>
    /// Identifiant unique de l'alerte
    /// </summary>
    public Guid AlertId { get; set; }
    
    /// <summary>
    /// Identifiant de l'entreprise/salon propriétaire
    /// </summary>
    [Required]
    public Guid EntrepriseId { get; set; }
    
    /// <summary>
    /// Type d'alerte
    /// </summary>
    public AlertType Type { get; set; }
    
    /// <summary>
    /// Message de l'alerte
    /// </summary>
    public string Message { get; set; } = string.Empty;
    
    /// <summary>
    /// Nombre d'éléments concernés par l'alerte
    /// </summary>
    public int Count { get; set; }
    
    /// <summary>
    /// Date et heure de création de l'alerte
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Indique si l'alerte a été lue
    /// </summary>
    public bool IsRead { get; set; }
}

