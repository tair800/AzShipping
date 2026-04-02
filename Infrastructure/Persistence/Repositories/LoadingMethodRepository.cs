using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;
using AzShipping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AzShipping.Infrastructure.Persistence.Repositories;

public class LoadingMethodRepository : ILoadingMethodRepository
{
    private readonly ApplicationDbContext _context;

    public LoadingMethodRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LoadingMethod>> GetAllAsync()
    {
        var loadingMethods = await _context.LoadingMethods
            .Include(lm => lm.Translations)
            .ToListAsync();

        return loadingMethods.OrderBy(lm =>
        {
            var enTranslation = lm.Translations.FirstOrDefault(t => t.LanguageCode == "EN");
            if (enTranslation != null)
                return enTranslation.Name;
            
            var firstTranslation = lm.Translations.FirstOrDefault();
            return firstTranslation?.Name ?? "";
        });
    }

    public async Task<LoadingMethod?> GetByIdAsync(Guid id)
    {
        return await _context.LoadingMethods
            .Include(lm => lm.Translations)
            .FirstOrDefaultAsync(lm => lm.Id == id);
    }

    public async Task<LoadingMethod> CreateAsync(LoadingMethod loadingMethod)
    {
        _context.LoadingMethods.Add(loadingMethod);
        await _context.SaveChangesAsync();
        return loadingMethod;
    }

    public async Task<LoadingMethod> UpdateAsync(LoadingMethod loadingMethod)
    {
        // Remove existing translations - query directly from database to avoid tracking issues
        var existingTranslations = await _context.LoadingMethodTranslations
            .Where(t => t.LoadingMethodId == loadingMethod.Id)
            .ToListAsync();
        _context.LoadingMethodTranslations.RemoveRange(existingTranslations);

        // Add new translations
        if (loadingMethod.Translations != null && loadingMethod.Translations.Any())
        {
            _context.LoadingMethodTranslations.AddRange(loadingMethod.Translations);
        }

        // Update the entity - mark only the properties that changed
        var existing = await _context.LoadingMethods.FindAsync(loadingMethod.Id);
        if (existing != null)
        {
            existing.IsActive = loadingMethod.IsActive;
            existing.UpdatedAt = loadingMethod.UpdatedAt;
        }
        else
        {
            _context.LoadingMethods.Update(loadingMethod);
        }

        await _context.SaveChangesAsync();
        
        // Reload the entity with includes to ensure translations are loaded
        return await GetByIdAsync(loadingMethod.Id) ?? loadingMethod;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var loadingMethod = await _context.LoadingMethods.FindAsync(id);
        if (loadingMethod == null)
            return false;

        _context.LoadingMethods.Remove(loadingMethod);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.LoadingMethods.AnyAsync(lm => lm.Id == id);
    }
}

