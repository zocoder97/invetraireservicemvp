using Microsoft.AspNetCore.Mvc;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;

namespace Inventory.API.Controllers;

/// <summary>
/// Contrôleur pour la gestion des fournisseurs
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;
    private readonly ILogger<SuppliersController> _logger;

    public SuppliersController(ISupplierService supplierService, ILogger<SuppliersController> logger)
    {
        _supplierService = supplierService;
        _logger = logger;
    }

    /// <summary>
    /// Récupère tous les fournisseurs
    /// </summary>
    /// <returns>Liste de tous les fournisseurs</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SupplierDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAllSuppliers()
    {
        try
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des fournisseurs");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère un fournisseur par son identifiant
    /// </summary>
    /// <param name="id">Identifiant du fournisseur</param>
    /// <returns>Le fournisseur trouvé</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SupplierDto>> GetSupplierById(Guid id)
    {
        try
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
                return NotFound($"Fournisseur avec l'ID {id} non trouvé");

            return Ok(supplier);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération du fournisseur {SupplierId}", id);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Crée un nouveau fournisseur
    /// </summary>
    /// <param name="createSupplierDto">Données du fournisseur à créer</param>
    /// <returns>Le fournisseur créé</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SupplierDto>> CreateSupplier([FromBody] CreateSupplierDto createSupplierDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdSupplier = await _supplierService.CreateSupplierAsync(createSupplierDto);
            return CreatedAtAction(nameof(GetSupplierById), new { id = createdSupplier.SupplierId }, createdSupplier);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création du fournisseur");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Met à jour un fournisseur existant
    /// </summary>
    /// <param name="id">Identifiant du fournisseur</param>
    /// <param name="updateSupplierDto">Données de mise à jour</param>
    /// <returns>Le fournisseur mis à jour</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SupplierDto>> UpdateSupplier(Guid id, [FromBody] UpdateSupplierDto updateSupplierDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedSupplier = await _supplierService.UpdateSupplierAsync(id, updateSupplierDto);
            if (updatedSupplier == null)
                return NotFound($"Fournisseur avec l'ID {id} non trouvé");

            return Ok(updatedSupplier);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la mise à jour du fournisseur {SupplierId}", id);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Supprime un fournisseur
    /// </summary>
    /// <param name="id">Identifiant du fournisseur à supprimer</param>
    /// <returns>Résultat de la suppression</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSupplier(Guid id)
    {
        try
        {
            var deleted = await _supplierService.DeleteSupplierAsync(id);
            if (!deleted)
                return NotFound($"Fournisseur avec l'ID {id} non trouvé");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la suppression du fournisseur {SupplierId}", id);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère les meilleurs fournisseurs triés par score global
    /// </summary>
    /// <param name="count">Nombre de fournisseurs à retourner (par défaut: 10)</param>
    /// <returns>Liste des meilleurs fournisseurs</returns>
    [HttpGet("top")]
    [ProducesResponseType(typeof(IEnumerable<SupplierDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SupplierDto>>> GetTopSuppliers([FromQuery] int count = 10)
    {
        try
        {
            var suppliers = await _supplierService.GetTopSuppliersAsync(count);
            return Ok(suppliers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des meilleurs fournisseurs");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }
}

