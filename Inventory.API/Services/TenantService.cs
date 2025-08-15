namespace Inventory.API.Services;

/// <summary>
/// Service pour gérer les informations du tenant (entreprise) actuel
/// </summary>
public interface ITenantService
{
    /// <summary>
    /// Récupère l'identifiant de l'entreprise depuis le contexte HTTP
    /// </summary>
    /// <returns>Identifiant de l'entreprise ou null si non trouvé</returns>
    Guid? GetCurrentEntrepriseId();
}

/// <summary>
/// Implémentation du service tenant
/// </summary>
public class TenantService : ITenantService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<TenantService> _logger;

    public TenantService(IHttpContextAccessor httpContextAccessor, ILogger<TenantService> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public Guid? GetCurrentEntrepriseId()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            _logger.LogWarning("HttpContext est null, impossible de récupérer l'EntrepriseId");
            return null;
        }

        // Essayer de récupérer l'EntrepriseId depuis les headers
        if (httpContext.Request.Headers.TryGetValue("X-Entreprise-Id", out var headerValue))
        {
            if (Guid.TryParse(headerValue.FirstOrDefault(), out var entrepriseId))
            {
                _logger.LogDebug("EntrepriseId récupéré depuis le header: {EntrepriseId}", entrepriseId);
                return entrepriseId;
            }
        }

        // Essayer de récupérer depuis les claims JWT (si authentification implémentée)
        var entrepriseIdClaim = httpContext.User?.FindFirst("entreprise_id")?.Value;
        if (!string.IsNullOrEmpty(entrepriseIdClaim) && Guid.TryParse(entrepriseIdClaim, out var claimEntrepriseId))
        {
            _logger.LogDebug("EntrepriseId récupéré depuis les claims: {EntrepriseId}", claimEntrepriseId);
            return claimEntrepriseId;
        }

        // Essayer de récupérer depuis les query parameters (pour les tests)
        if (httpContext.Request.Query.TryGetValue("entrepriseId", out var queryValue))
        {
            if (Guid.TryParse(queryValue.FirstOrDefault(), out var queryEntrepriseId))
            {
                _logger.LogDebug("EntrepriseId récupéré depuis les query params: {EntrepriseId}", queryEntrepriseId);
                return queryEntrepriseId;
            }
        }

        _logger.LogWarning("Aucun EntrepriseId trouvé dans la requête");
        return null;
    }
}

