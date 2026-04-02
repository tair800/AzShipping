using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;
using AzShipping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AzShipping.Infrastructure.Persistence.Repositories;

public class PackagingRepository : IPackagingRepository
{
    private readonly ApplicationDbContext _context;

    public PackagingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Packaging>> GetAllAsync()
    {
        var packagings = await _context.Packagings
            .Include(p => p.Translations)
            .ToListAsync();

        return packagings.OrderBy(p =>
        {
            var enTranslation = p.Translations.FirstOrDefault(t => t.LanguageCode == "EN");
            if (enTranslation != null)
                return enTranslation.Name;
            
            var firstTranslation = p.Translations.FirstOrDefault();
            return firstTranslation?.Name ?? "";
        });
    }

    public async Task<Packaging?> GetByIdAsync(Guid id)
    {
        return await _context.Packagings
            .Include(p => p.Translations)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Packaging> CreateAsync(Packaging packaging)
    {
        _context.Packagings.Add(packaging);
        await _context.SaveChangesAsync();
        return packaging;
    }

    public async Task<Packaging> UpdateAsync(Packaging packaging)
    {
        // Remove existing translations
        var existingTranslations = await _context.PackagingTranslations
            .Where(t => t.PackagingId == packaging.Id)
            .ToListAsync();
        _context.PackagingTranslations.RemoveRange(existingTranslations);

        // Add new translations
        _context.PackagingTranslations.AddRange(packaging.Translations);

        _context.Packagings.Update(packaging);
        await _context.SaveChangesAsync();
        return packaging;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var packaging = await _context.Packagings.FindAsync(id);
        if (packaging == null)
            return false;

        _context.Packagings.Remove(packaging);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Packagings.AnyAsync(p => p.Id == id);
    }
}

