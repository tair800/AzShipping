using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;
using AzShipping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AzShipping.Infrastructure.Persistence.Repositories;

public class DrivingLicenceCategoryRepository : IDrivingLicenceCategoryRepository
{
    private readonly ApplicationDbContext _context;

    public DrivingLicenceCategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DrivingLicenceCategory>> GetAllAsync()
    {
        return await _context.DrivingLicenceCategories
            .OrderBy(dlc => dlc.Name)
            .ToListAsync();
    }

    public async Task<DrivingLicenceCategory?> GetByIdAsync(Guid id)
    {
        return await _context.DrivingLicenceCategories.FindAsync(id);
    }

    public async Task<DrivingLicenceCategory> CreateAsync(DrivingLicenceCategory category)
    {
        _context.DrivingLicenceCategories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<DrivingLicenceCategory> UpdateAsync(DrivingLicenceCategory category)
    {
        // Get the existing entity from the database to ensure proper tracking
        var existing = await _context.DrivingLicenceCategories.FindAsync(category.Id);
        if (existing == null)
            throw new InvalidOperationException($"DrivingLicenceCategory with ID {category.Id} not found");

        // Update properties
        existing.Name = category.Name;
        existing.IsActive = category.IsActive;
        existing.UpdatedAt = category.UpdatedAt;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var category = await _context.DrivingLicenceCategories.FindAsync(id);
        if (category == null)
            return false;

        _context.DrivingLicenceCategories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.DrivingLicenceCategories.AnyAsync(dlc => dlc.Id == id);
    }
}

