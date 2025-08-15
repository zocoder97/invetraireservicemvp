using AutoMapper;
using Inventory.Domain.Entities;
using Inventory.Application.DTOs;
using Inventory.Domain.Enums;

namespace Inventory.Application.Mappers;

/// <summary>
/// Profil de mapping AutoMapper pour les entités et DTOs
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapping Product
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Trend, opt => opt.MapFrom(src => src.Trend.ToString().ToLower()));
        CreateMap<CreateProductDto, Product>()
            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping Supplier
        CreateMap<Supplier, SupplierDto>();
        CreateMap<CreateSupplierDto, Supplier>()
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping SmartAlert
        CreateMap<SmartAlert, SmartAlertDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString().ToLower()))
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => GetTimeAgo(src.CreatedAt)));
        CreateMap<CreateSmartAlertDto, SmartAlert>()
            .ForMember(dest => dest.AlertId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsRead, opt => opt.MapFrom(src => false));

        // Mapping DemandPrediction
        CreateMap<DemandPrediction, DemandPredictionDto>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd MMM")));

        // Mapping CostAnalysis
        CreateMap<CostAnalysis, CostAnalysisDto>()
            .ForMember(dest => dest.Trend, opt => opt.MapFrom(src => src.Trend.ToString().ToLower()));

        // Mapping MarketPriceAnalysis
        CreateMap<MarketPriceAnalysis, MarketPriceAnalysisDto>();

        // Mapping SeasonalityAnalysis
        CreateMap<SeasonalityAnalysis, SeasonalityAnalysisDto>();

        // Mapping PerformanceScore
        CreateMap<PerformanceScore, PerformanceScoreDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString().ToLower()));
    }

    /// <summary>
    /// Calcule le temps écoulé depuis une date donnée
    /// </summary>
    /// <param name="dateTime">Date de référence</param>
    /// <returns>Temps écoulé formaté (ex: "2h", "1j")</returns>
    private static string GetTimeAgo(DateTime dateTime)
    {
        var timeSpan = DateTime.UtcNow - dateTime;

        if (timeSpan.TotalMinutes < 60)
            return $"{(int)timeSpan.TotalMinutes}min";
        
        if (timeSpan.TotalHours < 24)
            return $"{(int)timeSpan.TotalHours}h";
        
        if (timeSpan.TotalDays < 30)
            return $"{(int)timeSpan.TotalDays}j";
        
        return $"{(int)(timeSpan.TotalDays / 30)}mois";
    }
}

