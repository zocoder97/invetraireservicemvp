using Microsoft.AspNetCore.Mvc;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;

namespace Inventory.API.Controllers;

/// <summary>
/// Contrôleur pour les analyses et données IA
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;
    private readonly ILogger<AnalyticsController> _logger;

    public AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger)
    {
        _analyticsService = analyticsService;
        _logger = logger;
    }

    /// <summary>
    /// Récupère toutes les prédictions de demande
    /// </summary>
    /// <returns>Liste des prédictions de demande</returns>
    [HttpGet("demandpredictions")]
    [ProducesResponseType(typeof(IEnumerable<DemandPredictionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DemandPredictionDto>>> GetDemandPredictions()
    {
        try
        {
            var predictions = await _analyticsService.GetDemandPredictionsAsync();
            return Ok(predictions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des prédictions de demande");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère les dernières prédictions de demande
    /// </summary>
    /// <param name="days">Nombre de jours à récupérer (par défaut: 7)</param>
    /// <returns>Liste des dernières prédictions</returns>
    [HttpGet("demandpredictions/latest")]
    [ProducesResponseType(typeof(IEnumerable<DemandPredictionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DemandPredictionDto>>> GetLatestDemandPredictions([FromQuery] int days = 7)
    {
        try
        {
            var predictions = await _analyticsService.GetLatestDemandPredictionsAsync(days);
            return Ok(predictions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des dernières prédictions de demande");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère toutes les analyses de coûts
    /// </summary>
    /// <returns>Liste des analyses de coûts</returns>
    [HttpGet("costanalysis")]
    [ProducesResponseType(typeof(IEnumerable<CostAnalysisDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CostAnalysisDto>>> GetCostAnalyses()
    {
        try
        {
            var costAnalyses = await _analyticsService.GetCostAnalysesAsync();
            return Ok(costAnalyses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des analyses de coûts");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère l'analyse de coût par catégorie
    /// </summary>
    /// <param name="categoryName">Nom de la catégorie</param>
    /// <returns>L'analyse de coût pour la catégorie spécifiée</returns>
    [HttpGet("costanalysis/category/{categoryName}")]
    [ProducesResponseType(typeof(CostAnalysisDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CostAnalysisDto>> GetCostAnalysisByCategory(string categoryName)
    {
        try
        {
            var costAnalysis = await _analyticsService.GetCostAnalysisByCategoryAsync(categoryName);
            if (costAnalysis == null)
                return NotFound($"Analyse de coût pour la catégorie '{categoryName}' non trouvée");

            return Ok(costAnalysis);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération de l'analyse de coût pour la catégorie {CategoryName}", categoryName);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère toutes les analyses de prix du marché
    /// </summary>
    /// <returns>Liste des analyses de prix du marché</returns>
    [HttpGet("marketprices")]
    [ProducesResponseType(typeof(IEnumerable<MarketPriceAnalysisDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MarketPriceAnalysisDto>>> GetMarketPriceAnalyses()
    {
        try
        {
            var marketPrices = await _analyticsService.GetMarketPriceAnalysesAsync();
            return Ok(marketPrices);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des analyses de prix du marché");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère l'analyse de prix pour un produit spécifique
    /// </summary>
    /// <param name="productName">Nom du produit</param>
    /// <returns>L'analyse de prix pour le produit spécifié</returns>
    [HttpGet("marketprices/product/{productName}")]
    [ProducesResponseType(typeof(MarketPriceAnalysisDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MarketPriceAnalysisDto>> GetMarketPriceAnalysisByProduct(string productName)
    {
        try
        {
            var marketPrice = await _analyticsService.GetMarketPriceAnalysisByProductAsync(productName);
            if (marketPrice == null)
                return NotFound($"Analyse de prix pour le produit '{productName}' non trouvée");

            return Ok(marketPrice);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération de l'analyse de prix pour le produit {ProductName}", productName);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère toutes les données de saisonnalité
    /// </summary>
    /// <returns>Liste des données de saisonnalité</returns>
    [HttpGet("seasonality")]
    [ProducesResponseType(typeof(IEnumerable<SeasonalityAnalysisDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SeasonalityAnalysisDto>>> GetSeasonalityAnalyses()
    {
        try
        {
            var seasonalityData = await _analyticsService.GetSeasonalityAnalysesAsync();
            return Ok(seasonalityData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des données de saisonnalité");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère les données de saisonnalité pour un mois spécifique
    /// </summary>
    /// <param name="monthName">Nom du mois</param>
    /// <returns>Les données de saisonnalité pour le mois spécifié</returns>
    [HttpGet("seasonality/month/{monthName}")]
    [ProducesResponseType(typeof(SeasonalityAnalysisDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SeasonalityAnalysisDto>> GetSeasonalityAnalysisByMonth(string monthName)
    {
        try
        {
            var seasonality = await _analyticsService.GetSeasonalityAnalysisByMonthAsync(monthName);
            if (seasonality == null)
                return NotFound($"Données de saisonnalité pour le mois '{monthName}' non trouvées");

            return Ok(seasonality);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des données de saisonnalité pour le mois {MonthName}", monthName);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère tous les scores de performance IA
    /// </summary>
    /// <returns>Liste des scores de performance</returns>
    [HttpGet("performancescores")]
    [ProducesResponseType(typeof(IEnumerable<PerformanceScoreDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PerformanceScoreDto>>> GetPerformanceScores()
    {
        try
        {
            var performanceScores = await _analyticsService.GetPerformanceScoresAsync();
            return Ok(performanceScores);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des scores de performance");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère le score de performance pour une métrique spécifique
    /// </summary>
    /// <param name="metricName">Nom de la métrique</param>
    /// <returns>Le score de performance pour la métrique spécifiée</returns>
    [HttpGet("performancescores/metric/{metricName}")]
    [ProducesResponseType(typeof(PerformanceScoreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PerformanceScoreDto>> GetPerformanceScoreByMetric(string metricName)
    {
        try
        {
            var performanceScore = await _analyticsService.GetPerformanceScoreByMetricAsync(metricName);
            if (performanceScore == null)
                return NotFound($"Score de performance pour la métrique '{metricName}' non trouvé");

            return Ok(performanceScore);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération du score de performance pour la métrique {MetricName}", metricName);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }
}

