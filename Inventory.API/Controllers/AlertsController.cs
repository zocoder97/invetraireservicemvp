using Microsoft.AspNetCore.Mvc;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Domain.Enums;

namespace Inventory.API.Controllers;

/// <summary>
/// Contrôleur pour la gestion des alertes intelligentes
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AlertsController : ControllerBase
{
    private readonly ISmartAlertService _alertService;
    private readonly ILogger<AlertsController> _logger;

    public AlertsController(ISmartAlertService alertService, ILogger<AlertsController> logger)
    {
        _alertService = alertService;
        _logger = logger;
    }

    /// <summary>
    /// Récupère toutes les alertes
    /// </summary>
    /// <returns>Liste de toutes les alertes</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SmartAlertDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SmartAlertDto>>> GetAllAlerts()
    {
        try
        {
            var alerts = await _alertService.GetAllAlertsAsync();
            return Ok(alerts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des alertes");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère les alertes critiques
    /// </summary>
    /// <returns>Liste des alertes critiques</returns>
    [HttpGet("critical")]
    [ProducesResponseType(typeof(IEnumerable<SmartAlertDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SmartAlertDto>>> GetCriticalAlerts()
    {
        try
        {
            var alerts = await _alertService.GetCriticalAlertsAsync();
            return Ok(alerts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des alertes critiques");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère les alertes par type
    /// </summary>
    /// <param name="type">Type d'alerte (critical, warning, info, success)</param>
    /// <returns>Liste des alertes du type spécifié</returns>
    [HttpGet("type/{type}")]
    [ProducesResponseType(typeof(IEnumerable<SmartAlertDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SmartAlertDto>>> GetAlertsByType(string type)
    {
        try
        {
            if (!Enum.TryParse<AlertType>(type, true, out var alertType))
                return BadRequest($"Type d'alerte invalide: {type}");

            var alerts = await _alertService.GetAlertsByTypeAsync(alertType);
            return Ok(alerts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des alertes par type {AlertType}", type);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère les alertes non lues
    /// </summary>
    /// <returns>Liste des alertes non lues</returns>
    [HttpGet("unread")]
    [ProducesResponseType(typeof(IEnumerable<SmartAlertDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SmartAlertDto>>> GetUnreadAlerts()
    {
        try
        {
            var alerts = await _alertService.GetUnreadAlertsAsync();
            return Ok(alerts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des alertes non lues");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Crée une nouvelle alerte
    /// </summary>
    /// <param name="createAlertDto">Données de l'alerte à créer</param>
    /// <returns>L'alerte créée</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SmartAlertDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SmartAlertDto>> CreateAlert([FromBody] CreateSmartAlertDto createAlertDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdAlert = await _alertService.CreateAlertAsync(createAlertDto);
            return CreatedAtAction(nameof(GetAllAlerts), new { id = createdAlert.AlertId }, createdAlert);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création de l'alerte");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Marque une alerte comme lue
    /// </summary>
    /// <param name="id">Identifiant de l'alerte</param>
    /// <returns>Résultat de l'opération</returns>
    [HttpPut("{id:guid}/markAsRead")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        try
        {
            var success = await _alertService.MarkAsReadAsync(id);
            if (!success)
                return NotFound($"Alerte avec l'ID {id} non trouvée");

            return Ok(new { message = "Alerte marquée comme lue" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du marquage de l'alerte {AlertId} comme lue", id);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Supprime une alerte
    /// </summary>
    /// <param name="id">Identifiant de l'alerte à supprimer</param>
    /// <returns>Résultat de la suppression</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAlert(Guid id)
    {
        try
        {
            var deleted = await _alertService.DeleteAlertAsync(id);
            if (!deleted)
                return NotFound($"Alerte avec l'ID {id} non trouvée");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la suppression de l'alerte {AlertId}", id);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }
}

