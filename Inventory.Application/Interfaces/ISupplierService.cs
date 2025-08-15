using Inventory.Application.DTOs;

namespace Inventory.Application.Interfaces;

/// <summary>
/// Interface pour le service de gestion des fournisseurs
/// </summary>
public interface ISupplierService
{
    /// <summary>
    /// Récupère tous les fournisseurs
    /// </summary>
    /// <returns>Liste de tous les fournisseurs</returns>
    Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync();
    
    /// <summary>
    /// Récupère un fournisseur par son identifiant
    /// </summary>
    /// <param name="id">Identifiant du fournisseur</param>
    /// <returns>Le fournisseur trouvé ou null</returns>
    Task<SupplierDto?> GetSupplierByIdAsync(Guid id);
    
    /// <summary>
    /// Crée un nouveau fournisseur
    /// </summary>
    /// <param name="createSupplierDto">Données du fournisseur à créer</param>
    /// <returns>Le fournisseur créé</returns>
    Task<SupplierDto> CreateSupplierAsync(CreateSupplierDto createSupplierDto);
    
    /// <summary>
    /// Met à jour un fournisseur existant
    /// </summary>
    /// <param name="id">Identifiant du fournisseur</param>
    /// <param name="updateSupplierDto">Données de mise à jour</param>
    /// <returns>Le fournisseur mis à jour ou null si non trouvé</returns>
    Task<SupplierDto?> UpdateSupplierAsync(Guid id, UpdateSupplierDto updateSupplierDto);
    
    /// <summary>
    /// Supprime un fournisseur
    /// </summary>
    /// <param name="id">Identifiant du fournisseur à supprimer</param>
    /// <returns>True si supprimé avec succès, false sinon</returns>
    Task<bool> DeleteSupplierAsync(Guid id);
    
    /// <summary>
    /// Récupère les meilleurs fournisseurs triés par score global
    /// </summary>
    /// <param name="count">Nombre de fournisseurs à retourner</param>
    /// <returns>Liste des meilleurs fournisseurs</returns>
    Task<IEnumerable<SupplierDto>> GetTopSuppliersAsync(int count = 10);
}

