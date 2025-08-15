using System.ComponentModel.DataAnnotations;
using Inventory.Domain.Enums;

namespace Inventory.Application.DTOs;

/// <summary>
/// DTO pour la création d'une alerte intelligente
/// </summary>
public class CreateSmartAlertDto
{
    /// <summary>
    /// Type d'alerte
    /// </summary>
    [Required(ErrorMessage = "Le type d'alerte est obligatoire")]
    public AlertType Type { get; set; }
    
    /// <summary>
    /// Message de l'alerte
    /// </summary>
    [Required(ErrorMessage = "Le message de l'alerte est obligatoire")]
    [StringLength(500, ErrorMessage = "Le message ne peut pas dépasser 500 caractères")]
    public string Message { get; set; } = string.Empty;
    
    /// <summary>
    /// Nombre d'éléments concernés par l'alerte
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Le nombre doit être positif")]
    public int Count { get; set; } = 1;
}

