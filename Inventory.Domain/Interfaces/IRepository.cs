using System.Linq.Expressions;

namespace Inventory.Domain.Interfaces;

/// <summary>
/// Interface générique pour les dépôts
/// </summary>
/// <typeparam name="T">Type de l'entité</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Récupère toutes les entités
    /// </summary>
    /// <returns>Liste de toutes les entités</returns>
    Task<IEnumerable<T>> GetAllAsync();
    
    /// <summary>
    /// Récupère une entité par son identifiant
    /// </summary>
    /// <param name="id">Identifiant de l'entité</param>
    /// <returns>L'entité trouvée ou null</returns>
    Task<T?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Trouve des entités selon un prédicat
    /// </summary>
    /// <param name="predicate">Prédicat de recherche</param>
    /// <returns>Liste des entités trouvées</returns>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    /// Ajoute une nouvelle entité
    /// </summary>
    /// <param name="entity">Entité à ajouter</param>
    /// <returns>L'entité ajoutée</returns>
    Task<T> AddAsync(T entity);
    
    /// <summary>
    /// Met à jour une entité existante
    /// </summary>
    /// <param name="entity">Entité à mettre à jour</param>
    /// <returns>L'entité mise à jour</returns>
    Task<T> UpdateAsync(T entity);
    
    /// <summary>
    /// Supprime une entité
    /// </summary>
    /// <param name="entity">Entité à supprimer</param>
    /// <returns>Task</returns>
    Task DeleteAsync(T entity);
    
    /// <summary>
    /// Supprime une entité par son identifiant
    /// </summary>
    /// <param name="id">Identifiant de l'entité à supprimer</param>
    /// <returns>Task</returns>
    Task DeleteByIdAsync(Guid id);
    
    /// <summary>
    /// Vérifie si une entité existe
    /// </summary>
    /// <param name="predicate">Prédicat de recherche</param>
    /// <returns>True si l'entité existe, false sinon</returns>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    /// Compte le nombre d'entités selon un prédicat
    /// </summary>
    /// <param name="predicate">Prédicat de recherche</param>
    /// <returns>Nombre d'entités</returns>
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
}

