using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;
using AzShipping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AzShipping.Infrastructure.Persistence.Repositories;

public class WorkerPostRepository : IWorkerPostRepository
{
    private readonly ApplicationDbContext _context;

    public WorkerPostRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkerPost>> GetAllAsync()
    {
        var posts = await _context.WorkerPosts
            .Include(wp => wp.Translations)
            .ToListAsync();

        return posts.OrderBy(wp =>
        {
            var enTranslation = wp.Translations.FirstOrDefault(t => t.LanguageCode == "EN");
            if (enTranslation != null)
                return enTranslation.Name;
            
            var firstTranslation = wp.Translations.FirstOrDefault();
            return firstTranslation?.Name ?? "";
        });
    }

    public async Task<WorkerPost?> GetByIdAsync(Guid id)
    {
        return await _context.WorkerPosts
            .Include(wp => wp.Translations)
            .FirstOrDefaultAsync(wp => wp.Id == id);
    }

    public async Task<WorkerPost> CreateAsync(WorkerPost workerPost)
    {
        _context.WorkerPosts.Add(workerPost);
        await _context.SaveChangesAsync();
        return workerPost;
    }

    public async Task<WorkerPost> UpdateAsync(WorkerPost workerPost)
    {
        // Remove existing translations - query directly from database to avoid tracking issues
        var existingTranslations = await _context.WorkerPostTranslations
            .Where(t => t.WorkerPostId == workerPost.Id)
            .ToListAsync();
        _context.WorkerPostTranslations.RemoveRange(existingTranslations);

        // Add new translations
        if (workerPost.Translations != null && workerPost.Translations.Any())
        {
            _context.WorkerPostTranslations.AddRange(workerPost.Translations);
        }

        // Update the entity - mark only the properties that changed
        var existing = await _context.WorkerPosts.FindAsync(workerPost.Id);
        if (existing != null)
        {
            existing.IsActive = workerPost.IsActive;
            existing.UpdatedAt = workerPost.UpdatedAt;
        }
        else
        {
            _context.WorkerPosts.Update(workerPost);
        }

        await _context.SaveChangesAsync();
        
        // Reload the entity with includes to ensure translations are loaded
        return await GetByIdAsync(workerPost.Id) ?? workerPost;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var post = await _context.WorkerPosts.FindAsync(id);
        if (post == null)
            return false;

        _context.WorkerPosts.Remove(post);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.WorkerPosts.AnyAsync(wp => wp.Id == id);
    }
}

