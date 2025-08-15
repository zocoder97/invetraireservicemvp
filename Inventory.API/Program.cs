using Microsoft.EntityFrameworkCore;
using Inventory.Infrastructure.Persistence;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Repositories;
using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Inventory.Application.Mappers;
using Inventory.API.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configuration des services
builder.Services.AddControllers();

// Configuration de la base de données PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuration d'AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configuration HttpContextAccessor pour le TenantService
builder.Services.AddHttpContextAccessor();

// Injection de dépendances - Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ITenantRepository<>), typeof(TenantRepository<>));

// Injection de dépendances - Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ISmartAlertService, SmartAlertService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<ITenantService, TenantService>();

// Configuration CORS pour permettre les requêtes cross-origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuration Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Inventory Microservice API",
        Version = "v1",
        Description = "API pour la gestion d'inventaire des salons de beauté avec support multi-tenant",
        Contact = new OpenApiContact
        {
            Name = "Équipe de développement",
            Email = "dev@beautyplatform.com"
        }
    });

    // Ajouter le support pour l'header X-Entreprise-Id
    c.AddSecurityDefinition("EntrepriseId", new OpenApiSecurityScheme
    {
        Description = "Identifiant de l'entreprise/salon",
        Name = "X-Entreprise-Id",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "EntrepriseId"
                },
                Scheme = "ApiKeyScheme",
                Name = "X-Entreprise-Id",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });

    // Inclusion des commentaires XML pour la documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Configuration du logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configuration du pipeline de requêtes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory Microservice API v1");
        c.RoutePrefix = string.Empty; // Swagger UI à la racine
    });
}

// Middleware CORS (doit être avant UseAuthorization)
app.UseCors("AllowAll");

// Middleware de routage
app.UseRouting();

// Middleware d'autorisation (si nécessaire plus tard)
app.UseAuthorization();

// Mapping des contrôleurs
app.MapControllers();

// Endpoint de santé
app.MapGet("/health", () => Results.Ok(new { 
    status = "healthy", 
    timestamp = DateTime.UtcNow,
    service = "Inventory Microservice",
    multiTenant = true
}));

// Création automatique de la base de données en développement
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        context.Database.EnsureCreated();
        app.Logger.LogInformation("Base de données créée ou vérifiée avec succès");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Erreur lors de la création de la base de données");
    }
}

// Configuration pour écouter sur toutes les interfaces
app.Urls.Add("http://0.0.0.0:5000");

app.Logger.LogInformation("Inventory Microservice démarré avec succès (Multi-tenant activé)");

app.Run();
