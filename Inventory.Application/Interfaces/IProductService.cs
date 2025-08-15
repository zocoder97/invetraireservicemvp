using Inventory.Application.DTOs;

namespace Inventory.Application.Interfaces;

/// <summary>
/// Interface pour le service de gestion des produits avec support multi-tenant
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Récupère tous les produits d'une entreprise
    /// </summary>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>Liste de tous les produits de l'entreprise</returns>
    Task<IEnumerable<ProductDto>> GetAllProductsAsync(Guid entrepriseId);
    
    /// <summary>
    /// Récupère un produit par son identifiant pour une entreprise donnée
    /// </summary>
    /// <param name="id">Identifiant du produit</param>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>Le produit trouvé ou null</returns>
    Task<ProductDto?> GetProductByIdAsync(Guid id, Guid entrepriseId);
    
    /// <summary>
    /// Crée un nouveau produit pour une entreprise
    /// </summary>
    /// <param name="createProductDto">Données du produit à créer</param>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>Le produit créé</returns>
    Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto, Guid entrepriseId);
    
    /// <summary>
    /// Met à jour un produit existant d'une entreprise
    /// </summary>
    /// <param name="id">Identifiant du produit</param>
    /// <param name="updateProductDto">Données de mise à jour</param>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>Le produit mis à jour ou null si non trouvé</returns>
    Task<ProductDto?> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto, Guid entrepriseId);
    
    /// <summary>
    /// Supprime un produit d'une entreprise
    /// </summary>
    /// <param name="id">Identifiant du produit à supprimer</param>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>True si supprimé avec succès, false sinon</returns>
    Task<bool> DeleteProductAsync(Guid id, Guid entrepriseId);
    
    /// <summary>
    /// Récupère les produits avec un stock critique d'une entreprise
    /// </summary>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>Liste des produits en stock critique</returns>
    Task<IEnumerable<ProductDto>> GetCriticalStockProductsAsync(Guid entrepriseId);
    
    /// <summary>
    /// Récupère les produits nécessitant un réapprovisionnement d'une entreprise
    /// </summary>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>Liste des produits à réapprovisionner</returns>
    Task<IEnumerable<ProductDto>> GetProductsToReorderAsync(Guid entrepriseId);
}

