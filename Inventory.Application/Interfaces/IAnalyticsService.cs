using Inventory.Application.DTOs;

namespace Inventory.Application.Interfaces;

/// <summary>
/// Interface pour le service d'analyses et de données IA
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Récupère toutes les prédictions de demande
    /// </summary>
    /// <returns>Liste des prédictions de demande</returns>
    Task<IEnumerable<DemandPredictionDto>> GetDemandPredictionsAsync();
    
    /// <summary>
    /// Récupère les dernières prédictions de demande
    /// </summary>
    /// <param name="days">Nombre de jours à récupérer</param>
    /// <returns>Liste des dernières prédictions</returns>
    Task<IEnumerable<DemandPredictionDto>> GetLatestDemandPredictionsAsync(int days = 7);
    
    /// <summary>
    /// Récupère toutes les analyses de coûts
    /// </summary>
    /// <returns>Liste des analyses de coûts</returns>
    Task<IEnumerable<CostAnalysisDto>> GetCostAnalysesAsync();
    
    /// <summary>
    /// Récupère l'analyse de coût par catégorie
    /// </summary>
    /// <param name="categoryName">Nom de la catégorie</param>
    /// <returns>L'analyse de coût pour la catégorie spécifiée</returns>
    Task<CostAnalysisDto?> GetCostAnalysisByCategoryAsync(string categoryName);
    
    /// <summary>
    /// Récupère toutes les analyses de prix du marché
    /// </summary>
    /// <returns>Liste des analyses de prix du marché</returns>
    Task<IEnumerable<MarketPriceAnalysisDto>> GetMarketPriceAnalysesAsync();
    
    /// <summary>
    /// Récupère l'analyse de prix pour un produit spécifique
    /// </summary>
    /// <param name="productName">Nom du produit</param>
    /// <returns>L'analyse de prix pour le produit spécifié</returns>
    Task<MarketPriceAnalysisDto?> GetMarketPriceAnalysisByProductAsync(string productName);
    
    /// <summary>
    /// Récupère toutes les données de saisonnalité
    /// </summary>
    /// <returns>Liste des données de saisonnalité</returns>
    Task<IEnumerable<SeasonalityAnalysisDto>> GetSeasonalityAnalysesAsync();
    
    /// <summary>
    /// Récupère les données de saisonnalité pour un mois spécifique
    /// </summary>
    /// <param name="monthName">Nom du mois</param>
    /// <returns>Les données de saisonnalité pour le mois spécifié</returns>
    Task<SeasonalityAnalysisDto?> GetSeasonalityAnalysisByMonthAsync(string monthName);
    
    /// <summary>
    /// Récupère tous les scores de performance IA
    /// </summary>
    /// <returns>Liste des scores de performance</returns>
    Task<IEnumerable<PerformanceScoreDto>> GetPerformanceScoresAsync();
    
    /// <summary>
    /// Récupère le score de performance pour une métrique spécifique
    /// </summary>
    /// <param name="metricName">Nom de la métrique</param>
    /// <returns>Le score de performance pour la métrique spécifiée</returns>
    Task<PerformanceScoreDto?> GetPerformanceScoreByMetricAsync(string metricName);
}

