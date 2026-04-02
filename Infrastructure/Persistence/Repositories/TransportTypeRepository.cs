using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;
using AzShipping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AzShipping.Infrastructure.Persistence.Repositories;

public class TransportTypeRepository : ITransportTypeRepository
{
    private readonly ApplicationDbContext _context;

    public TransportTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TransportType>> GetAllAsync()
    {
        return await _context.TransportTypes
            .Include(t => t.Translations)
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    public async Task<TransportType?> GetByIdAsync(Guid id)
    {
        return await _context.TransportTypes
            .Include(t => t.Translations)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TransportType> CreateAsync(TransportType transportType)
    {
        _context.TransportTypes.Add(transportType);
        await _context.SaveChangesAsync();
        return transportType;
    }

    public async Task<TransportType> UpdateAsync(TransportType transportType)
    {
        // Remove existing translations - query directly from database to avoid tracking issues
        var existingTranslations = await _context.TransportTypeTranslations
            .Where(t => t.TransportTypeId == transportType.Id)
            .ToListAsync();
        _context.TransportTypeTranslations.RemoveRange(existingTranslations);

        // Add new translations
        if (transportType.Translations != null && transportType.Translations.Any())
        {
            _context.TransportTypeTranslations.AddRange(transportType.Translations);
        }

        // Update the entity - mark only the properties that changed
        var existing = await _context.TransportTypes.FindAsync(transportType.Id);
        if (existing != null)
        {
            existing.Name = transportType.Name;
            existing.IsAir = transportType.IsAir;
            existing.IsSea = transportType.IsSea;
            existing.IsRoad = transportType.IsRoad;
            existing.IsRail = transportType.IsRail;
            existing.IsActive = transportType.IsActive;
            existing.UpdatedAt = transportType.UpdatedAt;
            
            // Explicitly mark Name as modified to ensure it's saved
            _context.Entry(existing).Property(e => e.Name).IsModified = true;
        }
        else
        {
            _context.TransportTypes.Update(transportType);
        }

        await _context.SaveChangesAsync();
        
        // Reload the entity with includes to ensure translations are loaded
        return await GetByIdAsync(transportType.Id) ?? transportType;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var transportType = await _context.TransportTypes.FindAsync(id);
        if (transportType == null)
            return false;

        _context.TransportTypes.Remove(transportType);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.TransportTypes.AnyAsync(t => t.Id == id);
    }
}

