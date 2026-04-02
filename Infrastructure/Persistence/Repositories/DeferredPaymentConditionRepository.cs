using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;
using AzShipping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AzShipping.Infrastructure.Persistence.Repositories;

public class DeferredPaymentConditionRepository : IDeferredPaymentConditionRepository
{
    private readonly ApplicationDbContext _context;

    public DeferredPaymentConditionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DeferredPaymentCondition>> GetAllAsync()
    {
        return await _context.DeferredPaymentConditions
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<DeferredPaymentCondition?> GetByIdAsync(Guid id)
    {
        return await _context.DeferredPaymentConditions
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<DeferredPaymentCondition> CreateAsync(DeferredPaymentCondition condition)
    {
        _context.DeferredPaymentConditions.Add(condition);
        await _context.SaveChangesAsync();
        return condition;
    }

    public async Task<DeferredPaymentCondition> UpdateAsync(DeferredPaymentCondition condition)
    {
        // Get the existing entity from the database to ensure proper tracking
        var existing = await _context.DeferredPaymentConditions.FindAsync(condition.Id);
        if (existing == null)
            throw new InvalidOperationException($"DeferredPaymentCondition with ID {condition.Id} not found");

        // Update properties
        existing.Name = condition.Name;
        existing.FullText = condition.FullText;
        existing.IsActive = condition.IsActive;
        existing.UpdatedAt = condition.UpdatedAt;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var condition = await _context.DeferredPaymentConditions.FindAsync(id);
        if (condition == null)
            return false;

        _context.DeferredPaymentConditions.Remove(condition);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.DeferredPaymentConditions.AnyAsync(c => c.Id == id);
    }
}

