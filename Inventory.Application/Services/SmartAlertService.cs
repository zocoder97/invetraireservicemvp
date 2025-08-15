using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Services;

/// <summary>
/// Service de gestion des alertes intelligentes
/// </summary>
public class SmartAlertService : ISmartAlertService
{
    private readonly IRepository<SmartAlert> _alertRepository;
    private readonly IMapper _mapper;

    public SmartAlertService(IRepository<SmartAlert> alertRepository, IMapper mapper)
    {
        _alertRepository = alertRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SmartAlertDto>> GetAllAlertsAsync()
    {
        var alerts = await _alertRepository.GetAllAsync();
        var sortedAlerts = alerts.OrderByDescending(a => a.CreatedAt);
        return _mapper.Map<IEnumerable<SmartAlertDto>>(sortedAlerts);
    }

    public async Task<IEnumerable<SmartAlertDto>> GetAlertsByTypeAsync(AlertType alertType)
    {
        var alerts = await _alertRepository.FindAsync(a => a.Type == alertType);
        var sortedAlerts = alerts.OrderByDescending(a => a.CreatedAt);
        return _mapper.Map<IEnumerable<SmartAlertDto>>(sortedAlerts);
    }

    public async Task<IEnumerable<SmartAlertDto>> GetCriticalAlertsAsync()
    {
        return await GetAlertsByTypeAsync(AlertType.Critical);
    }

    public async Task<IEnumerable<SmartAlertDto>> GetUnreadAlertsAsync()
    {
        var unreadAlerts = await _alertRepository.FindAsync(a => !a.IsRead);
        var sortedAlerts = unreadAlerts.OrderByDescending(a => a.CreatedAt);
        return _mapper.Map<IEnumerable<SmartAlertDto>>(sortedAlerts);
    }

    public async Task<SmartAlertDto> CreateAlertAsync(CreateSmartAlertDto createAlertDto)
    {
        var alert = _mapper.Map<SmartAlert>(createAlertDto);
        alert.AlertId = Guid.NewGuid();
        alert.CreatedAt = DateTime.UtcNow;
        alert.IsRead = false;

        var createdAlert = await _alertRepository.AddAsync(alert);
        return _mapper.Map<SmartAlertDto>(createdAlert);
    }

    public async Task<bool> MarkAsReadAsync(Guid alertId)
    {
        var alert = await _alertRepository.GetByIdAsync(alertId);
        if (alert == null)
            return false;

        alert.IsRead = true;
        await _alertRepository.UpdateAsync(alert);
        return true;
    }

    public async Task<bool> DeleteAlertAsync(Guid alertId)
    {
        var alert = await _alertRepository.GetByIdAsync(alertId);
        if (alert == null)
            return false;

        await _alertRepository.DeleteAsync(alert);
        return true;
    }
}

