using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;
using AzShipping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AzShipping.Infrastructure.Persistence.Repositories;

public class SalesFunnelStatusRepository : ISalesFunnelStatusRepository
{
    private readonly ApplicationDbContext _context;

    public SalesFunnelStatusRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SalesFunnelStatus>> GetAllAsync()
    {
        return await _context.SalesFunnelStatuses
            .Include(s => s.ResponsibleManager)
            .Include(s => s.Translations)
            .OrderBy(s => s.StatusPosition)
            .ToListAsync();
    }

    public async Task<SalesFunnelStatus?> GetByIdAsync(Guid id)
    {
        return await _context.SalesFunnelStatuses
            .Include(s => s.ResponsibleManager)
            .Include(s => s.Translations)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<SalesFunnelStatus> CreateAsync(SalesFunnelStatus status)
    {
        _context.SalesFunnelStatuses.Add(status);
        await _context.SaveChangesAsync();
        return status;
    }

    public async Task<SalesFunnelStatus> UpdateAsync(SalesFunnelStatus status)
    {
        // Remove existing translations
        var existingTranslations = await _context.SalesFunnelStatusTranslations
            .Where(t => t.SalesFunnelStatusId == status.Id)
            .ToListAsync();
        _context.SalesFunnelStatusTranslations.RemoveRange(existingTranslations);

        // Add new translations
        if (status.Translations != null && status.Translations.Any())
        {
            _context.SalesFunnelStatusTranslations.AddRange(status.Translations);
        }

        _context.SalesFunnelStatuses.Update(status);
        await _context.SaveChangesAsync();
        
        // Reload the entity with includes to ensure translations are loaded
        return await GetByIdAsync(status.Id) ?? status;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var status = await _context.SalesFunnelStatuses.FindAsync(id);
        if (status == null)
            return false;

        _context.SalesFunnelStatuses.Remove(status);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.SalesFunnelStatuses.AnyAsync(s => s.Id == id);
    }
}

