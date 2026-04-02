using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;
using AzShipping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AzShipping.Infrastructure.Persistence.Repositories;

public class CarrierTypeRepository : ICarrierTypeRepository
{
    private readonly ApplicationDbContext _context;

    public CarrierTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CarrierType>> GetAllAsync()
    {
        return await _context.CarrierTypes
            .OrderBy(ct => ct.Name)
            .ToListAsync();
    }

    public async Task<CarrierType?> GetByIdAsync(Guid id)
    {
        return await _context.CarrierTypes
            .FirstOrDefaultAsync(ct => ct.Id == id);
    }

    public async Task<CarrierType> CreateAsync(CarrierType carrierType)
    {
        _context.CarrierTypes.Add(carrierType);
        await _context.SaveChangesAsync();
        return carrierType;
    }

    public async Task<CarrierType> UpdateAsync(CarrierType carrierType)
    {
        var existing = await _context.CarrierTypes.FindAsync(carrierType.Id);
        if (existing != null)
        {
            existing.Name = carrierType.Name;
            existing.IsActive = carrierType.IsActive;
            existing.Colour = carrierType.Colour;
            existing.IsDefault = carrierType.IsDefault;
            existing.UpdatedAt = carrierType.UpdatedAt;
        }
        else
        {
            _context.CarrierTypes.Update(carrierType);
        }

        await _context.SaveChangesAsync();
        return carrierType;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var carrierType = await _context.CarrierTypes.FindAsync(id);
        if (carrierType == null)
            return false;

        _context.CarrierTypes.Remove(carrierType);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.CarrierTypes.AnyAsync(ct => ct.Id == id);
    }
}

