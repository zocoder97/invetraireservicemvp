using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Services;

/// <summary>
/// Service de gestion des produits avec support multi-tenant
/// </summary>
public class ProductService : IProductService
{
    private readonly ITenantRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public ProductService(ITenantRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(Guid entrepriseId)
    {
        var products = await _productRepository.GetAllByEntrepriseAsync(entrepriseId);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetProductByIdAsync(Guid id, Guid entrepriseId)
    {
        var product = await _productRepository.GetByIdAndEntrepriseAsync(id, entrepriseId);
        return product != null ? _mapper.Map<ProductDto>(product) : null;
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto, Guid entrepriseId)
    {
        var product = _mapper.Map<Product>(createProductDto);
        product.ProductId = Guid.NewGuid();
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;

        var createdProduct = await _productRepository.AddAsync(product, entrepriseId);
        return _mapper.Map<ProductDto>(createdProduct);
    }

    public async Task<ProductDto?> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto, Guid entrepriseId)
    {
        var existingProduct = await _productRepository.GetByIdAndEntrepriseAsync(id, entrepriseId);
        if (existingProduct == null)
            return null;

        // Mise à jour des propriétés non nulles
        if (!string.IsNullOrEmpty(updateProductDto.Name))
            existingProduct.Name = updateProductDto.Name;
        
        if (updateProductDto.CurrentStock.HasValue)
            existingProduct.CurrentStock = updateProductDto.CurrentStock.Value;
        
        if (updateProductDto.OptimalStock.HasValue)
            existingProduct.OptimalStock = updateProductDto.OptimalStock.Value;
        
        if (updateProductDto.ReorderPoint.HasValue)
            existingProduct.ReorderPoint = updateProductDto.ReorderPoint.Value;
        
        if (updateProductDto.Trend.HasValue)
            existingProduct.Trend = updateProductDto.Trend.Value;
        
        if (updateProductDto.Cost.HasValue)
            existingProduct.Cost = updateProductDto.Cost.Value;

        existingProduct.UpdatedAt = DateTime.UtcNow;

        var updatedProduct = await _productRepository.UpdateAsync(existingProduct, entrepriseId);
        return updatedProduct != null ? _mapper.Map<ProductDto>(updatedProduct) : null;
    }

    public async Task<bool> DeleteProductAsync(Guid id, Guid entrepriseId)
    {
        return await _productRepository.DeleteAsync(id, entrepriseId);
    }

    public async Task<IEnumerable<ProductDto>> GetCriticalStockProductsAsync(Guid entrepriseId)
    {
        var criticalProducts = await _productRepository.FindByEntrepriseAsync(p => p.Trend == TrendType.Critical, entrepriseId);
        return _mapper.Map<IEnumerable<ProductDto>>(criticalProducts);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsToReorderAsync(Guid entrepriseId)
    {
        var productsToReorder = await _productRepository.FindByEntrepriseAsync(p => p.CurrentStock <= p.ReorderPoint, entrepriseId);
        return _mapper.Map<IEnumerable<ProductDto>>(productsToReorder);
    }
}

