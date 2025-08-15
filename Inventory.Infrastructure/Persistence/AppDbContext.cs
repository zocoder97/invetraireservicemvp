using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;

namespace Inventory.Infrastructure.Persistence;

/// <summary>
/// Contexte de base de données pour l'application d'inventaire
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSets pour chaque entité
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SmartAlert> SmartAlerts { get; set; }
    public DbSet<DemandPrediction> DemandPredictions { get; set; }
    public DbSet<CostAnalysis> CostAnalyses { get; set; }
    public DbSet<MarketPriceAnalysis> MarketPriceAnalyses { get; set; }
    public DbSet<SeasonalityAnalysis> SeasonalityAnalyses { get; set; }
    public DbSet<PerformanceScore> PerformanceScores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration des entités
        ConfigureProduct(modelBuilder);
        ConfigureSupplier(modelBuilder);
        ConfigureSmartAlert(modelBuilder);
        ConfigureDemandPrediction(modelBuilder);
        ConfigureCostAnalysis(modelBuilder);
        ConfigureMarketPriceAnalysis(modelBuilder);
        ConfigureSeasonalityAnalysis(modelBuilder);
        ConfigurePerformanceScore(modelBuilder);
    }

    private void ConfigureProduct(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId);
            entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Cost).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Trend).HasConversion<string>();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }

    private void ConfigureSupplier(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId);
            entity.Property(e => e.SupplierId).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }

    private void ConfigureSmartAlert(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SmartAlert>(entity =>
        {
            entity.HasKey(e => e.AlertId);
            entity.Property(e => e.AlertId).ValueGeneratedOnAdd();
            entity.Property(e => e.Message).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Type).HasConversion<string>();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }

    private void ConfigureDemandPrediction(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DemandPrediction>(entity =>
        {
            entity.HasKey(e => e.PredictionId);
            entity.Property(e => e.PredictionId).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }

    private void ConfigureCostAnalysis(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CostAnalysis>(entity =>
        {
            entity.HasKey(e => e.CostAnalysisId);
            entity.Property(e => e.CostAnalysisId).ValueGeneratedOnAdd();
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Budget).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Spent).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Variance).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Trend).HasConversion<string>();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }

    private void ConfigureMarketPriceAnalysis(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MarketPriceAnalysis>(entity =>
        {
            entity.HasKey(e => e.MarketPriceId);
            entity.Property(e => e.MarketPriceId).ValueGeneratedOnAdd();
            entity.Property(e => e.Product).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CurrentPrice).HasColumnType("decimal(18,2)");
            entity.Property(e => e.MarketAvg).HasColumnType("decimal(18,2)");
            entity.Property(e => e.BestPrice).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Savings).HasColumnType("decimal(18,2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }

    private void ConfigureSeasonalityAnalysis(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SeasonalityAnalysis>(entity =>
        {
            entity.HasKey(e => e.SeasonalityId);
            entity.Property(e => e.SeasonalityId).ValueGeneratedOnAdd();
            entity.Property(e => e.Month).IsRequired().HasMaxLength(20);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }

    private void ConfigurePerformanceScore(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PerformanceScore>(entity =>
        {
            entity.HasKey(e => e.ScoreId);
            entity.Property(e => e.ScoreId).ValueGeneratedOnAdd();
            entity.Property(e => e.Metric).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Status).HasConversion<string>();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}

