using Inventory.Domain.Enums;

namespace Inventory.Application.DTOs;

/// <summary>
/// DTO pour l'entité SmartAlert
/// </summary>
public class SmartAlertDto
{
    /// <summary>
    /// Identifiant unique de l'alerte
    /// </summary>
    public Guid AlertId { get; set; }
    
    /// <summary>
    /// Type d'alerte
    /// </summary>
    public string Type { get; set; } = string.Empty;
    
    /// <summary>
    /// Message de l'alerte
    /// </summary>
    public string Message { get; set; } = string.Empty;
    
    /// <summary>
    /// Nombre d'éléments concernés par l'alerte
    /// </summary>
    public int Count { get; set; }
    
    /// <summary>
    /// Temps écoulé depuis la création (ex: "2h", "1j")
    /// </summary>
    public string Time { get; set; } = string.Empty;
    
    /// <summary>
    /// Indique si l'alerte a été lue
    /// </summary>
    public bool IsRead { get; set; }
}

