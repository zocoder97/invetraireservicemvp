using Microsoft.AspNetCore.Mvc;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.API.Services;

namespace Inventory.API.Controllers;

/// <summary>
/// Contrôleur pour la gestion des produits avec support multi-tenant
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ITenantService _tenantService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ITenantService tenantService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _tenantService = tenantService;
        _logger = logger;
    }

    /// <summary>
    /// Récupère tous les produits de l'entreprise
    /// </summary>
    /// <returns>Liste de tous les produits de l'entreprise</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        try
        {
            var entrepriseId = _tenantService.GetCurrentEntrepriseId();
            if (entrepriseId == null)
                return BadRequest("EntrepriseId manquant. Veuillez fournir l'header 'X-Entreprise-Id'.");

            var products = await _productService.GetAllProductsAsync(entrepriseId.Value);
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des produits");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère un produit par son identifiant
    /// </summary>
    /// <param name="id">Identifiant du produit</param>
    /// <returns>Le produit trouvé</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
    {
        try
        {
            var entrepriseId = _tenantService.GetCurrentEntrepriseId();
            if (entrepriseId == null)
                return BadRequest("EntrepriseId manquant. Veuillez fournir l'header 'X-Entreprise-Id'.");

            var product = await _productService.GetProductByIdAsync(id, entrepriseId.Value);
            if (product == null)
                return NotFound($"Produit avec l'ID {id} non trouvé pour cette entreprise");

            return Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération du produit {ProductId}", id);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Crée un nouveau produit
    /// </summary>
    /// <param name="createProductDto">Données du produit à créer</param>
    /// <returns>Le produit créé</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        try
        {
            var entrepriseId = _tenantService.GetCurrentEntrepriseId();
            if (entrepriseId == null)
                return BadRequest("EntrepriseId manquant. Veuillez fournir l'header 'X-Entreprise-Id'.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdProduct = await _productService.CreateProductAsync(createProductDto, entrepriseId.Value);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, createdProduct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création du produit");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Met à jour un produit existant
    /// </summary>
    /// <param name="id">Identifiant du produit</param>
    /// <param name="updateProductDto">Données de mise à jour</param>
    /// <returns>Le produit mis à jour</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> UpdateProduct(Guid id, [FromBody] UpdateProductDto updateProductDto)
    {
        try
        {
            var entrepriseId = _tenantService.GetCurrentEntrepriseId();
            if (entrepriseId == null)
                return BadRequest("EntrepriseId manquant. Veuillez fournir l'header 'X-Entreprise-Id'.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedProduct = await _productService.UpdateProductAsync(id, updateProductDto, entrepriseId.Value);
            if (updatedProduct == null)
                return NotFound($"Produit avec l'ID {id} non trouvé pour cette entreprise");

            return Ok(updatedProduct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la mise à jour du produit {ProductId}", id);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Supprime un produit
    /// </summary>
    /// <param name="id">Identifiant du produit à supprimer</param>
    /// <returns>Résultat de la suppression</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var entrepriseId = _tenantService.GetCurrentEntrepriseId();
            if (entrepriseId == null)
                return BadRequest("EntrepriseId manquant. Veuillez fournir l'header 'X-Entreprise-Id'.");

            var deleted = await _productService.DeleteProductAsync(id, entrepriseId.Value);
            if (!deleted)
                return NotFound($"Produit avec l'ID {id} non trouvé pour cette entreprise");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la suppression du produit {ProductId}", id);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère les produits avec un stock critique
    /// </summary>
    /// <returns>Liste des produits en stock critique</returns>
    [HttpGet("critical-stock")]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetCriticalStockProducts()
    {
        try
        {
            var entrepriseId = _tenantService.GetCurrentEntrepriseId();
            if (entrepriseId == null)
                return BadRequest("EntrepriseId manquant. Veuillez fournir l'header 'X-Entreprise-Id'.");

            var products = await _productService.GetCriticalStockProductsAsync(entrepriseId.Value);
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des produits en stock critique");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère les produits nécessitant un réapprovisionnement
    /// </summary>
    /// <returns>Liste des produits à réapprovisionner</returns>
    [HttpGet("reorder")]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsToReorder()
    {
        try
        {
            var entrepriseId = _tenantService.GetCurrentEntrepriseId();
            if (entrepriseId == null)
                return BadRequest("EntrepriseId manquant. Veuillez fournir l'header 'X-Entreprise-Id'.");

            var products = await _productService.GetProductsToReorderAsync(entrepriseId.Value);
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des produits à réapprovisionner");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }
}

