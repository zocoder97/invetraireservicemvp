using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Services;

/// <summary>
/// Service de gestion des fournisseurs
/// </summary>
public class SupplierService : ISupplierService
{
    private readonly IRepository<Supplier> _supplierRepository;
    private readonly IMapper _mapper;

    public SupplierService(IRepository<Supplier> supplierRepository, IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync()
    {
        var suppliers = await _supplierRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
    }

    public async Task<SupplierDto?> GetSupplierByIdAsync(Guid id)
    {
        var supplier = await _supplierRepository.GetByIdAsync(id);
        return supplier != null ? _mapper.Map<SupplierDto>(supplier) : null;
    }

    public async Task<SupplierDto> CreateSupplierAsync(CreateSupplierDto createSupplierDto)
    {
        var supplier = _mapper.Map<Supplier>(createSupplierDto);
        supplier.SupplierId = Guid.NewGuid();
        supplier.CreatedAt = DateTime.UtcNow;
        supplier.UpdatedAt = DateTime.UtcNow;

        var createdSupplier = await _supplierRepository.AddAsync(supplier);
        return _mapper.Map<SupplierDto>(createdSupplier);
    }

    public async Task<SupplierDto?> UpdateSupplierAsync(Guid id, UpdateSupplierDto updateSupplierDto)
    {
        var existingSupplier = await _supplierRepository.GetByIdAsync(id);
        if (existingSupplier == null)
            return null;

        // Mise à jour des propriétés non nulles
        if (!string.IsNullOrEmpty(updateSupplierDto.Name))
            existingSupplier.Name = updateSupplierDto.Name;
        
        if (updateSupplierDto.Reliability.HasValue)
            existingSupplier.Reliability = updateSupplierDto.Reliability.Value;
        
        if (updateSupplierDto.PriceScore.HasValue)
            existingSupplier.PriceScore = updateSupplierDto.PriceScore.Value;
        
        if (updateSupplierDto.DeliveryScore.HasValue)
            existingSupplier.DeliveryScore = updateSupplierDto.DeliveryScore.Value;
        
        if (updateSupplierDto.QualityScore.HasValue)
            existingSupplier.QualityScore = updateSupplierDto.QualityScore.Value;
        
        if (updateSupplierDto.OverallScore.HasValue)
            existingSupplier.OverallScore = updateSupplierDto.OverallScore.Value;

        existingSupplier.UpdatedAt = DateTime.UtcNow;

        var updatedSupplier = await _supplierRepository.UpdateAsync(existingSupplier);
        return _mapper.Map<SupplierDto>(updatedSupplier);
    }

    public async Task<bool> DeleteSupplierAsync(Guid id)
    {
        var supplier = await _supplierRepository.GetByIdAsync(id);
        if (supplier == null)
            return false;

        await _supplierRepository.DeleteAsync(supplier);
        return true;
    }

    public async Task<IEnumerable<SupplierDto>> GetTopSuppliersAsync(int count = 10)
    {
        var allSuppliers = await _supplierRepository.GetAllAsync();
        var topSuppliers = allSuppliers
            .OrderByDescending(s => s.OverallScore)
            .Take(count);
        
        return _mapper.Map<IEnumerable<SupplierDto>>(topSuppliers);
    }
}

