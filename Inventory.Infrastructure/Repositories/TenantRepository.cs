using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Persistence;

namespace Inventory.Infrastructure.Repositories;

/// <summary>
/// Implémentation du dépôt avec support multi-tenant
/// </summary>
/// <typeparam name="T">Type de l'entité</typeparam>
public class TenantRepository<T> : ITenantRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;
    private readonly PropertyInfo? _entrepriseIdProperty;

    public TenantRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
        
        // Récupérer la propriété EntrepriseId par réflexion
        _entrepriseIdProperty = typeof(T).GetProperty("EntrepriseId");
        
        if (_entrepriseIdProperty == null)
        {
            throw new InvalidOperationException($"L'entité {typeof(T).Name} doit avoir une propriété EntrepriseId pour le support multi-tenant");
        }
    }

    public virtual async Task<IEnumerable<T>> GetAllByEntrepriseAsync(Guid entrepriseId)
    {
        return await _dbSet.Where(CreateEntrepriseFilter(entrepriseId)).ToListAsync();
    }

    public virtual async Task<T?> GetByIdAndEntrepriseAsync(Guid id, Guid entrepriseId)
    {
        var idProperty = typeof(T).GetProperties()
            .FirstOrDefault(p => p.Name.EndsWith("Id") && p.PropertyType == typeof(Guid));
        
        if (idProperty == null)
            return null;

        return await _dbSet.Where(CreateEntrepriseFilter(entrepriseId))
            .FirstOrDefaultAsync(CreateIdFilter(id, idProperty.Name));
    }

    public virtual async Task<IEnumerable<T>> FindByEntrepriseAsync(Expression<Func<T, bool>> predicate, Guid entrepriseId)
    {
        return await _dbSet.Where(CreateEntrepriseFilter(entrepriseId))
            .Where(predicate)
            .ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity, Guid entrepriseId)
    {
        // Définir l'EntrepriseId sur l'entité
        _entrepriseIdProperty!.SetValue(entity, entrepriseId);
        
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T?> UpdateAsync(T entity, Guid entrepriseId)
    {
        // Vérifier que l'entité appartient à l'entreprise
        var entityEntrepriseId = (Guid)_entrepriseIdProperty!.GetValue(entity)!;
        if (entityEntrepriseId != entrepriseId)
            return null;

        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(Guid id, Guid entrepriseId)
    {
        var entity = await GetByIdAndEntrepriseAsync(id, entrepriseId);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, Guid entrepriseId)
    {
        return await _dbSet.Where(CreateEntrepriseFilter(entrepriseId))
            .AnyAsync(predicate);
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate, Guid entrepriseId)
    {
        return await _dbSet.Where(CreateEntrepriseFilter(entrepriseId))
            .CountAsync(predicate);
    }

    /// <summary>
    /// Crée un filtre pour l'EntrepriseId
    /// </summary>
    /// <param name="entrepriseId">Identifiant de l'entreprise</param>
    /// <returns>Expression de filtre</returns>
    private Expression<Func<T, bool>> CreateEntrepriseFilter(Guid entrepriseId)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, "EntrepriseId");
        var constant = Expression.Constant(entrepriseId);
        var equal = Expression.Equal(property, constant);
        
        return Expression.Lambda<Func<T, bool>>(equal, parameter);
    }

    /// <summary>
    /// Crée un filtre pour l'ID de l'entité
    /// </summary>
    /// <param name="id">Identifiant de l'entité</param>
    /// <param name="propertyName">Nom de la propriété ID</param>
    /// <returns>Expression de filtre</returns>
    private Expression<Func<T, bool>> CreateIdFilter(Guid id, string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var constant = Expression.Constant(id);
        var equal = Expression.Equal(property, constant);
        
        return Expression.Lambda<Func<T, bool>>(equal, parameter);
    }
}

