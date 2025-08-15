using Inventory.Application.DTOs;
using Inventory.Domain.Enums;

namespace Inventory.Application.Interfaces;

/// <summary>
/// Interface pour le service de gestion des alertes intelligentes
/// </summary>
public interface ISmartAlertService
{
    /// <summary>
    /// Récupère toutes les alertes
    /// </summary>
    /// <returns>Liste de toutes les alertes</returns>
    Task<IEnumerable<SmartAlertDto>> GetAllAlertsAsync();
    
    /// <summary>
    /// Récupère les alertes par type
    /// </summary>
    /// <param name="alertType">Type d'alerte</param>
    /// <returns>Liste des alertes du type spécifié</returns>
    Task<IEnumerable<SmartAlertDto>> GetAlertsByTypeAsync(AlertType alertType);
    
    /// <summary>
    /// Récupère les alertes critiques
    /// </summary>
    /// <returns>Liste des alertes critiques</returns>
    Task<IEnumerable<SmartAlertDto>> GetCriticalAlertsAsync();
    
    /// <summary>
    /// Récupère les alertes non lues
    /// </summary>
    /// <returns>Liste des alertes non lues</returns>
    Task<IEnumerable<SmartAlertDto>> GetUnreadAlertsAsync();
    
    /// <summary>
    /// Crée une nouvelle alerte
    /// </summary>
    /// <param name="createAlertDto">Données de l'alerte à créer</param>
    /// <returns>L'alerte créée</returns>
    Task<SmartAlertDto> CreateAlertAsync(CreateSmartAlertDto createAlertDto);
    
    /// <summary>
    /// Marque une alerte comme lue
    /// </summary>
    /// <param name="alertId">Identifiant de l'alerte</param>
    /// <returns>True si marquée comme lue avec succès, false sinon</returns>
    Task<bool> MarkAsReadAsync(Guid alertId);
    
    /// <summary>
    /// Supprime une alerte
    /// </summary>
    /// <param name="alertId">Identifiant de l'alerte à supprimer</param>
    /// <returns>True si supprimée avec succès, false sinon</returns>
    Task<bool> DeleteAlertAsync(Guid alertId);
}

