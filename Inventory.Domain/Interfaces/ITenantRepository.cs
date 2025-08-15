using System.Linq.Expressions;

namespace Inventory.Domain.Interfaces;

/// <summary>
/// Interface pour les dépôts avec support multi-tenant
/// </summary>
/// <typeparam name="T">Type de l'entité</typeparam>
public interface ITenantRepository<T> where T : class
{
    /// <summary>
    /// Récupère toutes les entités pour une entreprise donnée
    /// </summary>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>Liste des entités de l'entreprise</returns>
    Task<IEnumerable<T>> GetAllByEntrepriseAsync(Guid entrepriseId);
    
    /// <summary>
    /// Récupère une entité par son ID pour une entreprise donnée
    /// </summary>
    /// <param name="id">Identifiant de l'entité</param>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>L'entité trouvée ou null</returns>
    Task<T?> GetByIdAndEntrepriseAsync(Guid id, Guid entrepriseId);
    
    /// <summary>
    /// Recherche des entités selon un prédicat pour une entreprise donnée
    /// </summary>
    /// <param name="predicate">Prédicat de recherche</param>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>Liste des entités correspondantes</returns>
    Task<IEnumerable<T>> FindByEntrepriseAsync(Expression<Func<T, bool>> predicate, Guid entrepriseId);
    
    /// <summary>
    /// Ajoute une nouvelle entité avec l'ID d'entreprise
    /// </summary>
    /// <param name="entity">Entité à ajouter</param>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>L'entité ajoutée</returns>
    Task<T> AddAsync(T entity, Guid entrepriseId);
    
    /// <summary>
    /// Met à jour une entité en vérifiant qu'elle appartient à l'entreprise
    /// </summary>
    /// <param name="entity">Entité à mettre à jour</param>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>L'entité mise à jour ou null si non autorisée</returns>
    Task<T?> UpdateAsync(T entity, Guid entrepriseId);
    
    /// <summary>
    /// Supprime une entité en vérifiant qu'elle appartient à l'entreprise
    /// </summary>
    /// <param name="id">Identifiant de l'entité</param>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>True si supprimée, false sinon</returns>
    Task<bool> DeleteAsync(Guid id, Guid entrepriseId);
    
    /// <summary>
    /// Vérifie si une entité existe pour une entreprise donnée
    /// </summary>
    /// <param name="predicate">Prédicat de recherche</param>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>True si l'entité existe, false sinon</returns>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, Guid entrepriseId);
    
    /// <summary>
    /// Compte les entités selon un prédicat pour une entreprise donnée
    /// </summary>
    /// <param name="predicate">Prédicat de recherche</param>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>Nombre d'entités correspondantes</returns>
    Task<int> CountAsync(Expression<Func<T, bool>> predicate, Guid entrepriseId);
}

