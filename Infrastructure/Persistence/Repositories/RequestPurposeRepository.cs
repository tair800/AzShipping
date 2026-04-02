using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;
using AzShipping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AzShipping.Infrastructure.Persistence.Repositories;

public class RequestPurposeRepository : IRequestPurposeRepository
{
    private readonly ApplicationDbContext _context;

    public RequestPurposeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RequestPurpose>> GetAllAsync()
    {
        return await _context.RequestPurposes
            .OrderBy(rp => rp.Name)
            .ToListAsync();
    }

    public async Task<RequestPurpose?> GetByIdAsync(Guid id)
    {
        return await _context.RequestPurposes.FindAsync(id);
    }

    public async Task<RequestPurpose> CreateAsync(RequestPurpose requestPurpose)
    {
        _context.RequestPurposes.Add(requestPurpose);
        await _context.SaveChangesAsync();
        return requestPurpose;
    }

    public async Task<RequestPurpose> UpdateAsync(RequestPurpose requestPurpose)
    {
        // Get the existing entity from the database to ensure proper tracking
        var existing = await _context.RequestPurposes.FindAsync(requestPurpose.Id);
        if (existing == null)
            throw new InvalidOperationException($"RequestPurpose with ID {requestPurpose.Id} not found");

        // Update properties
        existing.Name = requestPurpose.Name;
        existing.IsActive = requestPurpose.IsActive;
        existing.UpdatedAt = requestPurpose.UpdatedAt;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var purpose = await _context.RequestPurposes.FindAsync(id);
        if (purpose == null)
            return false;

        _context.RequestPurposes.Remove(purpose);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.RequestPurposes.AnyAsync(rp => rp.Id == id);
    }
}

