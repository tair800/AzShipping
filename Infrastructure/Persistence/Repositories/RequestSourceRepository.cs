using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;
using AzShipping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AzShipping.Infrastructure.Persistence.Repositories;

public class RequestSourceRepository : IRequestSourceRepository
{
    private readonly ApplicationDbContext _context;

    public RequestSourceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RequestSource>> GetAllAsync()
    {
        return await _context.RequestSources
            .OrderBy(rs => rs.Name)
            .ToListAsync();
    }

    public async Task<RequestSource?> GetByIdAsync(Guid id)
    {
        return await _context.RequestSources.FindAsync(id);
    }

    public async Task<RequestSource> CreateAsync(RequestSource requestSource)
    {
        _context.RequestSources.Add(requestSource);
        await _context.SaveChangesAsync();
        return requestSource;
    }

    public async Task<RequestSource> UpdateAsync(RequestSource requestSource)
    {
        _context.RequestSources.Update(requestSource);
        await _context.SaveChangesAsync();
        return requestSource;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var source = await _context.RequestSources.FindAsync(id);
        if (source == null)
            return false;

        _context.RequestSources.Remove(source);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.RequestSources.AnyAsync(rs => rs.Id == id);
    }
}

