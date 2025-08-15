using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Services;

/// <summary>
/// Service d'analyses et de données IA
/// </summary>
public class AnalyticsService : IAnalyticsService
{
    private readonly IRepository<DemandPrediction> _demandPredictionRepository;
    private readonly IRepository<CostAnalysis> _costAnalysisRepository;
    private readonly IRepository<MarketPriceAnalysis> _marketPriceRepository;
    private readonly IRepository<SeasonalityAnalysis> _seasonalityRepository;
    private readonly IRepository<PerformanceScore> _performanceScoreRepository;
    private readonly IMapper _mapper;

    public AnalyticsService(
        IRepository<DemandPrediction> demandPredictionRepository,
        IRepository<CostAnalysis> costAnalysisRepository,
        IRepository<MarketPriceAnalysis> marketPriceRepository,
        IRepository<SeasonalityAnalysis> seasonalityRepository,
        IRepository<PerformanceScore> performanceScoreRepository,
        IMapper mapper)
    {
        _demandPredictionRepository = demandPredictionRepository;
        _costAnalysisRepository = costAnalysisRepository;
        _marketPriceRepository = marketPriceRepository;
        _seasonalityRepository = seasonalityRepository;
        _performanceScoreRepository = performanceScoreRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DemandPredictionDto>> GetDemandPredictionsAsync()
    {
        var predictions = await _demandPredictionRepository.GetAllAsync();
        var sortedPredictions = predictions.OrderBy(p => p.Date);
        return _mapper.Map<IEnumerable<DemandPredictionDto>>(sortedPredictions);
    }

    public async Task<IEnumerable<DemandPredictionDto>> GetLatestDemandPredictionsAsync(int days = 7)
    {
        var fromDate = DateTime.UtcNow.Date.AddDays(-days);
        var predictions = await _demandPredictionRepository.FindAsync(p => p.Date >= fromDate);
        var sortedPredictions = predictions.OrderBy(p => p.Date);
        return _mapper.Map<IEnumerable<DemandPredictionDto>>(sortedPredictions);
    }

    public async Task<IEnumerable<CostAnalysisDto>> GetCostAnalysesAsync()
    {
        var costAnalyses = await _costAnalysisRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CostAnalysisDto>>(costAnalyses);
    }

    public async Task<CostAnalysisDto?> GetCostAnalysisByCategoryAsync(string categoryName)
    {
        var costAnalyses = await _costAnalysisRepository.FindAsync(c => 
            c.Category.ToLower() == categoryName.ToLower());
        var costAnalysis = costAnalyses.FirstOrDefault();
        return costAnalysis != null ? _mapper.Map<CostAnalysisDto>(costAnalysis) : null;
    }

    public async Task<IEnumerable<MarketPriceAnalysisDto>> GetMarketPriceAnalysesAsync()
    {
        var marketPrices = await _marketPriceRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<MarketPriceAnalysisDto>>(marketPrices);
    }

    public async Task<MarketPriceAnalysisDto?> GetMarketPriceAnalysisByProductAsync(string productName)
    {
        var marketPrices = await _marketPriceRepository.FindAsync(m => 
            m.Product.ToLower() == productName.ToLower());
        var marketPrice = marketPrices.FirstOrDefault();
        return marketPrice != null ? _mapper.Map<MarketPriceAnalysisDto>(marketPrice) : null;
    }

    public async Task<IEnumerable<SeasonalityAnalysisDto>> GetSeasonalityAnalysesAsync()
    {
        var seasonalityData = await _seasonalityRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SeasonalityAnalysisDto>>(seasonalityData);
    }

    public async Task<SeasonalityAnalysisDto?> GetSeasonalityAnalysisByMonthAsync(string monthName)
    {
        var seasonalityData = await _seasonalityRepository.FindAsync(s => 
            s.Month.ToLower() == monthName.ToLower());
        var seasonality = seasonalityData.FirstOrDefault();
        return seasonality != null ? _mapper.Map<SeasonalityAnalysisDto>(seasonality) : null;
    }

    public async Task<IEnumerable<PerformanceScoreDto>> GetPerformanceScoresAsync()
    {
        var performanceScores = await _performanceScoreRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PerformanceScoreDto>>(performanceScores);
    }

    public async Task<PerformanceScoreDto?> GetPerformanceScoreByMetricAsync(string metricName)
    {
        var performanceScores = await _performanceScoreRepository.FindAsync(p => 
            p.Metric.ToLower() == metricName.ToLower());
        var performanceScore = performanceScores.FirstOrDefault();
        return performanceScore != null ? _mapper.Map<PerformanceScoreDto>(performanceScore) : null;
    }
}

